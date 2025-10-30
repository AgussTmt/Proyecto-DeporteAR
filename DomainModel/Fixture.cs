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

        public Guid IdCompeticion { get; set; }

        public List<Equipo> Equipos { get; set; }

        public Fixture()
        {
            Equipos = new List<Equipo>();
        }
    }

    public enum EstadoFixture
    {
        Pendiente,
        Finalizado,
        Postergado

    }
}
