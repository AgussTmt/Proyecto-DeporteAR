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
    /// Repositorio SQL para gestionar la tabla de posiciones (la entidad <see cref="Clasificacion"/>)
    /// de las competiciones.
    /// </summary>
    /// <remarks>
    /// Opera dentro de una transacción y conexión SQL existente (Unit of Work).
    /// </remarks>
    internal class ClasificacionRepository : SqlTransactRepository, IClasificacionRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de conexión y transacción de una
        /// Unidad de Trabajo (Unit of Work) existente.
        /// </summary>
        /// <param name="context">La <see cref="SqlConnection"/> activa.</param>
        /// <param name="_transaction">La <see cref="SqlTransaction"/> activa.</param>
        public ClasificacionRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        private const string _sqlSelect = @"SELECT 
                IdClasificacion, CantDerrotas, CantEmpates, CantVictorias, 
                PartidosJugados, NombreEquipo, IdCompeticion, GolesAFavor, Puntos
            FROM DbClasificacion";

        /// <summary>
        /// Obtiene el registro de clasificación (estadísticas) de un equipo específico
        /// dentro de una competición específica.
        /// </summary>
        /// <param name="competicion">La <see cref="Competicion"/> en la que participa el equipo.</param>
        /// <param name="equipo">El <see cref="Equipo"/> cuyas estadísticas se buscan.</param>
        /// <returns>El objeto <see cref="Clasificacion"/> o <c>null</c> si no se encuentra.</returns>
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

        /// <summary>
        /// Actualiza las estadísticas (partidos jugados, puntos, goles, etc.)
        /// de un equipo en la tabla de posiciones (ej: después de un partido).
        /// </summary>
        /// <param name="clasificacion">La entidad <see cref="Clasificacion"/> con los datos actualizados.</param>
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

        /// <summary>
        /// Agrega un nuevo registro de equipo a la tabla de posiciones
        /// (ej: al inicio del torneo o al inscribir un equipo).
        /// </summary>
        /// <param name="clasificacion">La entidad <see cref="Clasificacion"/> a insertar.</param>
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
        /// Obtiene la tabla de posiciones completa (lista de <see cref="Clasificacion"/>) para una competición,
        /// ordenada por Puntos (desc) y Goles a Favor (desc).
        /// </summary>
        /// <param name="idCompeticion">El ID de la <see cref="Competicion"/>.</param>
        /// <returns>Una lista de <see cref="Clasificacion"/> (la tabla de posiciones).</returns>
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

        /// <summary>
        /// Elimina un registro de clasificación de la base de datos.
        /// </summary>
        /// <param name="idClasificacion">El ID (PK) del registro de <see cref="Clasificacion"/> a eliminar.</param>
        public void Delete(Guid idClasificacion)
        {
            string sql = "DELETE FROM DbClasificacion WHERE IdClasificacion = @IdClasificacion";
            base.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@IdClasificacion", idClasificacion));
        }
    }
}