using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;
using DomainModel;

namespace DAL.Interfaces
{
    public interface IFixtureRepository : IGenericRepository <Fixture>
    {
        List<Fixture> GetByCompeticion(Competicion competicion);

        void UpdateFecha(Fixture fixture);

        List<Fixture> GetByTimeRange(DateTime dateTime);
        void Delete(Guid id);

        IEnumerable<Fixture> GetByCompeticionPendientes(Guid idCompeticion);

        int CountPartidosPendientes(Guid idCompeticion);
    }
}
