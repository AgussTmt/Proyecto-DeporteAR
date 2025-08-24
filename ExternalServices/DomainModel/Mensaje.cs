using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    internal class Mensaje
    {
        public Guid IdMensaje { get; set; }

        public string Contenido { get; set; }

        public Guid Destinatario { get; set; }

        public EstadoMensaje Estado { get; set; }

        public DateTime FechaDeEnvio { get; set; }
        public string RespuestaUsuario { get; set; }
    }

    public enum EstadoMensaje
    {
        Enviado,
        Entregado,
        Leído,
        Fallido
    }
}
