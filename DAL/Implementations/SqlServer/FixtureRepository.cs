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
    /// <summary>
    /// Repositorio SQL para gestionar las entidades <see cref="Fixture"/> (partidos del torneo).
    /// </summary>
    /// <remarks>
    /// Opera dentro de una transacción y conexión SQL existente (Unit of Work).
    /// </remarks>
    internal class FixtureRepository : SqlTransactRepository, IFixtureRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de conexión y transacción de una
        /// Unidad de Trabajo (Unit of Work) existente.
        /// </summary>
        /// <param name="context">La <see cref="SqlConnection"/> activa.</param>
        /// <param name="_transaction">La <see cref="SqlTransaction"/> activa.</param>
        public FixtureRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        private const string _sqlSelect = @"SELECT 
                f.IdFixture, f.IdCompeticion, e.Descripcion, f.Resultado, f.IdCanchaHorario
            FROM DbFixture f
            LEFT JOIN DbEstadoFixture e ON f.IdEstadoFixture = e.IdEstadoFixture";

        /// <summary>
        /// Agrega un nuevo <see cref="Fixture"/> (partido) a la base de datos.
        /// También sincroniza los equipos participantes en la tabla <c>DbFixtureEquipo</c>.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Fixture"/> a insertar.</param>
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

            // Sincroniza la tabla hija N:N (DbFixtureEquipo)
            SyncEquipos(entity);
        }



        /// <summary>
        /// (No implementado) Cambia el estado de habilitación de un fixture.
        /// </summary>
        public void CambiarHabilitado(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene una lista de todos los fixtures (partidos) en el sistema.
        /// </summary>
        /// <returns>Una colección de <see cref="Fixture"/>.</returns>
        /// <remarks>
        /// ¡ADVERTENCIA DE RENDIMIENTO! Este método sufre del problema N+1.
        /// Ejecuta 1 consulta para traer los fixtures, y luego N consultas
        /// (una por cada fixture) al llamar a <c>PopulateEquipos</c>.
        /// </remarks>
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
                PopulateEquipos(fixture); // N+1 consultas
            }
            return list;
        }

        /// <summary>
        /// Obtiene todos los partidos de una competición específica.
        /// </summary>
        /// <param name="competicion">La <see cref="Competicion"/> a consultar.</param>
        /// <returns>Una lista de <see cref="Fixture"/> (partidos).</returns>
        /// <remarks>
        /// ¡ADVERTENCIA DE RENDIMIENTO! Este método sufre del problema N+1.
        /// Ejecuta 1 consulta para traer los fixtures, y luego N consultas
        /// (una por cada fixture) al llamar a <c>PopulateEquipos</c>.
        /// </remarks>
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
                PopulateEquipos(fixture); // N+1 consultas
            }
            return list;
        }

        /// <summary>
        /// Obtiene un <see cref="Fixture"/> (partido) específico por su ID.
        /// </summary>
        /// <param name="id">El ID (PK) del fixture.</param>
        /// <returns>El <see cref="Fixture"/> encontrado (con equipos), o <c>null</c>.</returns>
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
                PopulateEquipos(fixture); // Carga los equipos (Consulta 2)
            }
            return fixture;
        }

        /// <summary>
        /// Obtiene todos los partidos programados para una fecha específica.
        /// </summary>
        /// <param name="dateTime">La fecha a consultar.</param>
        /// <returns>Una lista de <see cref="Fixture"/> (partidos).</returns>
        public List<Fixture> GetByTimeRange(DateTime dateTime)
        {
            var list = new List<Fixture>();

            string sql = $"{_sqlSelect} " +
                         "INNER JOIN [DbCancha Horario] ch ON f.IdCanchaHorario = ch.[IdCancha-Horario] " +
                         "WHERE CONVERT(date, ch.Horario) = CONVERT(date, @Fecha)";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Fecha", dateTime.Date)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var fixture = FixtureAdapter.Current.Get(values);
                    PopulateEquipos(fixture); // N+1 consultas
                    list.Add(fixture);
                }
            }
            return list;
        }

        /// <summary>
        /// Actualiza un <see cref="Fixture"/> (partido) existente.
        /// También sincroniza (borra e inserta) los equipos en <c>DbFixtureEquipo</c>.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Fixture"/> con los datos modificados.</param>
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

            // Sincroniza la tabla hija N:N (DbFixtureEquipo)
            SyncEquipos(entity);
        }


        /// <summary>
        /// Método de ayuda (privado) para obtener el <see cref="Guid"/> (IdEstadoFixture)
        /// de un <see cref="EstadoFixture"/> (enum) a partir de su string.
        /// </summary>
        /// <param name="estado">El enum <see cref="EstadoFixture"/>.</param>
        /// <returns>El <see cref="Guid"/> del estado.</returns>
        /// <exception cref="InvalidOperationException">Si el estado no existe en la tabla DbEstadoFixture.</exception>
        private Guid GetEstadoFixtureId(EstadoFixture estado)
        {
            string desc = estado.ToString();
            string sql = "SELECT IdEstadoFixture FROM DbEstadoFixture WHERE Descripcion = @Descripcion";
            object result = base.ExecuteScalar(sql, CommandType.Text, new SqlParameter("@Descripcion", desc));
            if (result == null || result == DBNull.Value)
                throw new InvalidOperationException($"El estado '{desc}' no fue encontrado en DbEstadoFixture.");
            return (Guid)result;
        }

        /// <summary>
        /// Método de ayuda (privado) que carga la lista de equipos (solo IDs)
        /// para un partido (fixture) dado.
        /// </summary>
        /// <param name="fixture">El <see cref="Fixture"/> al que se le cargarán los equipos.</param>
        private void PopulateEquipos(Fixture fixture)
        {
            fixture.Equipos.Clear();
            string sql = "SELECT IdEquipo FROM DbFixtureEquipo WHERE IdFixture = @IdFixture";
            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdFixture", fixture.IdFixture)))
            {
                while (reader.Read())
                {
                    // Agrega stubs de Equipo (solo el ID)
                    fixture.Equipos.Add(new Equipo { IdEquipo = (Guid)reader["IdEquipo"] });
                }
            }
        }

        /// <summary>
        /// Método de ayuda (privado) que sincroniza (borrando y re-insertando)
        /// los equipos de un partido en la tabla <c>DbFixtureEquipo</c>.
        /// </summary>
        /// <param name="fixture">El <see cref="Fixture"/> con la lista de <see cref="Equipo"/> a sincronizar.</param>
        private void SyncEquipos(Fixture fixture)
        {
            // 1. Borra todos los equipos existentes para este fixture
            string sqlDelete = "DELETE FROM DbFixtureEquipo WHERE IdFixture = @IdFixture";
            base.ExecuteNonQuery(sqlDelete, CommandType.Text, new SqlParameter("@IdFixture", fixture.IdFixture));

            // 2. Re-inserta los equipos de la lista
            string sqlInsert = "INSERT INTO DbFixtureEquipo (IdFixture, IdEquipo) VALUES (@IdF, @IdE)";
            foreach (var equipo in fixture.Equipos)
            {
                base.ExecuteNonQuery(sqlInsert, CommandType.Text,
                    new SqlParameter("@IdF", fixture.IdFixture),
                    new SqlParameter("@IdE", equipo.IdEquipo)
                );
            }
        }

        /// <summary>
        /// (No implementado) Elimina un fixture.
        /// </summary>
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene una lista de todos los partidos pendientes (a futuro)
        /// de una competición, ordenados por fecha.
        /// </summary>
        /// <param name="idCompeticion">El ID de la <see cref="Competicion"/>.</param>
        /// <returns>Una lista de <see cref="Fixture"/> (partidos).</returns>
        public IEnumerable<Fixture> GetByCompeticionPendientes(Guid idCompeticion)
        {
            string sql = $"{_sqlSelect} " +
           "INNER JOIN [DbCancha Horario] ch ON f.IdCanchaHorario = ch.[IdCancha-Horario] " +
           "WHERE f.IdCompeticion = @IdCompeticion " +
           "AND ch.Horario > GETDATE() " + // Filtra por futuro
           "AND f.IdEstadoFixture = @EstadoPendiente " + // Filtra por pendiente
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
                PopulateEquipos(fixture); // N+1 consultas
            }

            return lista;
        }

        /// <summary>
        /// Cuenta el número de partidos en estado 'Pendiente' para una competición.
        /// </summary>
        /// <param name="idCompeticion">El ID de la <see cref="Competicion"/>.</param>
        /// <returns>El número de partidos pendientes.</returns>
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