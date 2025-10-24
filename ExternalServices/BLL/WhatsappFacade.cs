using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace ExternalServices.BLL
{
    internal class WhatsappFacade
    {
        public void InscribirACompeticion(string telefono, Guid idCompeticion)
        {
            Console.WriteLine("no implementado");
        }

        public void CancelarInscripcion(string telefono, Guid idCompeticion)
        {
            Console.WriteLine("no implementado");
        }

        public void NotificarProximoPartido(Fixture partido, string telefonoCapitan)
        {
            Console.WriteLine("no implementado");
        }

        public void ConsultarTablaDePuntos(string telefono, Guid idCompeticion)
        {
            Console.WriteLine("no implementado");
        }
    }
}
