using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    /// <summary>
    /// Define la plantilla de disponibilidad horaria estándar para una cancha
    /// en un día específico de la semana.
    /// (Ej: "Cancha 1" está disponible los Lunes de 09:00 a 22:00).
    /// </summary>
    public class CanchaDisponibilidad
    {
        /// <summary>
        /// Identificador único del registro de disponibilidad (PK).
        /// </summary>
        public Guid IdDisponibilidad { get; set; }

        /// <summary>
        /// La clave foránea (FK) que vincula esta disponibilidad a una <see cref="Cancha"/> específica.
        /// </summary>
        public Guid IdCancha { get; set; }

        /// <summary>
        /// El día de la semana para el cual aplica esta franja horaria (ej: DayOfWeek.Monday).
        /// </summary>
        public DayOfWeek DiaSemana { get; set; }

        /// <summary>
        /// La hora de apertura de la cancha para este día (ej: 09:00:00).
        /// </summary>
        public TimeSpan HoraInicio { get; set; }

        /// <summary>
        /// La hora de cierre de la cancha para este día (ej: 22:00:00).
        /// </summary>
        public TimeSpan HoraFin { get; set; }
    }
}
