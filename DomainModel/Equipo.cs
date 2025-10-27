using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Equipo
    {
        public Guid IdEquipo { get; set; }

        public Cliente Capitan { get; set; }

        public List<Jugador> Jugadores { get; set; }

        public string Nombre { get; set; }

        public int CantAusencias { get; set; }

        public DateTime FechaCreacion { get; set; }

        public EstadoAsistencia EstadoProxPartido { get; set; }

        public bool Habilitado { get; set; }

        //Entiendo q esto no deberia estar aca, y con el patron MCV solucionaria esta falta en la arquitectura.
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
