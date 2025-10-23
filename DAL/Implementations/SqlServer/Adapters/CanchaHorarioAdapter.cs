using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations.SqlServer.Adapters
{
    internal class CanchaHorarioAdapter
    {
        private readonly static CanchaHorarioAdapter _instance = new CanchaHorarioAdapter();

        public static CanchaHorarioAdapter Current
        {
            get { return _instance; }
        }

        private CanchaHorarioAdapter() { }


        public CanchaHorario Get(object[] values)
        {
            if (values == null || values.Length < 8)
            {
                throw new ArgumentException("Se requieren 8 valores para mapear una CanchaHorario.");
            }


            var estado = EstadoReserva.Libre;
            if (values[7] != DBNull.Value)
            {

                Enum.TryParse(values[7].ToString(), out estado);
            }

            return new CanchaHorario
            {
                IdCanchaHorario = (Guid)values[0],

                // Crea un objeto "stub" solo con el ID
                Cancha = new Cancha { IdCancha = (Guid)values[1] },

                FechaHorario = (DateTime)values[2],

                // Crea un objeto "stub" si el IdCliente no es nulo
                ReservadaPor = (values[3] == DBNull.Value)
                                ? null
                                : new Cliente { IdCliente = (Guid)values[3] },

                Abonada = (values[4] != DBNull.Value) && (bool)values[4],
                FueCambiada = (values[5] != DBNull.Value) && (bool)values[5],

                Estado = estado
            };
        }
    }
}
