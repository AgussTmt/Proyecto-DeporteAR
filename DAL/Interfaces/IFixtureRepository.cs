using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;
using DomainModel;

namespace DAL.Interfaces
{
    internal interface IFixtureRepository : IGenericRepository <Fixture>
    {
        List<Fixture> GetByCompeticion(Competicion competicion);

        void UpdateFecha(Fixture fixture);

        List<Fixture> GetByTimeRange(DateTime dateTime);
    }
}
