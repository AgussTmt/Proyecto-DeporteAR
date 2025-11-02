using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using DomainModel;

namespace BLL.Interfaces
{
    public interface IFixtureService : IGenericService <Fixture>
    {

        void CargarResul(Fixture fixture, List<Jugador> jugadoresActualizados, bool localAusente, bool visitanteAusente);

        List<Fixture> ListarPorRangoTiempo(DateTime dateTime);

        IEnumerable<Fixture> GetByCompeticion(Guid idCompeticion);
    }
}

