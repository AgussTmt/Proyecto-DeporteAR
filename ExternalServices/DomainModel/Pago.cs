using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    /// Probablemente necesite añadir cosas si es q uso mercado pago. 24/8
    internal class Pago
    {
        public Guid IdPago { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string IdTransaccion { get; set; }

        public int Monto { get; set; }
    }
}
