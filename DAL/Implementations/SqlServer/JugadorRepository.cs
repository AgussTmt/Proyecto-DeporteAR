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
    internal class JugadorRepository : SqlTransactRepository, IJugadorRepository
    {
        public JugadorRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        private const string _sqlSelect = @"SELECT 
                IdJugador, IdEquipo, Nombre, PartidosJugados, Mvp, Apellido 
            FROM DbJugador";

        public void Add(Jugador entity)
        {
            if (entity.IdEquipo == Guid.Empty)
                throw new InvalidOperationException("No se puede guardar un Jugador sin un IdEquipo válido.");

            // 1. Inserta en la tabla principal (DbJugador)
            string sql = @"INSERT INTO DbJugador 
                           (IdJugador, IdEquipo, Nombre, PartidosJugados, Mvp, Apellido)
                           VALUES
                           (@IdJ, @IdE, @Nombre, @PJ, @Mvp, @Apellido)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdJ", entity.Idjugador),
                new SqlParameter("@IdE", entity.IdEquipo),
                new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                new SqlParameter("@PJ", entity.PartidosJugados),
                new SqlParameter("@Mvp", entity.CantMvp),
                new SqlParameter("@Apellido", (object)entity.Apellido ?? DBNull.Value)
            );

            // 2. Sincroniza las tablas hijas
            SyncPuntuacion(entity);
            SyncSanciones(entity);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Jugador> GetAll()
        {
            var lista = new List<Jugador>();

            
            using (var reader = base.ExecuteReader(_sqlSelect, CommandType.Text))
            {
                while (reader.Read())
                {
                    
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    var jugador = JugadorAdapter.Current.Get(values);

                    PopulatePuntuacion(jugador);
                    PopulateSanciones(jugador);

                    lista.Add(jugador);
                }
            }
            return lista;
        }

        public Jugador GetById(Guid id)
        {
            Jugador jugador = null;
            string sql = $"{_sqlSelect} WHERE IdJugador = @Id";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    jugador = JugadorAdapter.Current.Get(values);

                    // Rellena los diccionarios
                    PopulatePuntuacion(jugador);
                    PopulateSanciones(jugador);
                }
            }
            return jugador;
        }

        public void Update(Jugador entity)
        {
            string sql = @"UPDATE DbJugador SET
                            IdEquipo = @IdE,
                            Nombre = @Nombre,
                            PartidosJugados = @PJ,
                            Mvp = @Mvp,
                            Apellido = @Apellido
                           WHERE IdJugador = @IdJ";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdE", entity.IdEquipo),
                new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                new SqlParameter("@PJ", entity.PartidosJugados),
                new SqlParameter("@Mvp", entity.CantMvp),
                new SqlParameter("@Apellido", (object)entity.Apellido ?? DBNull.Value),
                new SqlParameter("@IdJ", entity.Idjugador)
            );

            // 2. Sincroniza las tablas hijas
            SyncPuntuacion(entity);
            SyncSanciones(entity);
        }


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

        private void SyncPuntuacion(Jugador jugador)
        {
            // 1. Borra las puntuaciones viejas
            string sqlDelete = "DELETE FROM DbPuntuacion WHERE IdJugador = @IdJugador";
            base.ExecuteNonQuery(sqlDelete, CommandType.Text, new SqlParameter("@IdJugador", jugador.Idjugador));

            // 2. Inserta las nuevas
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

        private void SyncSanciones(Jugador jugador)
        {
            // 1. Borra las sanciones viejas
            string sqlDelete = "DELETE FROM DbSanciones WHERE IdJugador = @IdJugador";
            base.ExecuteNonQuery(sqlDelete, CommandType.Text, new SqlParameter("@IdJugador", jugador.Idjugador));

            // 2. Inserta las nuevas
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

        public void CambiarHabilitado(Guid id)
        {
            string sql = @"UPDATE DbJugador 
                   SET Habilitado = ~Habilitado 
                   WHERE IdJugador = @IdJugador";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdJugador", id)
            );
        }

        public IEnumerable<Jugador> GetByEquipo(Guid idEquipo)
        {
            var lista = new List<Jugador>();
            string sql = $"{_sqlSelect} WHERE IdEquipo = @IdEquipo";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdEquipo", idEquipo)))
            {
                
                while (reader.Read())
                {
                    
                    object[] values = new object[reader.FieldCount];
                    
                    reader.GetValues(values);

                    
                    var jugador = JugadorAdapter.Current.Get(values);

                    
                    PopulatePuntuacion(jugador);
                    PopulateSanciones(jugador);

                    lista.Add(jugador);
                }
            }
            return lista;
        }
    }
}
