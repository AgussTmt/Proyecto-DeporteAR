using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Jugador
    {
        public Guid IdEquipo { get; set; }
        public Guid Idjugador { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int CantMvp { get; set; }

        public int PartidosJugados { get; set; }

        public Dictionary<string, int> Puntuacion { get; set; }

        public Dictionary<string, int> Sanciones { get; set; }
        public bool Habilitado { get; set; }

        public Jugador()
        {
            
            Puntuacion = new Dictionary<string, int>();
            Sanciones = new Dictionary<string, int>();
            Habilitado = true;
        }
    }
}
