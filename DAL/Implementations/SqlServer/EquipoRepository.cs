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
    internal class EquipoRepository : SqlTransactRepository, IEquipoRepository
    {
        public EquipoRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }


        private const string _sqlSelect = @"SELECT 
            e.IdEquipo, e.CantAusencias, e.FechaDeCreacion, e.Nombre,
            e.IdCliente, 
            ea.Descripcion AS EstadoAsistenciaDesc,
            e.Habilitado
        FROM DbEquipo e
        LEFT JOIN DbEstadoAsistencia ea ON e.IdEstadoAsistencia = ea.IdEstadoAsistencia
        WHERE e.Habilitado = 1";

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
                PopulateJugadores(equipo);
            }
            

            return listaEquipos;
        }

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
               
                PopulateJugadores(equipo);
            }
            return equipo;
        }

        public IEnumerable<Equipo> GetAll()
        {
            var list = new List<Equipo>();


            using (var reader = base.ExecuteReader(_sqlSelect, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(EquipoAdapter.Current.Get(values));
                }
            }
            return list;
        }

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
    }
}
