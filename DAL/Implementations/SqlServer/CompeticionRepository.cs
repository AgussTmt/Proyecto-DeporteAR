using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Implementations.SqlServer.Adapters;
using DAL.Implementations.SqlServer.Helper;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class CompeticionRepository : SqlTransactRepository, ICompeticionRepository
    {
        public CompeticionRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        private const string _sqlSelect = @"SELECT 
                c.IdCompeticion, c.Cupos, c.Estado, c.Cupos_min, c.FechaCreacion, c.FechaInicio, 
                c.FranjaHoraria, c.Frecuencia, c.Nombre, c.PrecioInscripcion,
                f.Descripcion AS FormatoDescripcion,
                k.IdCancha,
                d.Descripcion AS DeporteDescripcion
            FROM DbCompeticion c
            LEFT JOIN DbFormato f ON c.IdFormato = f.IdFormato
            LEFT JOIN DbCancha k ON c.IdCancha = k.IdCancha
            LEFT JOIN DbDeporte d ON k.IdDeporte = d.IdDeporte";

        public void Add(Competicion entity)
        {
            Guid formatoId = GetFormatoId(entity.Formato);
            string estado = entity.Estado.ToString();

            string sql = @"INSERT INTO DbCompeticion 
                           (IdCompeticion, Cupos, Cupos_min, Estado, FechaCreacion, FechaInicio, IdFormato, FranjaHoraria, Frecuencia, Nombre, PrecioInscripcion, IdCancha)
                           VALUES
                           (@Id, @Cupos, 0, @Estado, @FechaC, @FechaI, @IdFormato, @Franja, @Frec, @Nombre, @Precio, @IdCancha)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Id", entity.IdCompeticion),
                new SqlParameter("@Cupos", entity.Cupos),
                new SqlParameter("@Estado", estado),
                new SqlParameter("@FechaC", entity.FechaCreacion),
                new SqlParameter("@FechaI", entity.FechaInicio),
                new SqlParameter("@IdFormato", formatoId),
                new SqlParameter("@Franja", (object)entity.FranjaHoraria ?? DBNull.Value),
                new SqlParameter("@Frec", entity.Frecuencia),
                new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                new SqlParameter("@Precio", entity.Precio),
                new SqlParameter("@IdCancha", entity.canchaAsignada.IdCancha)
            );
        }

        public void AddEquipo(Guid idCompeticion, Guid idEquipo)
        {
            string sql = "INSERT INTO DbEquipoCompeticion (IdCompeticion, IdEquipo) VALUES (@IdComp, @IdEquipo)";
            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdComp", idCompeticion),
                new SqlParameter("@IdEquipo", idEquipo)
            );
        }

        public void CambiarHabilitado(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Competicion> GetAll()
        {
                var list = new List<Competicion>();
                using (var reader = base.ExecuteReader(_sqlSelect, CommandType.Text, new SqlParameter()))
                {
                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        reader.GetValues(values);
                        list.Add(CompeticionAdapter.Current.Get(values));
                    }
                }

                // Llenamos las listas de equipos (problema N+1, pero sigue el patrón)
                foreach (var c in list)
                {
                    PopulateEquipos(c);
                }
                return list;
            }

        public List<Competicion> GetByClient(Cliente cliente)
        {
            string sql = $@"SELECT DISTINCT c.*
                            FROM ({_sqlSelect}) c
                            JOIN DbEquipoCompeticion ec ON c.IdCompeticion = ec.IdCompeticion
                            JOIN DbEquipo e ON ec.IdEquipo = e.IdEquipo
                            WHERE e.IdCliente = @IdCliente";

            var list = new List<Competicion>();
            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdCliente", cliente.IdCliente)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(CompeticionAdapter.Current.Get(values));
                }
            }
            foreach (var c in list)
            {
                PopulateEquipos(c);
            }
            return list;
        }

        public Competicion GetById(Guid id)
        {
            Competicion competicion = null;
            string sql = $"{_sqlSelect} WHERE c.IdCompeticion = @Id";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    competicion = CompeticionAdapter.Current.Get(values);

                    // Llenamos la lista de equipos
                    PopulateEquipos(competicion);
                }
            }
            return competicion;
        }

        public List<Competicion> GetByTimeAndCancha(Competicion competicion)
        {
            string sql = $"{_sqlSelect} WHERE c.IdCancha = @IdCancha AND c.FranjaHoraria = @Franja";
            var list = new List<Competicion>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text,
                new SqlParameter("@IdCancha", competicion.canchaAsignada.IdCancha),
                new SqlParameter("@Franja", competicion.FranjaHoraria)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(CompeticionAdapter.Current.Get(values));
                }
            }
            foreach (var c in list)
            {
                PopulateEquipos(c);
            }
            return list;
        }

        public List<Competicion> GetWithVacancies()
        {
            string sql = $@"SELECT c.*
                FROM ({_sqlSelect}) c
                LEFT JOIN (
                    SELECT IdCompeticion, COUNT(*) AS EquiposInscriptos
                    FROM DbEquipoCompeticion
                    GROUP BY IdCompeticion
                ) ec ON c.IdCompeticion = ec.IdCompeticion
                WHERE c.Cupos > ISNULL(ec.EquiposInscriptos, 0)";

            var list = new List<Competicion>();
            using (var reader = base.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(CompeticionAdapter.Current.Get(values));
                }
            }
            // (Opcional) Llenar equipos para estas también
            foreach (var c in list)
            {
                PopulateEquipos(c);
            }
            return list;
        }

        public void RemoveEquipo(Guid idCompeticion, Guid idEquipo)
        {
            string sql = "DELETE FROM DbEquipoCompeticion WHERE IdCompeticion = @IdComp AND IdEquipo = @IdEquipo";
            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdComp", idCompeticion),
                new SqlParameter("@IdEquipo", idEquipo)
            );
        }

        public void Update(Competicion entity)
        {
                Guid formatoId = GetFormatoId(entity.Formato);
                string estado = entity.Estado.ToString();

                string sql = @"UPDATE DbCompeticion SET
                            Cupos = @Cupos,
                            Estado = @Estado,
                            FechaInicio = @FechaI,
                            IdFormato = @IdFormato,
                            FranjaHoraria = @Franja,
                            Frecuencia = @Frec,
                            Nombre = @Nombre,
                            PrecioInscripcion = @Precio,
                            IdCancha = @IdCancha
                           WHERE IdCompeticion = @Id";

                base.ExecuteNonQuery(sql, CommandType.Text,
                    new SqlParameter("@Cupos", entity.Cupos),
                    new SqlParameter("@Estado", estado),
                    new SqlParameter("@FechaI", entity.FechaInicio),
                    new SqlParameter("@IdFormato", formatoId),
                    new SqlParameter("@Franja", (object)entity.FranjaHoraria ?? DBNull.Value),
                    new SqlParameter("@Frec", entity.Frecuencia),
                    new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                    new SqlParameter("@Precio", entity.Precio),
                    new SqlParameter("@IdCancha", entity.canchaAsignada.IdCancha),
                    new SqlParameter("@Id", entity.IdCompeticion)
                );
            }
        

        private Guid GetFormatoId(FormatoEnum formato)
        {
            string desc = formato.ToString();
            string sql = "SELECT IdFormato FROM DbFormato WHERE Descripcion = @Descripcion";
            object result = base.ExecuteScalar(sql, CommandType.Text, new SqlParameter("@Descripcion", desc));
            if (result == null || result == DBNull.Value)
                throw new InvalidOperationException($"El formato '{desc}' no fue encontrado en DbFormato.");
            return (Guid)result;
        }


        private void PopulateEquipos(Competicion competicion)
        {
            string sql = "SELECT IdEquipo FROM DbEquipoCompeticion WHERE IdCompeticion = @IdComp";
            competicion.ListaEquipos = new List<Equipo>();
            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdComp", competicion.IdCompeticion)))
            {
                while (reader.Read())
                {
                    competicion.ListaEquipos.Add(new Equipo { IdEquipo = (Guid)reader["IdEquipo"] });
                }
            }
        }
    }
}
