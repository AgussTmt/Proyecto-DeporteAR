using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    /// <summary>
    /// Representa una Cancha (de fútbol, tenis, pádel, etc.) en el establecimiento.
    /// Es una entidad central del dominio de negocio.
    /// </summary>
    public class Cancha
    {
        /// <summary>
        /// Identificador único de la cancha (PK).
        /// </summary>
        public Guid IdCancha { get; set; }

        /// <summary>
        /// Capacidad máxima de jugadores permitida en la cancha (ej: 5, 10, 11).
        /// </summary>
        public int Capacidad { get; set; }

        /// <summary>
        /// Nombre del deporte que se practica (ej: "Fútbol 5", "Tenis", "Pádel").
        /// </summary>
        public string Deporte { get; set; }

        /// <summary>
        /// La duración estándar (en minutos) de un turno o reserva para esta cancha (ej: 60, 90).
        /// </summary>
        public int DuracionXPartidoMin { get; set; }

        /// <summary>
        /// Indica el estado de la cancha.
        /// (ej: true = Habilitada, false = En mantenimiento/Deshabilitada).
        /// </summary>
        public bool Estado { get; set; }

        /// <summary>
        /// Fecha y hora en que la cancha fue registrada en el sistema (campo de auditoría).
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Nombre descriptivo o identificatorio de la cancha.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// El precio (costo) de la reserva para esta cancha.
        /// </summary>
        public decimal Precio { get; set; }
    }

}
