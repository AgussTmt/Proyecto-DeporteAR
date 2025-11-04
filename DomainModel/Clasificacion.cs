using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    /// <summary>
    /// Representa una fila en la tabla de clasificación (tabla de posiciones)
    /// de un torneo o competición. Almacena las estadísticas de un equipo.
    /// </summary>
    public class Clasificacion
    {
        /// <summary>
        /// Identificador único de este registro de clasificación (PK).
        /// </summary>
        public Guid IdClasificacion { get; set; }

        /// <summary>
        /// El número total de derrotas (partidos perdidos) del equipo.
        /// </summary>
        public int Derrotas { get; set; }

        /// <summary>
        /// El número total de empates (partidos empatados) del equipo.
        /// </summary>
        public int Empates { get; set; }

        /// <summary>
        /// El número total de goles marcados por el equipo.
        /// </summary>
        public int GolesAFavor { get; set; }

        /// <summary>
        /// El nombre (o identificador) del equipo al que pertenecen estas estadísticas.
        /// </summary>
        public string Equipo { get; set; }

        /// <summary>
        /// El número total de partidos disputados por el equipo.
        /// </summary>
        public int PartidosJugados { get; set; }

        /// <summary>
        /// El número total de victorias (partidos ganados) del equipo.
        /// </summary>
        public int Victorias { get; set; }

        /// <summary>
        /// El puntaje total acumulado por el equipo (ej: 3 por victoria, 1 por empate).
        /// </summary>
        public int Puntos { get; set; }

        /// <summary>
        /// La clave foránea (FK) que vincula este registro a una <see cref="Competicion"/> específica.
        /// </summary>
        public Guid IdCompeticion { get; set; }
    }
}
