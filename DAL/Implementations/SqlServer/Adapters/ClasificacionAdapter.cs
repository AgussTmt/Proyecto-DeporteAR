using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DAL.Implementations.SqlServer.Adapters
{
    internal class ClasificacionAdapter
    {
        private readonly static ClasificacionAdapter _instance = new ClasificacionAdapter();
        public static ClasificacionAdapter Current { get { return _instance; } }
        private ClasificacionAdapter() { }

        /// <summary>
        /// Mapea un array de object[] a una entidad Clasificacion.
        /// Asume el orden: 0:Id, 1:Derrotas, 2:Empates, 3:Victorias, 
        /// 4:PartidosJugados, 5:NombreEquipo, 6:IdCompeticion, 
        /// 7:GolesAFavor, 8:Puntos
        /// </summary>
        public Clasificacion Get(object[] values)
        {
            if (values == null || values.Length < 9)
            {
                throw new ArgumentException("Se requieren 9 valores para mapear una Clasificacion.");
            }

            return new Clasificacion
            {
                IdClasificacion = (Guid)values[0],
                Derrotas = values[1] == DBNull.Value ? 0 : (int)values[1],
                Empates = values[2] == DBNull.Value ? 0 : (int)values[2],
                Victorias = values[3] == DBNull.Value ? 0 : (int)values[3],
                PartidosJugados = values[4] == DBNull.Value ? 0 : (int)values[4],
                Equipo = values[5]?.ToString(),
                IdCompeticion = (Guid)values[6],
                GolesAFavor = values[7] == DBNull.Value ? 0 : (int)values[7],
                Puntos = values[8] == DBNull.Value ? 0 : (int)values[8]
            };
        }
    }
}
