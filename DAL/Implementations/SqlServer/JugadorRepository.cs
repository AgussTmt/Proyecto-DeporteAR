using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using DAL.Implementations.SqlServer.Adapters;
using DAL.Implementations.SqlServer.Helper;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    /// <summary>
    /// Repositorio SQL para gestionar las entidades <see cref="Jugador"/>.
    /// Provee métodos de ABM (CRUD) y maneja las sub-entidades
    /// (Puntuacion y Sanciones) mediante sincronización.
    /// </summary>
    /// <remarks>
    /// Opera dentro de una transacción y conexión SQL existente (Unit of Work).
    /// </remarks>
    internal class JugadorRepository : SqlTransactRepository, IJugadorRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de conexión y transacción de una
        /// Unidad de Trabajo (Unit of Work) existente.
        /// </summary>
        /// <param name="context">La <see cref="SqlConnection"/> activa.</param>
        /// <param name="_transaction">La <see cref="SqlTransaction"/> activa.</param>
        public JugadorRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        private const string _sqlSelect = @"SELECT 
                j.IdJugador, j.IdEquipo, j.Nombre, j.PartidosJugados, j.Mvp, j.Apellido, j.Habilitado,
                e.Nombre as NombreEquipo
            FROM DbJugador j
            LEFT JOIN DbEquipo e ON j.IdEquipo = e.IdEquipo";

        /// <summary>
        /// Agrega un nuevo <see cref="Jugador"/> a la base de datos.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Jugador"/> a insertar.</param>
        public void Add(Jugador entity)
        {
            string sql = @"INSERT INTO DbJugador 
                           (IdJugador, IdEquipo, Nombre, PartidosJugados, Mvp, Apellido, Habilitado)
                           VALUES
                           (@IdJ, @IdE, @Nombre, @PJ, @Mvp, @Apellido, 1)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdJ", entity.Idjugador),
                new SqlParameter("@IdE", (object)entity.IdEquipo ?? DBNull.Value),
                new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                new SqlParameter("@PJ", entity.PartidosJugados),
                new SqlParameter("@Mvp", entity.CantMvp),
                new SqlParameter("@Apellido", (object)entity.Apellido ?? DBNull.Value)
            );

        }

        /// <summary>
        /// Obtiene una lista de todos los jugadores HABILITADOS (<c>Habilitado = 1</c>).
        /// </summary>
        /// <returns>Una colección de <see cref="Jugador"/>.</returns>
        public IEnumerable<Jugador> GetAll()
        {
            // 1. El SQL con 3 consultas. El orden importa.
            // (Tu _sqlSelect ya hace el LEFT JOIN a Equipo, ¡perfecto!)
            string sql = $@"
                {_sqlSelect} 
                WHERE j.Habilitado = 1;

                SELECT p.IdJugador, p.Descripcion, p.Cantidad 
                FROM DbPuntuacion p
                INNER JOIN DbJugador j ON p.IdJugador = j.Idjugador
                WHERE j.Habilitado = 1;

                SELECT s.IdJugador, s.Descripcion, s.Cantidad 
                FROM DbSanciones s
                INNER JOIN DbJugador j ON s.IdJugador = j.Idjugador
                WHERE j.Habilitado = 1;
            ";

            // Usamos un Diccionario para "agrupar" los datos en memoria.
            // Es la forma más rápida de encontrar a un jugador por su ID.
            var jugadoresDictionary = new Dictionary<Guid, Jugador>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text))
            {
                // --- PRIMER RESULT SET (Jugadores) ---
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var jugador = JugadorAdapter.Current.Get(values);

                    // ¡IMPORTANTE! Inicializamos los diccionarios vacíos
                    jugador.Puntuacion = new Dictionary<string, int>();
                    jugador.Sanciones = new Dictionary<string, int>();

                    jugadoresDictionary.Add(jugador.Idjugador, jugador);
                }

                // --- SEGUNDO RESULT SET (Puntuaciones) ---
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var idJugador = (Guid)reader["IdJugador"];
                        var descripcion = reader["Descripcion"].ToString();
                        var cantidad = (int)reader["Cantidad"];

                        // Buscamos el jugador en el diccionario y le agregamos el stat
                        if (jugadoresDictionary.TryGetValue(idJugador, out var jugador))
                        {
                            jugador.Puntuacion.Add(descripcion, cantidad);
                        }
                    }
                }

                // --- TERCER RESULT SET (Sanciones) ---
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var idJugador = (Guid)reader["IdJugador"];
                        var descripcion = reader["Descripcion"].ToString();
                        var cantidad = (int)reader["Cantidad"];

                        // Buscamos el jugador en el diccionario y le agregamos la sanción
                        if (jugadoresDictionary.TryGetValue(idJugador, out var jugador))
                        {
                            jugador.Sanciones.Add(descripcion, cantidad);
                        }
                    }
                }
            }

            // Devolvemos solo los Valores (la lista de jugadores) del diccionario.
            return jugadoresDictionary.Values;
        }

        /// <summary>
        /// Obtiene un <see cref="Jugador"/> HABILITADO específico por su ID.
        /// </summary>
        /// <param name="id">El ID (PK) del jugador.</param>
        /// <returns>El <see cref="Jugador"/> encontrado (con Puntuacion/Sanciones), o <c>null</c>.</returns>
        public Jugador GetById(Guid id)
        {
            // 1. El SQL con 3 consultas, filtradas por ID.
            string sql = $@"
                {_sqlSelect} 
                WHERE j.IdJugador = @Id AND j.Habilitado = 1;

                SELECT p.IdJugador, p.Descripcion, p.Cantidad 
                FROM DbPuntuacion p
                WHERE p.IdJugador = @Id;

                SELECT s.IdJugador, s.Descripcion, s.Cantidad 
                FROM DbSanciones s
                WHERE s.IdJugador = @Id;
            ";

            Jugador jugador = null;

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Id", id)))
            {
                // --- PRIMER RESULT SET (Jugador) ---
                if (reader.Read()) // Usamos 'if' porque es 1 solo
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    jugador = JugadorAdapter.Current.Get(values);
                    jugador.Puntuacion = new Dictionary<string, int>();
                    jugador.Sanciones = new Dictionary<string, int>();
                }

                // Si no encontramos al jugador, no seguimos
                if (jugador == null)
                    return null;

                // --- SEGUNDO RESULT SET (Puntuaciones) ---
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        jugador.Puntuacion.Add(
                            reader["Descripcion"].ToString(),
                            (int)reader["Cantidad"]
                        );
                    }
                }

                // --- TERCER RESULT SET (Sanciones) ---
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        jugador.Sanciones.Add(
                            reader["Descripcion"].ToString(),
                            (int)reader["Cantidad"]
                        );
                    }
                }
            }
            return jugador;
        }

        /// <summary>
        /// Actualiza un <see cref="Jugador"/> existente en la base de datos.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Jugador"/> con los datos modificados.</param>
        public void Update(Jugador entity)
        {
            string sql = @"UPDATE DbJugador SET
                            IdEquipo = @IdE,
                            Nombre = @Nombre,
                            PartidosJugados = @PJ,
                            Mvp = @Mvp,
                            Apellido = @Apellido,
                            Habilitado = @Habilitado
                           WHERE IdJugador = @IdJ";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdE", (object)entity.IdEquipo ?? DBNull.Value),
                new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                new SqlParameter("@PJ", entity.PartidosJugados),
                new SqlParameter("@Mvp", entity.CantMvp),
                new SqlParameter("@Apellido", (object)entity.Apellido ?? DBNull.Value),
                new SqlParameter("@Habilitado", entity.Habilitado),
                new SqlParameter("@IdJ", entity.Idjugador)
            );

        }



        /// <summary>
        /// Invierte (toggle) el estado de Habilitado/Deshabilitado de un jugador.
        /// </summary>
        /// <param name="id">El ID (PK) del jugador.</param>
        public void CambiarHabilitado(Guid id)
        {
            string sql = @"UPDATE DbJugador 
                   SET Habilitado = ~Habilitado 
                   WHERE IdJugador = @IdJugador";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdJugador", id)
            );
        }

        /// <summary>
        /// Obtiene una lista de todos los jugadores HABILITADOS de un equipo específico.
        /// </summary>
        /// <param name="idEquipo">El ID (PK) del <see cref="Equipo"/>.</param>
        /// <returns>Una colección de <see cref="Jugador"/>.</returns>
        public IEnumerable<Jugador> GetByEquipo(Guid idEquipo)
        {
            string sql = $@"
                {_sqlSelect} 
                WHERE j.IdEquipo = @IdEquipo AND j.Habilitado = 1;

                SELECT p.IdJugador, p.Descripcion, p.Cantidad 
                FROM DbPuntuacion p
                INNER JOIN DbJugador j ON p.IdJugador = j.Idjugador
                WHERE j.IdEquipo = @IdEquipo AND j.Habilitado = 1;

                SELECT s.IdJugador, s.Descripcion, s.Cantidad 
                FROM DbSanciones s
                INNER JOIN DbJugador j ON s.IdJugador = j.Idjugador
                WHERE j.IdEquipo = @IdEquipo AND j.Habilitado = 1;
            ";

            var jugadoresDictionary = new Dictionary<Guid, Jugador>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdEquipo", idEquipo)))
            {
                // --- PRIMER RESULT SET (Jugadores) ---
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var jugador = JugadorAdapter.Current.Get(values);
                    jugador.Puntuacion = new Dictionary<string, int>();
                    jugador.Sanciones = new Dictionary<string, int>();
                    jugadoresDictionary.Add(jugador.Idjugador, jugador);
                }

                // --- SEGUNDO RESULT SET (Puntuaciones) ---
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var idJugador = (Guid)reader["IdJugador"];
                        if (jugadoresDictionary.TryGetValue(idJugador, out var jugador))
                        {
                            jugador.Puntuacion.Add(
                                reader["Descripcion"].ToString(),
                                (int)reader["Cantidad"]
                            );
                        }
                    }
                }

                // --- TERCER RESULT SET (Sanciones) ---
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var idJugador = (Guid)reader["IdJugador"];
                        if (jugadoresDictionary.TryGetValue(idJugador, out var jugador))
                        {
                            jugador.Sanciones.Add(
                                reader["Descripcion"].ToString(),
                                (int)reader["Cantidad"]
                            );
                        }
                    }
                }
            }
            return jugadoresDictionary.Values;
        }

        /// <summary>
        /// Obtiene una lista de todos los jugadores HABILITADOS que no tienen equipo
        /// (agentes libres).
        /// </summary>
        /// <returns>Una colección de <see cref="Jugador"/>.</returns>
        public List<Jugador> GetSinEquipo()
        {
            string sql = $@"
                {_sqlSelect} 
                WHERE j.IdEquipo IS NULL AND j.Habilitado = 1;

                SELECT p.IdJugador, p.Descripcion, p.Cantidad 
                FROM DbPuntuacion p
                INNER JOIN DbJugador j ON p.IdJugador = j.Idjugador
                WHERE j.IdEquipo IS NULL AND j.Habilitado = 1;

                SELECT s.IdJugador, s.Descripcion, s.Cantidad 
                FROM DbSanciones s
                INNER JOIN DbJugador j ON s.IdJugador = j.Idjugador
                WHERE j.IdEquipo IS NULL AND j.Habilitado = 1;
            ";

            var jugadoresDictionary = new Dictionary<Guid, Jugador>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text))
            {
                // --- PRIMER RESULT SET (Jugadores) ---
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var jugador = JugadorAdapter.Current.Get(values);
                    jugador.Puntuacion = new Dictionary<string, int>();
                    jugador.Sanciones = new Dictionary<string, int>();
                    jugadoresDictionary.Add(jugador.Idjugador, jugador);
                }

                // --- SEGUNDO RESULT SET (Puntuaciones) ---
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var idJugador = (Guid)reader["IdJugador"];
                        if (jugadoresDictionary.TryGetValue(idJugador, out var jugador))
                        {
                            jugador.Puntuacion.Add(
                                reader["Descripcion"].ToString(),
                                (int)reader["Cantidad"]
                            );
                        }
                    }
                }

                // --- TERCER RESULT SET (Sanciones) ---
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var idJugador = (Guid)reader["IdJugador"];
                        if (jugadoresDictionary.TryGetValue(idJugador, out var jugador))
                        {
                            jugador.Sanciones.Add(
                                reader["Descripcion"].ToString(),
                                (int)reader["Cantidad"]
                            );
                        }
                    }
                }
            }
            return jugadoresDictionary.Values.ToList();
        }

        /// <summary>
        /// Obtiene una lista de TODOS los jugadores (habilitados y deshabilitados).
        /// </summary>
        /// <returns>Una colección de <see cref="Jugador"/>.</returns>
        public IEnumerable<Jugador> GetAllIncludingDisabled()
        {
            // 1. El SQL (sin el WHERE Habilitado = 1)
            string sql = $@"
                {_sqlSelect};

                SELECT p.IdJugador, p.Descripcion, p.Cantidad 
                FROM DbPuntuacion p;

                SELECT s.IdJugador, s.Descripcion, s.Cantidad 
                FROM DbSanciones s;
            ";

            var jugadoresDictionary = new Dictionary<Guid, Jugador>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text))
            {
                // --- PRIMER RESULT SET (Jugadores) ---
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var jugador = JugadorAdapter.Current.Get(values);
                    jugador.Puntuacion = new Dictionary<string, int>();
                    jugador.Sanciones = new Dictionary<string, int>();
                    jugadoresDictionary.Add(jugador.Idjugador, jugador);
                }

                // --- SEGUNDO RESULT SET (Puntuaciones) ---
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var idJugador = (Guid)reader["IdJugador"];
                        var descripcion = reader["Descripcion"].ToString();
                        var cantidad = (int)reader["Cantidad"];
                        if (jugadoresDictionary.TryGetValue(idJugador, out var jugador))
                        {
                            jugador.Puntuacion.Add(descripcion, cantidad);
                        }
                    }
                }

                // --- TERCER RESULT SET (Sanciones) ---
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var idJugador = (Guid)reader["IdJugador"];
                        var descripcion = reader["Descripcion"].ToString();
                        var cantidad = (int)reader["Cantidad"];
                        if (jugadoresDictionary.TryGetValue(idJugador, out var jugador))
                        {
                            jugador.Sanciones.Add(descripcion, cantidad);
                        }
                    }
                }
            }

            return jugadoresDictionary.Values;
        }

        // <summary>
        /// Agrega o actualiza una estadística específica (Puntuacion) para un jugador
        /// usando un comando MERGE (UPSERT).
        /// </summary>
        /// <param name="idJugador">El ID del jugador.</param>
        /// <param name="tipo">La clave (ej: "Goles").</param>
        /// <param name="cantidadASumar">La cantidad a sumar (ej: 1).</param>
        /// <remarks>
        /// Esta es la forma quirúrgica y eficiente. En lugar de leer y
        /// sincronizar toda la colección, hace 1 sola operación en la BD.
        /// </remarks>
        public void AddOrUpdatePuntuacionStat(Guid idJugador, string tipo, int cantidadASumar)
        {
            string sqlMerge = @"
                
                MERGE INTO DbPuntuacion AS T(Target)
                USING(
                    --Esta es la data 'nueva' que queremos meter
                    VALUES(@IdJugador, @Desc, @Cant)
                ) AS S(Source) (IdJugador, Descripcion, Cantidad)
                ON T.IdJugador = S.IdJugador AND T.Descripcion = S.Descripcion

                WHEN MATCHED THEN
                    UPDATE SET T.Cantidad = T.Cantidad + @Cant

                WHEN NOT MATCHED BY TARGET THEN
                    INSERT(IdPuntuacion, IdJugador, Descripcion, Cantidad)
                    VALUES(NEWID(), @IdJugador, @Desc, @Cant); ";

            base.ExecuteNonQuery(sqlMerge, CommandType.Text,
                new SqlParameter("@IdJugador", idJugador),
                new SqlParameter("@Desc", tipo),
                new SqlParameter("@Cant", cantidadASumar)
            );
        }

        /// <summary>
        /// Agrega o actualiza una Sanción específica para un jugador
        /// usando un comando MERGE (UPSERT).
        /// </summary>
        /// <param name="idJugador">El ID del jugador.</param>
        /// <param name="tipo">La clave (ej: "Amarillas").</param>
        /// <param name="cantidadASumar">La cantidad a sumar (ej: 1).</param>
        public void AddOrUpdateSancionStat(Guid idJugador, string tipo, int cantidadASumar)
        {
            string sqlMerge = @"
                MERGE INTO DbSanciones AS T
                USING (
                    VALUES (@IdJugador, @Desc, @Cant)
                ) AS S (IdJugador, Descripcion, Cantidad)
                ON T.IdJugador = S.IdJugador AND T.Descripcion = S.Descripcion
                
                WHEN MATCHED THEN
                    UPDATE SET T.Cantidad = T.Cantidad + @Cant
                
                WHEN NOT MATCHED BY TARGET THEN
                    INSERT (IdSancion, IdJugador, Descripcion, Cantidad)
                    VALUES (NEWID(), @IdJugador, @Desc, @Cant);";

            base.ExecuteNonQuery(sqlMerge, CommandType.Text,
                new SqlParameter("@IdJugador", idJugador),
                new SqlParameter("@Desc", tipo),
                new SqlParameter("@Cant", cantidadASumar)
            );
        }
    }
}