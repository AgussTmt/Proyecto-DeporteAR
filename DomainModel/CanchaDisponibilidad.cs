using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class CanchaDisponibilidad
    {
        public Guid IdDisponibilidad { get; set; }
        public Guid IdCancha { get; set; }
        public DayOfWeek DiaSemana { get; set; } 
        public TimeSpan HoraInicio { get; set; } 
        public TimeSpan HoraFin { get; set; }
    }
}
