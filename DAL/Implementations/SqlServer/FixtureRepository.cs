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
                f.IdFixture, f.IdCompeticion, e.Descripcion, f.Resultado, f.IdCanchaHorario
            FROM DbFixture f
            LEFT JOIN DbEstadoFixture e ON f.IdEstadoFixture = e.IdEstadoFixture";

        public void Add(Fixture entity)
        {

            Guid estadoId = GetEstadoFixtureId(entity.Estado);

            string sql = @"INSERT INTO DbFixture 
                           (IdFixture, IdCompeticion, IdEstadoFixture, Resultado, IdCanchaHorario)
                           VALUES
                           (@IdF, @IdC, @IdE, @Res, @IdCanchaHorario)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdF", entity.IdFixture),
                new SqlParameter("@IdC", entity.IdCompeticion),
                new SqlParameter("@IdE", estadoId),
                new SqlParameter("@Res", (object)entity.Resultado ?? DBNull.Value),
                new SqlParameter("@IdCanchaHorario", entity.CanchaHorario.IdCanchaHorario)
            );

            //Tabla hija
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
                    list.Add(fixture);
                }
            }
            foreach (var fixture in list)
            {
                PopulateEquipos(fixture);
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
                    list.Add(fixture);
                }
                
            }
            foreach (var fixture in list)
            {
                PopulateEquipos(fixture);
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
                    
                }
            }
            if (fixture != null)
            {
                PopulateEquipos(fixture);
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
                            IdCanchaHorario = @IdCanchaHorario
                           WHERE IdFixture = @IdF";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdC", entity.IdCompeticion),
                new SqlParameter("@IdE", estadoId),
                new SqlParameter("@Res", (object)entity.Resultado ?? DBNull.Value),
                new SqlParameter("@IdCanchaHorario", entity.CanchaHorario.IdCanchaHorario),
                new SqlParameter("@IdF", entity.IdFixture)
            );

            
            SyncEquipos(entity);
        }

        public void UpdateFecha(Fixture fixture)
        {
            return;
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
            string sqlDelete = "DELETE FROM DbFixtureEquipo WHERE IdFixture = @IdFixture";
            base.ExecuteNonQuery(sqlDelete,CommandType.Text, new SqlParameter("@IdFixture", fixture.IdFixture));

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

        public IEnumerable<Fixture> GetByCompeticionPendientes(Guid idCompeticion)
        {
            string sql = $"{_sqlSelect} " +
           "INNER JOIN [DbCancha Horario] ch ON f.IdCanchaHorario = ch.[IdCancha-Horario] " +
           "WHERE f.IdCompeticion = @IdCompeticion " +
           "AND ch.Horario > GETDATE() " +
           "AND f.IdEstadoFixture = @EstadoPendiente " +
           "ORDER BY ch.Horario ASC";

            Guid idEstadoPendiente = GetEstadoFixtureId(EstadoFixture.Pendiente);
            var lista = new List<Fixture>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text,
                new SqlParameter("@IdCompeticion", idCompeticion),
                new SqlParameter("@EstadoPendiente", idEstadoPendiente)
            ))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var fixture = FixtureAdapter.Current.Get(values);
                    lista.Add(fixture);
                }
            } 

            foreach (var fixture in lista)
            {
                PopulateEquipos(fixture);
            }

            return lista;
        }

        public int CountPartidosPendientes(Guid idCompeticion)
        {
            Guid idEstadoPendiente = GetEstadoFixtureId(EstadoFixture.Pendiente);
            string sql = @"SELECT COUNT(*) 
                   FROM DbFixture 
                   WHERE IdCompeticion = @IdComp 
                   AND IdEstadoFixture = @IdEstadoPendiente";

            object result = base.ExecuteScalar(sql, CommandType.Text,
                new SqlParameter("@IdComp", idCompeticion),
                new SqlParameter("@IdEstadoPendiente", idEstadoPendiente));

            if (result != null && result != DBNull.Value)
            {
                return (int)result;
            }
            return 0;
        }
    }
}
