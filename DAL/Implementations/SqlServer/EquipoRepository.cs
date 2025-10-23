using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Implementations.SqlServer.Helper;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class EquipoRepository : SqlTransactRepository, IEquipoRepository
    {
        public EquipoRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }


        private const string _sqlSelect = @"SELECT 
                e.IdEquipo, e.CantAusencias, e.FechaDeCreacion, e.Nombre,
                e.IdCliente, 
                ea.Descripcion AS EstadoAsistenciaDesc
            FROM DbEquipo e
            LEFT JOIN DbEstadoAsistencia ea ON e.IdEstadoAsistencia = ea.IdEstadoAsistencia";

        public void Add(Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public void AddPlayers(List<Jugador> jugadores)
        {
            throw new NotImplementedException();
        }

        public Equipo GetByCompeticion(Competicion competicion)
        {
            throw new NotImplementedException();
        }

        public void Update(Equipo equipo)
        {
            throw new NotImplementedException();
        }


        private Guid GetEstadoAsistenciaId(EstadoAsistencia estado)
        {
            string desc = estado.ToString();
            string sql = "SELECT IdEstadoAsistencia FROM DbEstadoAsistencia WHERE Descripcion = @Descripcion";
            object result = base.ExecuteScalar(sql, CommandType.Text, new SqlParameter("@Descripcion", desc));
            if (result == null || result == DBNull.Value)
                throw new InvalidOperationException($"El estado '{desc}' no fue encontrado en DbEstadoAsistencia.");
            return (Guid)result;
        }

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
    }
}
