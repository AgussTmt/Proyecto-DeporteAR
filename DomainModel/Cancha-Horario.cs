using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class CanchaHorario
    {
        public Guid IdCanchaHorario { get; set; }

        public bool Abonada { get; set; }

        public DateTime FechaHorario { get; set; }

        public bool FueCambiada { get; set; }

        public Cliente ReservadaPor { get; set; }

        public EstadoReserva Estado{ get; set; }

        public Cancha Cancha { get; set; }


    }

    public enum EstadoReserva
    {
        Libre,
        Espera,
        Reservada,
        Cancelada,
        OcupadoPorTorneo
    }
}
