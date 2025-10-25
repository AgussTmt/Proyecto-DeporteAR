using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Tools;
using DAL.Implementations.SqlServer.Helper;
using DAL.Interfaces;
using DomainModel;
using DAL.Implementations.SqlServer.Adapters;

namespace DAL.Implementations.SqlServer
{
    internal class CanchaRepository : SqlTransactRepository, ICanchaRepository
    {
        public CanchaRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        private const string _sqlSelectAll = @"SELECT
            c.IdCancha, c.Capacidad, d.Descripcion AS DeporteDesc, c.DuracionXPartido,
            c.FechaDeCreacion, c.EstadoCancha, c.Nombre, c.Precio
        FROM DbCancha c
        LEFT JOIN DbDeporte d ON c.IdDeporte = d.IdDeporte";

        private const string _sqlSelectEnabled = _sqlSelectAll + " WHERE c.EstadoCancha = 1";

        public void Add(Cancha entity)
        {
            Guid idDeporte = GetDeporteIdByDescripcion(entity.Deporte);


            string sql = @"INSERT INTO DbCancha
                            (IdCancha, Capacidad, IdDeporte, DuracionXPartido, EstadoCancha, Nombre, Precio, FechaDeCreacion)
                           VALUES
                            (@IdCancha, @Capacidad, @IdDeporte, @DuracionXPartido, @EstadoCancha, @Nombre, @Precio, @FechaDeCreacion)";
            var duracion = TimeSpan.FromMinutes(entity.DuracionXPartidoMin);


            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdCancha", entity.IdCancha),
                new SqlParameter("@Capacidad", entity.Capacidad),
                new SqlParameter("@IdDeporte", idDeporte),
                new SqlParameter("@DuracionXPartido", duracion),
                new SqlParameter("@EstadoCancha", entity.Estado),
                new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                new SqlParameter("@Precio", entity.Precio.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                new SqlParameter("@FechaDeCreacion", entity.FechaCreacion)
            );
        }

        public void CambiarHabilitado(Guid id)
        {
            string sql = @"UPDATE DbCancha 
                   SET EstadoCancha = ~EstadoCancha 
                   WHERE IdCancha = @IdCancha";

            
            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdCancha", id)
            );
        }

        public IEnumerable<Cancha> GetAll()
        {
            var canchas = new List<Cancha>();

            // 7. Usamos el ExecuteReader de la clase base
            using (var reader = base.ExecuteReader(_sqlSelectEnabled, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    canchas.Add(CanchaAdapter.Current.Get(values));
                }
            }
            return canchas;
        }

        public IEnumerable<Cancha> GetAllIncludingDisabled()
        {
            var canchas = new List<Cancha>();
            using (var reader = base.ExecuteReader(_sqlSelectAll, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    canchas.Add(CanchaAdapter.Current.Get(values));
                }
            }
            return canchas;
        }

        public Cancha GetById(Guid id)
        {
            string sql = $"{_sqlSelectAll} WHERE c.IdCancha = @IdCancha";
            Cancha cancha = null;

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdCancha", id)))
            {
                if (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    cancha = CanchaAdapter.Current.Get(values);
                }
            }
            return cancha;
        }

        public void Update(Cancha entity)
        {
            
            Guid idDeporte = GetDeporteIdByDescripcion(entity.Deporte);

            string sql = @"UPDATE DbCancha SET
                            Capacidad = @Capacidad,
                            IdDeporte = @IdDeporte,
                            DuracionXPartido = @DuracionXPartido,
                            EstadoCancha = @EstadoCancha,
                            Nombre = @Nombre,
                            Precio = @Precio
                           WHERE IdCancha = @IdCancha";

            var duracion = TimeSpan.FromMinutes(entity.DuracionXPartidoMin);
            var precioString = entity.Precio.ToString(System.Globalization.CultureInfo.InvariantCulture);

            
            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Capacidad", entity.Capacidad),
                new SqlParameter("@IdDeporte", idDeporte),
                new SqlParameter("@DuracionXPartido", duracion),
                new SqlParameter("@EstadoCancha", entity.Estado),
                new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                new SqlParameter("@Precio", precioString),
                new SqlParameter("@IdCancha", entity.IdCancha) 
            );
        }



        private Guid GetDeporteIdByDescripcion(string descripcion)
        {
            string sql = "SELECT IdDeporte FROM DbDeporte WHERE Descripcion = @Descripcion";

            // 4. Usamos el ExecuteScalar de la clase base
            object result = base.ExecuteScalar(sql, CommandType.Text, 
                new SqlParameter("@Descripcion", descripcion));

            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException($"El deporte '{descripcion}' no existe en la tabla DbDeporte.");
            }
            return (Guid)result;
        }

    }
}
