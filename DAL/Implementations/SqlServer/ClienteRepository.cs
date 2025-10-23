using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Implementations.SqlServer.Helper;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class ClienteRepository : SqlTransactRepository, IClienteRepository
    {
        public ClienteRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

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
