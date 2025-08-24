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

    }

    public enum EstadoAsistencia
    {
        Pendiente,
        Confirmado,
        Cancelado
    }
}
