using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DAL.Implementations.SqlServer.Adapters
{
    internal class CanchaDisponibilidadAdapter
    {
        private readonly static CanchaDisponibilidadAdapter _instance = new CanchaDisponibilidadAdapter();

        public static CanchaDisponibilidadAdapter Current
        {
            get { return _instance; }
        }

        public CanchaDisponibilidad Get(object[] values)
        {
            if (values == null || values.Length < 5)
            {
                throw new ArgumentException("Se requieren 5 valores para mapear CanchaDisponibilidad.");
            }

            int diaSemanaInt = (int)values[2];
            DayOfWeek diaSemanaEnum = (DayOfWeek)diaSemanaInt;

            return new CanchaDisponibilidad
            {
                IdDisponibilidad = (Guid)values[0],
                IdCancha = (Guid)values[1],
                DiaSemana = diaSemanaEnum,
                HoraInicio = (TimeSpan)values[3],
                HoraFin = (TimeSpan)values[4]
            };
        }
    }
}
