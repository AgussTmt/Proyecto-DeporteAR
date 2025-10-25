using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class ReservaHistorial
    {
        public Guid IdHistorial { get; set; }
        public Guid IdCanchaHorario { get; set; }
        public Guid? IdCliente { get; set; }
        public DateTime FechaHoraEvento { get; set; }
        public string EstadoAnterior { get; set; } 
        public string EstadoNuevo { get; set; }     
        public string Detalle { get; set; }
    }
}
