using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Clasificacion
    {
        public Guid IdClasificacion { get; set; }

        public int Derrotas { get; set; }

        public int Empates { get; set; }

        public int GolesAFavor { get; set; }

        public string Equipo { get; set; }

        public int PartidosJugados { get; set; }

        public int Victorias { get; set; }

        public int Puntos { get; set; }
    }
}
