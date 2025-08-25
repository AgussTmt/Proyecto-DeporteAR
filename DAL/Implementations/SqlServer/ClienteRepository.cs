using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class ClienteRepository : IClienteRepository
    {
        public void Add(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Cliente GetByNumero(string Numero)
        {
            throw new NotImplementedException();
        }
    }
}
