using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class CanchaHorarioRepository : ICanchaHorarioRepository
    {
        public void Add(CanchaHorario entity)
        {
            throw new NotImplementedException();
        }

        public void AssignCliente(Cliente cliente, CanchaHorario canchaHorario)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CanchaHorario> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<CanchaHorario> GetByCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public List<CanchaHorario> GetByEstadoReserva(EstadoReserva estadoReserva)
        {
            throw new NotImplementedException();
        }

        public CanchaHorario GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<CanchaHorario> GetByTimeRange(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public List<CanchaHorario> GetOrderByDemand()
        {
            throw new NotImplementedException();
        }

        public void Update(CanchaHorario entity)
        {
            throw new NotImplementedException();
        }
    }
}
