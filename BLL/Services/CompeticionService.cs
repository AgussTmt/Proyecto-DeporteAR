using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DomainModel;

namespace BLL.Services
{
    internal class CompeticionService : ICompeticionService
    {
        public void Add(Competicion entity)
        {
            throw new NotImplementedException();
        }

        public void AñadirEquipo(Competicion competicion, Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public void CrearFixture(Competicion competicion)
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

        public Competicion GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Competicion> ListarConVacantes(Competicion competicion)
        {
            throw new NotImplementedException();
        }

        public List<Competicion> ListarPorCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public void QuitarEquipo(Competicion competicion, Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public void Update(Competicion entity)
        {
            throw new NotImplementedException();
        }
    }
}
