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

        private DayOfWeek? MapearDia(string diaEsp)
        {
            // Usamos ToLower() y CompareInfo para ignorar tildes y mayúsculas
            var cultureInfo = new System.Globalization.CultureInfo("es-ES");
            var compareInfo = cultureInfo.CompareInfo;
            var options = System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace;

            if (compareInfo.Compare(diaEsp, "domingo", options) == 0) return DayOfWeek.Sunday;
            if (compareInfo.Compare(diaEsp, "lunes", options) == 0) return DayOfWeek.Monday;
            if (compareInfo.Compare(diaEsp, "martes", options) == 0) return DayOfWeek.Tuesday;
            if (compareInfo.Compare(diaEsp, "miércoles", options) == 0) return DayOfWeek.Wednesday;
            if (compareInfo.Compare(diaEsp, "jueves", options) == 0) return DayOfWeek.Thursday;
            if (compareInfo.Compare(diaEsp, "viernes", options) == 0) return DayOfWeek.Friday;
            if (compareInfo.Compare(diaEsp, "sábado", options) == 0) return DayOfWeek.Saturday;

            return null; // Opcional: manejar "Miercoles" y "Sabado" sin tilde si así están en tu BD
        }
    }
}
