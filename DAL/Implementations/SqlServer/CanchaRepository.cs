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
    /// <summary>
    /// Repositorio SQL para gestionar las entidades <see cref="Cancha"/>.
    /// Provee métodos de ABM (CRUD) para las canchas.
    /// </summary>
    /// <remarks>
    /// Opera dentro de una transacción y conexión SQL existente (Unit of Work).
    /// </remarks>
    internal class CanchaRepository : SqlTransactRepository, ICanchaRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de conexión y transacción de una
        /// Unidad de Trabajo (Unit of Work) existente.
        /// </summary>
        /// <param name="context">La <see cref="SqlConnection"/> activa.</param>
        /// <param name="_transaction">La <see cref="SqlTransaction"/> activa.</param>
        public CanchaRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        private const string _sqlSelectAll = @"SELECT
            c.IdCancha, c.Capacidad, d.Descripcion AS DeporteDesc, c.DuracionXPartido,
            c.FechaDeCreacion, c.EstadoCancha, c.Nombre, c.Precio
        FROM DbCancha c
        LEFT JOIN DbDeporte d ON c.IdDeporte = d.IdDeporte";

        private const string _sqlSelectEnabled = _sqlSelectAll + " WHERE c.EstadoCancha = 1";

        /// <summary>
        /// Agrega una nueva <see cref="Cancha"/> a la base de datos.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Cancha"/> a insertar.</param>
        /// <remarks>
        /// Este método traduce el <c>entity.Deporte</c> (string) a su <see cref="Guid"/> (IdDeporte)
        /// y convierte <c>DuracionXPartidoMin</c> (int) a <see cref="TimeSpan"/>.
        /// También formatea el <c>Precio</c> a un string (InvariantCulture).
        /// </remarks>
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


        /// <summary>
        /// Invierte (toggle) el estado de habilitación (<c>EstadoCancha</c>) de una cancha.
        /// </summary>
        /// <param name="id">El ID de la <see cref="Cancha"/> a modificar.</param>
        /// <remarks>
        public void CambiarHabilitado(Guid id)
        {
            string sql = @"UPDATE DbCancha 
                   SET EstadoCancha = ~EstadoCancha 
                   WHERE IdCancha = @IdCancha";

            
            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdCancha", id)
            );
        }

        /// <summary>
        /// Obtiene una lista de todas las canchas HABILITADAS (<c>EstadoCancha = 1</c>).
        /// </summary>
        /// <returns>Una colección de <see cref="Cancha"/>.</returns>
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

        /// <summary>
        /// Obtiene una lista de TODAS las canchas, incluyendo las deshabilitadas.
        /// </summary>
        /// <returns>Una colección de <see cref="Cancha"/>.</returns>
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

        /// <summary>
        /// Obtiene una <see cref="Cancha"/> específica por su ID,
        /// independientemente de si está habilitada o no.
        /// </summary>
        /// <param name="id">El ID de la <see cref="Cancha"/>.</param>
        /// <returns>La <see cref="Cancha"/> encontrada, o <c>null</c>.</returns>
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

        /// <summary>
        /// Actualiza un registro de <see cref="Cancha"/> existente.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Cancha"/> con los datos modificados.</param>
        /// <remarks>
        /// Al igual que Add, traduce <c>Deporte</c> a Guid, <c>DuracionXPartidoMin</c> a <see cref="TimeSpan"/>
        /// y <c>Precio</c> a string.
        /// </remarks>
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


        /// <summary>
        /// Método de ayuda (privado) para obtener el <see cref="Guid"/> (IdDeporte)
        /// de un deporte a partir de su string de descripción.
        /// </summary>
        /// <param name="descripcion">La descripción del deporte (ej: "Fútbol 5").</param>
        /// <returns>El <see cref="Guid"/> del deporte.</returns>
        /// <exception cref="InvalidOperationException">Si el deporte no existe en la tabla DbDeporte.</exception>
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
