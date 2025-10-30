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
            if (values == null || values.Length < 7)
            {
                throw new ArgumentException("Se requieren 7 valores para mapear una CanchaHorario.");
            }


            var estado = EstadoReserva.Libre;
            if (values[6] != DBNull.Value)
            {

                Enum.TryParse(values[6].ToString(), out estado);
            }

            return new CanchaHorario
            {
                IdCanchaHorario = (Guid)values[0],
                Cancha = new Cancha { IdCancha = (Guid)values[1] },
                FechaHorario = values[2] == DBNull.Value ? default(DateTime) : (DateTime)values[2],
                ReservadaPor = (values[3] == DBNull.Value)
                                ? null
                                : new Cliente { IdCliente = (Guid)values[3] },
                Abonada = Convert.ToBoolean(values[4]),
                FueCambiada = Convert.ToBoolean(values[5]),

                Estado = estado
            };
        }
    }
}
