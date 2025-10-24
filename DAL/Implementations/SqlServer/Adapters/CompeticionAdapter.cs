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
            
            if (values == null || values.Length < 13)
            {
                throw new ArgumentException("Se requieren 13 valores para mapear una Competicion.");
            }


            Enum.TryParse(values[10]?.ToString(), out FormatoEnum formato);

            Enum.TryParse(values[3]?.ToString(), out EstadoCompeticion estado);

            return new Competicion
            {
                IdCompeticion = (Guid)values[0],
                Cupos = values[1] == DBNull.Value ? 0 : (int)values[1],
                CuposMinimos = values[2] == DBNull.Value ? 0 : (int)values[2],


                Estado = estado,
                FechaCreacion = (DateTime)values[4],
                FechaInicio = (DateTime)values[5],
                FranjaHoraria = values[6]?.ToString(),
                Frecuencia = values[7] == DBNull.Value ? 0 : (int)values[7],
                Nombre = values[8]?.ToString(),
                Precio = values[9] == DBNull.Value ? 0m : (decimal)values[9],
                Formato = formato,


                canchaAsignada = new Cancha { IdCancha = (Guid)values[11] },

                Deporte = values[12]?.ToString(),


                ListaEquipos = new List<Equipo>()
            };
        }
    }
}


