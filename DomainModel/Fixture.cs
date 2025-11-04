using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    /// <summary>
    /// Representa un único partido (match) dentro del fixture de una <see cref="Competicion"/>.
    /// Vincula a los equipos con el horario y cancha, y almacena el resultado.
    /// </summary>
    public class Fixture
    {
        /// <summary>
        /// Identificador único del partido (PK).
        /// </summary>
        public Guid IdFixture { get; set; }

        /// <summary>
        /// El resultado final del partido (ej: "2-1", "6-3 6-4").
        /// </summary>
        public string Resultado { get; set; }

        /// <summary>
        /// El estado actual del partido (ej: Pendiente, Finalizado).
        /// </summary>
        public EstadoFixture Estado { get; set; }

        /// <summary>
        /// La clave foránea (FK) que vincula este partido a una <see cref="Competicion"/>.
        /// </summary>
        public Guid IdCompeticion { get; set; }

        /// <summary>
        /// La lista de<see cref = "Equipo" /> que participan en este partido(normalmente 2).
        /// </summary>
        public List<Equipo> Equipos { get; set; }

        /// <summary>
        /// El <see cref="CanchaHorario"/> (turno) asignado para este partido,
        /// que define la cancha y la hora.
        /// </summary>
        public CanchaHorario CanchaHorario { get; set; }

        /// <summary>
        /// Constructor que inicializa la lista de <see cref="Equipos"/>.
        /// </summary>
        public Fixture()
        {
            Equipos = new List<Equipo>();
        }
    }

    public enum EstadoFixture
    {
        Pendiente,
        Finalizado,
        Postergado

    }
}
