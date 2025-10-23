using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations.SqlServer.Adapters
{
    
        internal class CompeticionAdapter
        {
            private readonly static CompeticionAdapter _instance = new CompeticionAdapter();

            public static CompeticionAdapter Current
            {
                get { return _instance; }
            }

            private CompeticionAdapter() { }

            /// <summary>
            /// Mapea un array de object[] a una entidad Competicion.
            /// Asume el orden: 0:IdCompeticion, 1:Cupos, 2:Estado (string), 3:FechaCreacion,
            /// 4:FechaInicio, 5:FranjaHoraria, 6:Frecuencia, 7:Nombre, 8:PrecioInscripcion,
            /// 9:Formato (string), 10:IdCancha (para stub), 11:Deporte (string)
            /// </summary>
            public Competicion Get(object[] values)
            {
                if (values == null || values.Length < 12)
                {
                    throw new ArgumentException("Se requieren 12 valores para mapear una Competicion.");
                }

                
                Enum.TryParse(values[9]?.ToString(), out FormatoEnum formato);
                
                Enum.TryParse(values[2]?.ToString(), out EstadoCompeticion estado);

                return new Competicion
                {
                    IdCompeticion = (Guid)values[0],
                    Cupos = values[1] == DBNull.Value ? 0 : (int)values[1],
                    Estado = estado,
                    FechaCreacion = (DateTime)values[3],
                    FechaInicio = (DateTime)values[4],
                    FranjaHoraria = values[5]?.ToString(),
                    Frecuencia = values[6] == DBNull.Value ? 0 : (int)values[6],
                    Nombre = values[7]?.ToString(),
                    Precio = values[8] == DBNull.Value ? 0m : (decimal)values[8],
                    Formato = formato,

                    
                    canchaAsignada = new Cancha { IdCancha = (Guid)values[10] },

                    Deporte = values[11]?.ToString(),

                    
                    ListaEquipos = new List<Equipo>()
                };
            }
        }
}

