using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Cancha
    {
        public Guid IdCancha { get; set; }

        public int Capacidad { get; set; }

        public string Deporte{ get; set; }

        public int DuracionXPartidoMin{ get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion{ get; set; }

        public string FranjaHoraria { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }
    }

}
