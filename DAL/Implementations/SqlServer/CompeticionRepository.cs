using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class CompeticionRepository : ICompeticionRepository
    {
        public void Add(Competicion entity)
        {
            throw new NotImplementedException();
        }

        public void AddEquipo(Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Competicion> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Competicion> GetByClient(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Competicion GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Competicion> GetByTimeAndCancha(Competicion competicion)
        {
            throw new NotImplementedException();
        }

        public List<Competicion> GetWithVacancies()
        {
            throw new NotImplementedException();
        }

        public void RemoveEquipo(Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public void Update(Competicion entity)
        {
            throw new NotImplementedException();
        }
    }
}
