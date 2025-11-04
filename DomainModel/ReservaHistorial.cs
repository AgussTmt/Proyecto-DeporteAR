using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    /// <summary>
    /// Representa un registro de auditoría (log) que rastrea los cambios
    /// de estado en un <see cref="CanchaHorario"/> (una reserva).
    /// </summary>
    public class ReservaHistorial
    {
        /// <summary>
        /// Identificador único del registro de historial (PK).
        /// </summary>
        public Guid IdHistorial { get; set; }

        /// <summary>
        /// La clave foránea (FK) que vincula este evento de historial
        /// al <see cref="CanchaHorario"/> que fue modificado.
        /// </summary>
        public Guid IdCanchaHorario { get; set; }

        /// <summary>
        /// El <see cref="Cliente"/> que originó el cambio.
        /// Puede ser <c>null</c> si el cambio fue originado por el sistema o un admin.
        /// </summary>
        public Guid? IdCliente { get; set; }

        /// <summary>
        /// La fecha y hora exactas en que ocurrió el evento de cambio.
        /// </summary>
        public DateTime FechaHoraEvento { get; set; }

        /// <summary>
        /// El estado de la reserva *antes* de que ocurriera el evento.
        /// (ej: "Libre").
        /// </summary>
        public string EstadoAnterior { get; set; }

        /// <summary>
        /// El estado de la reserva *después* de que ocurriera el evento.
        /// (ej: "Reservada").
        /// </summary>
        public string EstadoNuevo { get; set; }

        /// <summary>
        /// Un detalle o nota legible que describe el evento
        /// (ej: "Cliente canceló fuera de término", "Sistema asignó a torneo").
        /// </summary>
        public string Detalle { get; set; }
    }
}
