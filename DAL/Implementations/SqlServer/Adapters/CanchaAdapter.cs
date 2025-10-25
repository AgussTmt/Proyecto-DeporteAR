using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DAL.Implementations.SqlServer.Adapters
{

    internal class CanchaAdapter
    {
        private readonly static CanchaAdapter _instance = new CanchaAdapter();

        public static CanchaAdapter Current
        {
            get { return _instance; }
        }

        private CanchaAdapter() { }

        /// <summary>
        /// Mapea un array de object[] a una entidad Cancha.
        /// Asume el orden de columnas: 0:IdCancha, 1:Capacidad, 2:IdDeporte, 
        /// 3:DuracionXPartido, 4:FechaDeCreacion, 5:EstadoCancha, 6:FranjaHoraria, 
        /// 7:Nombre, 8:Precio
        /// </summary>
        public Cancha Get(object[] values)
        {
            if (values == null || values.Length < 8)
            {
                throw new ArgumentException("Se requieren 9 valores para mapear una Cancha.");
            }


            return new Cancha
            {
                IdCancha = (Guid)values[0],
                Capacidad = values[1] == DBNull.Value ? 0 : Convert.ToInt32(values[1]),
                Deporte = values[2] == DBNull.Value ? null : values[2].ToString(),
                DuracionXPartidoMin = values[3] == DBNull.Value ? 0 : (int)((TimeSpan)values[3]).TotalMinutes,
                FechaCreacion = values[4] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(values[4]),
                Estado = values[5] != DBNull.Value && Convert.ToBoolean(values[5]),
                Nombre = values[6]?.ToString(),
                Precio = values[7] == DBNull.Value ? 0m : decimal.Parse(values[7].ToString())
            };

        }
    }
}

