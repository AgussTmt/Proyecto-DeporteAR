using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DAL.Interfaces
{
    public interface IClienteRepository
    {
        void Add(Cliente cliente);
        IEnumerable<Cliente> GetAll();
        Cliente GetById(Guid id);
        Cliente GetByNumero(string Numero);
        void Update(Cliente entity);
    }
}
