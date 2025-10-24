using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DomainModel;
using DAL.Implementations.SqlServer.Helper;
using DAL.Implementations.SqlServer.Adapters;

namespace DAL.Implementations.SqlServer
{
    internal class FixtureRepository : SqlTransactRepository ,IFixtureRepository
    {

        public FixtureRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        private const string _sqlSelect = @"SELECT 
                f.IdFixture, f.IdCompeticion, e.Descripcion, f.Resultado, f.Horario
            FROM DbFixture f
            LEFT JOIN DbEstadoFixture e ON f.IdEstadoFixture = e.IdEstadoFixture";

        public void Add(Fixture entity)
        {

            Guid estadoId = GetEstadoFixtureId(entity.Estado);

            string sql = @"INSERT INTO DbFixture 
                           (IdFixture, IdCompeticion, IdEstadoFixture, Resultado, Horario)
                           VALUES
                           (@IdF, @IdC, @IdE, @Res, @Horario)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdF", entity.IdFixture),
                new SqlParameter("@IdC", entity.IdCompeticion),
                new SqlParameter("@IdE", estadoId),
                new SqlParameter("@Res", (object)entity.Resultado ?? DBNull.Value),
                new SqlParameter("@Horario", entity.Horario)
            );

            // Sincroniza la tabla hija
            SyncEquipos(entity);
        }

       

        public void CambiarHabilitado(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fixture> GetAll()
        {
            var list = new List<Fixture>();
            using (var reader = base.ExecuteReader(_sqlSelect, CommandType.Text, new SqlParameter()))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var fixture = FixtureAdapter.Current.Get(values);
                    PopulateEquipos(fixture); 
                    list.Add(fixture);
                }
            }
            return list;
        }

        public List<Fixture> GetByCompeticion(Competicion competicion)
        {
            var list = new List<Fixture>();
            string sql = $"{_sqlSelect} WHERE f.IdCompeticion = @IdC";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdC", competicion.IdCompeticion)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var fixture = FixtureAdapter.Current.Get(values);
                    PopulateEquipos(fixture);
                    
                    list.Add(fixture);
                }
            }
            return list;
        }

        public Fixture GetById(Guid id)
        {
            Fixture fixture = null;
            string sql = $"{_sqlSelect} WHERE f.IdFixture = @Id";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    fixture = FixtureAdapter.Current.Get(values);
                    PopulateEquipos(fixture);
                }
            }
            return fixture;
        }

        public List<Fixture> GetByTimeRange(DateTime dateTime)
        {
            var list = new List<Fixture>();
            
            string sql = $"{_sqlSelect} WHERE CONVERT(date, f.Horario) = CONVERT(date, @Fecha)";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Fecha", dateTime.Date)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var fixture = FixtureAdapter.Current.Get(values);
                    PopulateEquipos(fixture); 
                    list.Add(fixture);
                }
            }
            return list;
        }

        public void Update(Fixture entity)
        {
            Guid estadoId = GetEstadoFixtureId(entity.Estado);

            string sql = @"UPDATE DbFixture SET
                            IdCompeticion = @IdC,
                            IdEstadoFixture = @IdE,
                            Resultado = @Res,
                            Horario = @Horario
                           WHERE IdFixture = @IdF";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdC", entity.IdCompeticion),
                new SqlParameter("@IdE", estadoId),
                new SqlParameter("@Res", (object)entity.Resultado ?? DBNull.Value),
                new SqlParameter("@Horario", entity.Horario),
                new SqlParameter("@IdF", entity.IdFixture)
            );

            
            SyncEquipos(entity);
        }

        public void UpdateFecha(Fixture fixture)
        {
            // Este método es para el servicio "postergar"
            string sql = "UPDATE DbFixture SET Horario = @Horario WHERE IdFixture = @IdF";
            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Horario", fixture.Horario),
                new SqlParameter("@IdF", fixture.IdFixture)
            );
        }


        private Guid GetEstadoFixtureId(EstadoFixture estado)
        {
            string desc = estado.ToString();
            string sql = "SELECT IdEstadoFixture FROM DbEstadoFixture WHERE Descripcion = @Descripcion";
            object result = base.ExecuteScalar(sql, CommandType.Text, new SqlParameter("@Descripcion", desc));
            if (result == null || result == DBNull.Value)
                throw new InvalidOperationException($"El estado '{desc}' no fue encontrado en DbEstadoFixture.");
            return (Guid)result;
        }

        private void PopulateEquipos(Fixture fixture)
        {
            fixture.Equipos.Clear();
            string sql = "SELECT IdEquipo FROM DbFixtureEquipo WHERE IdFixture = @IdFixture";
            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdFixture", fixture.IdFixture)))
            {
                while (reader.Read())
                {
                    // Agrega stubs de Equipo
                    fixture.Equipos.Add(new Equipo { IdEquipo = (Guid)reader["IdEquipo"] });
                }
            }
        }


        private void SyncEquipos(Fixture fixture)
        {
            // 1. Borra los equipos viejos
            string sqlDelete = "DELETE FROM DbFixtureEquipo WHERE IdFixture = @IdFixture";
            base.ExecuteNonQuery(sqlDelete,CommandType.Text, new SqlParameter("@IdFixture", fixture.IdFixture));

            // 2. Inserta los nuevos (asume 2 equipos)
            string sqlInsert = "INSERT INTO DbFixtureEquipo (IdFixture, IdEquipo) VALUES (@IdF, @IdE)";
            foreach (var equipo in fixture.Equipos)
            {
                base.ExecuteNonQuery(sqlInsert, CommandType.Text, 
                    new SqlParameter("@IdF", fixture.IdFixture),
                    new SqlParameter("@IdE", equipo.IdEquipo)
                );
            }
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
