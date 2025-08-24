using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using DomainModel;

namespace BLL.Interfaces
{
    internal interface IFixtureService : IGenericService <Fixture>
    {
        void postergar(Fixture fixture);

        void CargarResul(Fixture fixture);

        List<Fixture> ListarPorRangoTiempo(DateTime dateTime);
    }
}
