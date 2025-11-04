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
    /// <summary>
    /// Repositorio SQL para gestionar las entidades <see cref="Equipo"/>.
    /// Provee métodos de ABM (CRUD) para los equipos de las competiciones.
    /// </summary>
    /// <remarks>
    /// Opera dentro de una transacción y conexión SQL existente (Unit of Work).
    /// </remarks>
    internal class EquipoRepository : SqlTransactRepository, IEquipoRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de conexión y transacción de una
        /// Unidad de Trabajo (Unit of Work) existente.
        /// </summary>
        /// <param name="context">La <see cref="SqlConnection"/> activa.</param>
        /// <param name="_transaction">La <see cref="SqlTransaction"/> activa.</param>
        public EquipoRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }


        private const string _sqlSelect = @"SELECT 
            e.IdEquipo, e.CantAusencias, e.FechaDeCreacion, e.Nombre,
            e.IdCliente, 
            ea.Descripcion AS EstadoProxPartido,
            e.Habilitado
        FROM DbEquipo e
        LEFT JOIN DbEstadoAsistencia ea ON e.IdEstadoAsistencia = ea.IdEstadoAsistencia
        ";

        /// <summary>
        /// Agrega un nuevo <see cref="Equipo"/> a la base de datos.
        /// </summary>
        /// <param name="equipo">La entidad <see cref="Equipo"/> a insertar.</param>
        public void Add(Equipo equipo)
        {
            Guid estadoId = GetEstadoAsistenciaId(equipo.EstadoProxPartido);

            string sql = @"INSERT INTO DbEquipo 
                       (IdEquipo, CantAusencias, IdCliente, IdEstadoAsistencia, FechaDeCreacion, Nombre, Habilitado)
                       VALUES
                       (@IdEquipo, @CantAusencias, @IdCliente, @IdEstadoAsistencia, @FechaDeCreacion, @Nombre, 1)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdEquipo", equipo.IdEquipo),
                new SqlParameter("@CantAusencias", equipo.CantAusencias),
                new SqlParameter("@IdCliente", (object)equipo.Capitan?.IdCliente ?? DBNull.Value),
                new SqlParameter("@IdEstadoAsistencia", estadoId),
                new SqlParameter("@FechaDeCreacion", equipo.FechaCreacion),
                new SqlParameter("@Nombre", (object)equipo.Nombre ?? DBNull.Value)
            );
        }


        /// <summary>
        /// Obtiene la lista de equipos inscritos en una competición específica,
        /// incluyendo la lista de jugadores de cada equipo.
        /// </summary>
        /// <param name="competicion">La <see cref="Competicion"/> a consultar.</param>
        /// <returns>Una lista de <see cref="Equipo"/> completos (con jugadores).</returns>
        public List<Equipo> GetByCompeticion(Competicion competicion)
        {
            string sql = $@"SELECT e.*
                    FROM ({_sqlSelect}) e
                    JOIN DbEquipoCompeticion ec ON e.IdEquipo = ec.IdEquipo
                    WHERE ec.IdCompeticion = @IdCompeticion";

            var listaEquipos = new List<Equipo>();


            using (var reader = base.ExecuteReader(sql, CommandType.Text,
                new SqlParameter("@IdCompeticion", competicion.IdCompeticion)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    listaEquipos.Add(EquipoAdapter.Current.Get(values));
                }
            }


            foreach (var equipo in listaEquipos)
            {
                PopulateJugadores(equipo); // N+1 consultas
            }


            return listaEquipos;
        }

        /// <summary>
        /// Actualiza un registro de <see cref="Equipo"/> existente en la base de datos.
        /// </summary>
        /// <param name="equipo">La entidad <see cref="Equipo"/> con los datos modificados.</param>
        public void Update(Equipo equipo)
        {
            Guid estadoId = GetEstadoAsistenciaId(equipo.EstadoProxPartido);

            string sql = @"UPDATE DbEquipo SET
                        CantAusencias = @CantAusencias,
                        IdCliente = @IdCliente,
                        IdEstadoAsistencia = @IdEstadoAsistencia,
                        Nombre = @Nombre,
                        Habilitado = @Habilitado
                       WHERE IdEquipo = @IdEquipo";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@CantAusencias", equipo.CantAusencias),
                new SqlParameter("@IdCliente", (object)equipo.Capitan?.IdCliente ?? DBNull.Value),
                new SqlParameter("@IdEstadoAsistencia", estadoId),
                new SqlParameter("@Nombre", (object)equipo.Nombre ?? DBNull.Value),
                new SqlParameter("@Habilitado", equipo.Habilitado),
                new SqlParameter("@IdEquipo", equipo.IdEquipo)
            );
        }


        /// <summary>
        /// Método de ayuda (privado) para obtener el <see cref="Guid"/> (IdEstadoAsistencia)
        /// de un <see cref="EstadoAsistencia"/> (enum) a partir de su string.
        /// </summary>
        /// <param name="estado">El enum <see cref="EstadoAsistencia"/>.</param>
        /// <returns>El <see cref="Guid"/> del estado.</returns>
        /// <exception cref="InvalidOperationException">Si el estado no existe en la tabla DbEstadoAsistencia.</exception>
        private Guid GetEstadoAsistenciaId(EstadoAsistencia estado)
        {
            string desc = estado.ToString();
            string sql = "SELECT IdEstadoAsistencia FROM DbEstadoAsistencia WHERE Descripcion = @Descripcion";
            object result = base.ExecuteScalar(sql, CommandType.Text, new SqlParameter("@Descripcion", desc));
            if (result == null || result == DBNull.Value)
                throw new InvalidOperationException($"El estado '{desc}' no fue encontrado en DbEstadoAsistencia.");
            return (Guid)result;
        }

        /// <summary>
        /// Método de ayuda (privado) que carga la lista de <see cref="Jugador"/>
        /// para un equipo dado.
        /// </summary>
        /// <param name="equipo">El <see cref="Equipo"/> al que se le cargarán los jugadores.</param>
        private void PopulateJugadores(Equipo equipo)
        {

            string sql = "SELECT IdJugador, IdEquipo, Nombre, PartidosJugados, Mvp, Apellido FROM DbJugador WHERE IdEquipo = @IdEquipo";
            equipo.Jugadores = new List<Jugador>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdEquipo", equipo.IdEquipo)))
            {
                while (reader.Read())
                {
                    equipo.Jugadores.Add(new Jugador
                    {
                        Idjugador = (Guid)reader["IdJugador"],
                        Nombre = reader["Nombre"]?.ToString(),
                        Apellido = reader["Apellido"]?.ToString(),
                        PartidosJugados = (int)reader["PartidosJugados"],
                        CantMvp = (int)reader["Mvp"],
                        IdEquipo = (Guid)reader["IdEquipo"]
                    });
                }
            }
        }

        /// <summary>
        /// Obtiene un <see cref="Equipo"/> específico por su ID,
        /// incluyendo su lista de jugadores.
        /// </summary>
        /// <param name="idEquipo">El ID (PK) del equipo.</param>
        /// <returns>El <see cref="Equipo"/> encontrado (con jugadores), o <c>null</c>.</returns>
        public Equipo GetById(Guid idEquipo)
        {
            Equipo equipo = null;
            string sql = $"{_sqlSelect} WHERE e.IdEquipo = @IdEquipo";


            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdEquipo", idEquipo)))
            {
                if (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    equipo = EquipoAdapter.Current.Get(values);
                }
            }


            if (equipo != null)
            {

                PopulateJugadores(equipo); // Carga los jugadores (Consulta 2)
            }
            return equipo;
        }

        /// <summary>
        /// Obtiene una lista de todos los equipos HABILITADOS (<c>Habilitado = 1</c>),
        /// incluyendo la lista de jugadores de cada equipo.
        /// </summary>
        /// <returns>Una colección de <see cref="Equipo"/>.</returns>
        public IEnumerable<Equipo> GetAll()
        {
            var list = new List<Equipo>();
            string sql = $"{_sqlSelect} WHERE e.Habilitado = 1";


            using (var reader = base.ExecuteReader(_sqlSelect, CommandType.Text)) // ¡OJO! No está usando el 'sql' filtrado
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(EquipoAdapter.Current.Get(values));
                }
            }
            foreach (var equipo in list)
            {
                PopulateJugadores(equipo); // N+1 consultas
            }
            return list;
        }

        /// <summary>
        /// Cambia el estado de Habilitado/Deshabilitado de un equipo.
        /// </summary>
        /// <param name="idEquipo">El ID del equipo a modificar.</param>
        /// <param name="habilitado">El nuevo estado (true o false).</param>
        public void CambiarHabilitado(Guid idEquipo, bool habilitado)
        {
            string sql = @"UPDATE DbEquipo SET
                            Habilitado = @Habilitado
                           WHERE IdEquipo = @IdEquipo";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Habilitado", habilitado),
                new SqlParameter("@IdEquipo", idEquipo)
            );
        }

        /// <summary>
        /// Obtiene una lista de TODOS los equipos (habilitados y deshabilitados),
        /// incluyendo la lista de jugadores de cada equipo.
        /// </summary>
        /// <returns>Una colección de <see cref="Equipo"/>.</returns>
        public IEnumerable<Equipo> GetAllIncludingDisabled()
        {
            var list = new List<Equipo>();

            string sql = _sqlSelect;

            using (var reader = base.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(EquipoAdapter.Current.Get(values));
                }
            }
            foreach (var equipo in list)
            {
                PopulateJugadores(equipo); // N+1 consultas
            }
            return list;
        }

        /// <summary>
        /// Obtiene una lista de todos los equipos habilitados que pertenecen a un capitán (Cliente).
        /// </summary>
        /// <param name="idCliente">El ID del <see cref="Cliente"/> (capitán).</param>
        /// <returns>Una lista de <see cref="Equipo"/>.</returns>
        public List<Equipo> GetByCapitan(Guid idCliente)
        {
            var list = new List<Equipo>();
            string sql = $"{_sqlSelect} WHERE e.IdCliente = @IdCliente AND e.Habilitado = 1";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdCliente", idCliente)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(EquipoAdapter.Current.Get(values));
                }
            }
            foreach (var equipo in list)
            {
                PopulateJugadores(equipo); 
            }
            return list;
        }
    }
}