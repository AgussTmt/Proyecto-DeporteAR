using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Cliente
    {

        public Guid IdCliente { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }
    }
}
