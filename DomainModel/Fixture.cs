using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Fixture
    {
        public Guid IdFixture { get; set; }

        public DateTime Horario { get; set; }

        public string  Resultado { get; set; }

        public EstadoFixture Estado { get; set; }
    }

    public enum EstadoFixture
    {
        Pendiente,
        Finalizado,
        Postergado
    }
}
