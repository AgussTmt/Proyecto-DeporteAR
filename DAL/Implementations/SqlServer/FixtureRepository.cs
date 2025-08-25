using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class FixtureRepository : IFixtureRepository
    {
        public void Add(Fixture entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fixture> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Fixture> GetByCompeticion(Competicion competicion)
        {
            throw new NotImplementedException();
        }

        public Fixture GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Fixture> GetByTimeRange(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void Update(Fixture entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateFecha(Fixture fixture)
        {
            throw new NotImplementedException();
        }
    }
}
