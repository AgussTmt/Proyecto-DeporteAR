using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    /// <summary>
    /// Representa a un Cliente que puede realizar reservas.
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Identificador único del cliente (PK).
        /// </summary>
        public Guid IdCliente { get; set; }

        /// <summary>
        /// Nombre y apellido del cliente.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Número de teléfono de contacto del cliente.
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Dirección de email de contacto del cliente.
        /// </summary>
        public string Email { get; set; }
    }
}
