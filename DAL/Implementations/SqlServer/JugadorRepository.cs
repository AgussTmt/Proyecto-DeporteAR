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
    internal class JugadorRepository : SqlTransactRepository, IJugadorRepository
    {
        public JugadorRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        public void Add(Jugador entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Jugador> GetAll()
        {
            throw new NotImplementedException();
        }

        public Jugador GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Jugador entity)
        {
            throw new NotImplementedException();
        }
    }
}
