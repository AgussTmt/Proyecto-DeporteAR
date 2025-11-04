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
        /// También sincroniza sus colecciones de Puntuacion y Sanciones (delete-then-insert).
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

            // Sincroniza las tablas hijas (delete-then-insert)
            SyncPuntuacion(entity);
            SyncSanciones(entity);
        }

        /// <summary>
        /// Obtiene una lista de todos los jugadores HABILITADOS (<c>Habilitado = 1</c>).
        /// </summary>
        /// <returns>Una colección de <see cref="Jugador"/>.</returns>
        public IEnumerable<Jugador> GetAll()
        {
            var lista = new List<Jugador>();

            string sql = $"{_sqlSelect} WHERE j.Habilitado = 1";


            using (var reader = base.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {

                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var jugador = JugadorAdapter.Current.Get(values);
                    lista.Add(jugador);
                }
            }

            foreach (var jugador in lista)
            {
                PopulatePuntuacion(jugador); // N+...
                PopulateSanciones(jugador); // ...N consultas
            }
            return lista;
        }

        /// <summary>
        /// Obtiene un <see cref="Jugador"/> HABILITADO específico por su ID.
        /// </summary>
        /// <param name="id">El ID (PK) del jugador.</param>
        /// <returns>El <see cref="Jugador"/> encontrado (con Puntuacion/Sanciones), o <c>null</c>.</returns>
        public Jugador GetById(Guid id)
        {
            Jugador jugador = null;
            string sql = $"{_sqlSelect} WHERE j.IdJugador = @Id AND j.Habilitado = 1";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    jugador = JugadorAdapter.Current.Get(values);
                }
            }
            if (jugador != null)
            {
                PopulatePuntuacion(jugador); // Consulta 2
                PopulateSanciones(jugador); // Consulta 3
            }
            return jugador;
        }

        /// <summary>
        /// Actualiza un <see cref="Jugador"/> existente en la base de datos.
        /// También sincroniza sus colecciones de Puntuacion y Sanciones (delete-then-insert).
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

            // Sincroniza las tablas hijas (delete-then-insert)
            SyncPuntuacion(entity);
            SyncSanciones(entity);
        }


        /// <summary>
        /// Método de ayuda (privado) que carga el diccionario <c>Puntuacion</c> de un jugador.
        /// </summary>
        /// <param name="jugador">El <see cref="Jugador"/> al que se le cargarán las puntuaciones.</param>
        /// <remarks>Esta es una de las causas del problema N+2.</remarks>
        private void PopulatePuntuacion(Jugador jugador)
        {
            jugador.Puntuacion.Clear();
            string sql = "SELECT Descripcion, Cantidad FROM DbPuntuacion WHERE IdJugador = @IdJugador";
            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdJugador", jugador.Idjugador)))
            {
                while (reader.Read())
                {
                    jugador.Puntuacion.Add(reader["Descripcion"].ToString(), (int)reader["Cantidad"]);
                }
            }
        }

        /// <summary>
        /// Método de ayuda (privado) que carga el diccionario <c>Sanciones</c> de un jugador.
        /// </summary>
        /// <param name="jugador">El <see cref="Jugador"/> al que se le cargarán las sanciones.</param>
        /// <remarks>Esta es una de las causas del problema N+2.</remarks>
        private void PopulateSanciones(Jugador jugador)
        {
            jugador.Sanciones.Clear();
            string sql = "SELECT Descripcion, Cantidad FROM DbSanciones WHERE IdJugador = @IdJugador";
            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdJugador", jugador.Idjugador)))
            {
                while (reader.Read())
                {
                    jugador.Sanciones.Add(reader["Descripcion"].ToString(), (int)reader["Cantidad"]);
                }
            }
        }

        /// <summary>
        /// Método de ayuda (privado) que sincroniza (delete-then-insert) la colección
        /// <c>Puntuacion</c> de un jugador con la base de datos.
        /// </summary>
        /// <param name="jugador">El <see cref="Jugador"/> con la lista de puntuaciones a guardar.</param>
        private void SyncPuntuacion(Jugador jugador)
        {
            string sqlDelete = "DELETE FROM DbPuntuacion WHERE IdJugador = @IdJugador";
            base.ExecuteNonQuery(sqlDelete, CommandType.Text, new SqlParameter("@IdJugador", jugador.Idjugador));

            string sqlInsert = "INSERT INTO DbPuntuacion (IdPuntuacion, Descripcion, Cantidad, IdJugador) VALUES (@IdP, @Desc, @Cant, @IdJ)";
            foreach (var item in jugador.Puntuacion)
            {
                base.ExecuteNonQuery(sqlInsert, CommandType.Text,
                    new SqlParameter("@IdP", Guid.NewGuid()),
                    new SqlParameter("@Desc", item.Key),
                    new SqlParameter("@Cant", item.Value),
                    new SqlParameter("@IdJ", jugador.Idjugador)
                );
            }
        }

        /// <summary>
        /// Método de ayuda (privado) que sincroniza (delete-then-insert) la colección
        /// <c>Sanciones</c> de un jugador con la base de datos.
        /// </summary>
        /// <param name="jugador">El <see cref="Jugador"/> con la lista de sanciones a guardar.</param>
        private void SyncSanciones(Jugador jugador)
        {
            string sqlDelete = "DELETE FROM DbSanciones WHERE IdJugador = @IdJugador";
            base.ExecuteNonQuery(sqlDelete, CommandType.Text, new SqlParameter("@IdJugador", jugador.Idjugador));

            string sqlInsert = "INSERT INTO DbSanciones (IdSancion, Descripcion, Cantidad, IdJugador) VALUES (@IdS, @Desc, @Cant, @IdJ)";
            foreach (var item in jugador.Sanciones)
            {
                base.ExecuteNonQuery(sqlInsert, CommandType.Text,
                    new SqlParameter("@IdS", Guid.NewGuid()),
                    new SqlParameter("@Desc", item.Key),
                    new SqlParameter("@Cant", item.Value),
                    new SqlParameter("@IdJ", jugador.Idjugador)
                );
            }
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
            var lista = new List<Jugador>();
            string sql = $"{_sqlSelect} WHERE j.IdEquipo = @IdEquipo AND j.Habilitado = 1";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdEquipo", idEquipo)))
            {

                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var jugador = JugadorAdapter.Current.Get(values);

                    lista.Add(jugador);
                }

            }
            foreach (var jugador in lista)
            {
                PopulatePuntuacion(jugador); // N+...
                PopulateSanciones(jugador); // ...N consultas
            }
            return lista;
        }

        /// <summary>
        /// Obtiene una lista de todos los jugadores HABILITADOS que no tienen equipo
        /// (agentes libres).
        /// </summary>
        /// <returns>Una colección de <see cref="Jugador"/>.</returns>
        public List<Jugador> GetSinEquipo()
        {
            var lista = new List<Jugador>();
            string sql = $"{_sqlSelect} WHERE j.IdEquipo IS NULL AND j.Habilitado = 1";
            using (var reader = base.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var jugador = JugadorAdapter.Current.Get(values);
                    lista.Add(jugador);
                }
            }
            foreach (var jugador in lista)
            {
                PopulatePuntuacion(jugador); // N+...
                PopulateSanciones(jugador); // ...N consultas
            }
            return lista;
        }

        /// <summary>
        /// Obtiene una lista de TODOS los jugadores (habilitados y deshabilitados).
        /// </summary>
        /// <returns>Una colección de <see cref="Jugador"/>.</returns>
        public IEnumerable<Jugador> GetAllIncludingDisabled()
        {

            string sql = _sqlSelect;

            List<Jugador> jugadores = new List<Jugador>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    jugadores.Add(JugadorAdapter.Current.Get(values));
                }
            }

            foreach (var jugador in jugadores)
            {
                PopulatePuntuacion(jugador); 
                PopulateSanciones(jugador); 
            }
            return jugadores;
        }
    }
}