using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    /// <summary>
    /// Representa a un Jugador individual, miembro de un <see cref="Equipo"/>.
    /// Almacena estadísticas personales como puntuaciones y sanciones.
    /// </summary>
    public class Jugador
    {
        /// <summary>
        /// La clave foránea (FK) que vincula a este jugador con su <see cref="Equipo"/>.
        /// Es nullable (Guid?) por si un jugador puede existir sin equipo (ej: "agente libre").
        /// </summary>
        public Guid? IdEquipo { get; set; }

        /// <summary>
        /// Identificador único del jugador (PK).
        /// </summary>
        public Guid Idjugador { get; set; }

        /// <summary>
        /// El nombre del equipo al que pertenece (útil para vistas rápidas sin hacer JOIN).
        /// </summary>
        public string NombreEquipo { get; set; }

        /// <summary>
        /// Nombre de pila del jugador.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido del jugador.
        /// </summary>
        public string Apellido { get; set; }

        /// <summary>
        /// Contador de veces que el jugador fue elegido "Mejor Jugador del Partido" (MVP).
        /// </summary>
        public int CantMvp { get; set; }

        /// <summary>
        /// El número total de partidos disputados por el jugador.
        /// </summary>
        public int PartidosJugados { get; set; }

        /// <summary>
        /// Diccionario para almacenar puntuaciones/estadísticas (ej: "Goles": 5, "Asistencias": 2).
        /// </summary>
        public Dictionary<string, int> Puntuacion { get; set; }

        /// <summary>
        /// Diccionario para almacenar sanciones (ej: "Amarillas": 3, "Rojas": 1).
        /// </summary>
        public Dictionary<string, int> Sanciones { get; set; }

        /// <summary>
        /// Indica si el jugador está habilitado para jugar (ej: no está suspendido o lesionado).
        /// </summary>
        public bool Habilitado { get; set; }

        /// <summary>
        /// Constructor que inicializa los diccionarios de Puntuacion y Sanciones,
        /// y establece Habilitado a <c>true</c> por defecto.
        /// </summary>
        public Jugador()
        {

            Puntuacion = new Dictionary<string, int>();
            Sanciones = new Dictionary<string, int>();
            Habilitado = true;
        }



        //Entiendo q esto no deberia estar aca, y con el patron MCV solucionaria esta falta en la arquitectura.
        /// <summary>
        /// Propiedad calculada que devuelve el nombre formateado (Apellido, Nombre).
        /// (Nota: Es lógica de Vista/ViewModel, pero útil tenerla aquí).
        /// </summary>
        public string NombreCompleto
        {
            get { return $"{Apellido}, {Nombre}"; }
        }
    }
}
