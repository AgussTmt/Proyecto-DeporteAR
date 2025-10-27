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
    internal class ClasificacionRepository : SqlTransactRepository, IClasificacionRepository
    {
        public ClasificacionRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        private const string _sqlSelect = @"SELECT 
                IdClasificacion, CantDerrotas, CantEmpates, CantVictorias, 
                PartidosJugados, NombreEquipo, IdCompeticion, GolesAFavor, Puntos
            FROM DbClasificacion";

        public Clasificacion GetByCompeticionEquipo(Competicion competicion, Equipo equipo)
        {
            Clasificacion clasificacion = null;
            string sql = $"{_sqlSelect} WHERE IdCompeticion = @IdComp AND NombreEquipo = @NombreEquipo";

            using (var reader = base.ExecuteReader(sql, CommandType.Text,
                new SqlParameter("@IdComp", competicion.IdCompeticion),
                new SqlParameter("@NombreEquipo", equipo.Nombre)))
            {
                if (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    clasificacion = ClasificacionAdapter.Current.Get(values);
                }
            }
            return clasificacion; 
        }

        public void Update(Clasificacion clasificacion)
        {
            
            string sql = @"UPDATE DbClasificacion SET
                            CantDerrotas = @Derrotas,
                            CantEmpates = @Empates,
                            CantVictorias = @Victorias,
                            PartidosJugados = @PJ,
                            GolesAFavor = @GF,
                            Puntos = @Puntos
                           WHERE IdClasificacion = @Id";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Derrotas", clasificacion.Derrotas),
                new SqlParameter("@Empates", clasificacion.Empates),
                new SqlParameter("@Victorias", clasificacion.Victorias),
                new SqlParameter("@PJ", clasificacion.PartidosJugados),
                new SqlParameter("@GF", clasificacion.GolesAFavor),
                new SqlParameter("@Puntos", clasificacion.Puntos),
                new SqlParameter("@Id", clasificacion.IdClasificacion)
            );
        }

        public void Add(Clasificacion clasificacion)
        {
            string sql = @"INSERT INTO DbClasificacion
                           (IdClasificacion, CantDerrotas, CantEmpates, CantVictorias, PartidosJugados, NombreEquipo, IdCompeticion, GolesAFavor, Puntos)
                           VALUES
                           (@Id, @Der, @Emp, @Vic, @PJ, @Nombre, @IdComp, @GF, @Puntos)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Id", clasificacion.IdClasificacion),
                new SqlParameter("@Der", clasificacion.Derrotas),
                new SqlParameter("@Emp", clasificacion.Empates),
                new SqlParameter("@Vic", clasificacion.Victorias),
                new SqlParameter("@PJ", clasificacion.PartidosJugados),
                new SqlParameter("@Nombre", clasificacion.Equipo),
                new SqlParameter("@IdComp", clasificacion.IdCompeticion),
                new SqlParameter("@GF", clasificacion.GolesAFavor),
                new SqlParameter("@Puntos", clasificacion.Puntos)
            );
        }

        /// <summary>
        /// Obtiene la tabla de posiciones completa de una competición
        /// </summary>
        public List<Clasificacion> GetByCompeticion(Guid idCompeticion)
        {
            var list = new List<Clasificacion>();
            string sql = $"{_sqlSelect} WHERE IdCompeticion = @IdComp ORDER BY Puntos DESC, GolesAFavor DESC";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdComp", idCompeticion)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(ClasificacionAdapter.Current.Get(values));
                }
            }
            return list;
        }

        public void Delete(Guid idClasificacion)
        {
            string sql = "DELETE FROM DbClasificacion WHERE IdClasificacion = @IdClasificacion";
            base.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@IdClasificacion", idClasificacion));
        }
    }
}
