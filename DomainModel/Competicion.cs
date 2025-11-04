using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    /// <summary>
    /// Representa una Competición (ej: Torneo, Liga) organizada en el establecimiento.
    /// Es una entidad Agregada (Aggregate Root) que gestiona Equipos, Partidos y Clasificaciones.
    /// </summary>
    public class Competicion
    {
        /// <summary>
        /// Identificador único de la competición (PK).
        /// </summary>
        public Guid IdCompeticion { get; set; }

        /// <summary>
        /// La <see cref="Cancha"/> principal donde se disputarán los partidos de esta competición.
        /// </summary>
        public Cancha canchaAsignada { get; set; }

        /// <summary>
        /// El número máximo de equipos que pueden inscribirse.
        /// </summary>
        public int Cupos { get; set; }

        /// <summary>
        /// El número mínimo de equipos necesarios para que la competición pueda iniciar.
        /// </summary>
        public int CuposMinimos { get; set; }

        /// <summary>
        /// El deporte de la competición (ej: "Fútbol 5", "Pádel").
        /// </summary>
        public string Deporte { get; set; }

        /// <summary>
        /// La lista de <see cref="Equipo"/> inscritos en esta competición.
        /// </summary>
        public List<Equipo> ListaEquipos { get; set; }

        /// <summary>
        /// El estado actual de la competición (ej: Sin Fixture, Finalizado).
        /// </summary>
        public EstadoCompeticion Estado { get; set; }

        /// <summary>
        /// Fecha y hora en que la competición fue registrada en el sistema.
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// La fecha programada para el inicio de la competición.
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// El formato de la competición (Liga).
        /// </summary>
        public FormatoEnum Formato { get; set; }

        /// <summary>
        /// Describe la franja horaria en que se jugarán los partidos (ej: "Sábados 14hs-18hs").
        /// </summary>
        public string FranjaHoraria { get; set; }

        /// <summary>
        /// La frecuencia con que se juega (ej: 1 = Semanal, 2 = Quincenal).
        /// </summary>
        public int Frecuencia { get; set; }

        /// <summary>
        /// El nombre oficial de la competición (ej: "Copa Verano 2025").
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// El precio (costo) de inscripción para un equipo.
        /// </summary>
        public decimal Precio { get; set; }
    }

    public enum EstadoCompeticion
    {
        SinFixture,
        ConFixture,
        Finalizado, 
        Cancelado,      
        Archivado
    }

    public enum FormatoEnum
    {
        Liga,
        Torneo
    }
}
