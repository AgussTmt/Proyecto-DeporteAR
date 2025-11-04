using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    /// <summary>
    /// Representa a un Equipo inscrito en una <see cref="Competicion"/>.
    /// Agrupa a un capitán (Cliente) y una lista de Jugadores.
    /// </summary>
    public class Equipo
    {
        /// <summary>
        /// Identificador único del equipo (PK).
        /// </summary>
        public Guid IdEquipo { get; set; }

        /// <summary>
        /// El <see cref="Cliente"/> que actúa como capitán o responsable del equipo.
        /// </summary>
        public Cliente Capitan { get; set; }

        /// <summary>
        /// La lista de <see cref="Jugador"/> que componen el plantel del equipo.
        /// </summary>
        public List<Jugador> Jugadores { get; set; }

        /// <summary>
        /// El nombre oficial del equipo.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Contador de ausencias (partidos perdidos por "no presentarse").
        /// </summary>
        public int CantAusencias { get; set; }

        /// <summary>
        /// Fecha y hora en que el equipo fue registrado en el sistema.
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// El estado de asistencia del equipo para el próximo partido programado
        /// (ej: Pendiente de confirmar, Confirmado, Cancelado).
        /// </summary>
        public EstadoAsistencia EstadoProxPartido { get; set; }

        /// <summary>
        /// Indica si el equipo está habilitado para participar
        /// (ej: false si fue expulsado o se dio de baja).
        /// </summary>
        public bool Habilitado { get; set; }

        //Entiendo q esto no deberia estar aca, y con el patron MCV solucionaria esta falta en la arquitectura.
        /// <summary>
        /// Propiedad calculada que devuelve el número de jugadores en la lista.
        /// (Nota: Es lógica de vista/VM, pero útil tenerla aquí).
        /// </summary>
        public int CantidadJugadores
        {
            get { return Jugadores?.Count ?? 0; }
        }

    }

    public enum EstadoAsistencia
    {
        Pendiente,
        Confirmado,
        Cancelado
    }
}
