using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Patrones_3parcial.UnitOfWork.Implementaciones.UnitOfWork
{
    internal class UnitOfWorkSqlServerAdapter : IUnitOfWorkAdapter
    {
       

        private SqlConnection _context { get; set; }

        private SqlTransaction _transaction { get; set; }

        public IUnitOfWorkRepository Repositories { get; set; }
        public UnitOfWorkSqlServerAdapter(string connectionString)
        {
            _context = new SqlConnection(connectionString);
            _context.Open();

            _transaction = _context.BeginTransaction();

            Repositories = new UnitOfWorkSqlServerRepository(_context, _transaction);
        }

        public IUnitOfWorkRepository Repository => throw new NotImplementedException();

        public void Dispose()
        {
            if (_transaction is not null)
            {
                _transaction.Dispose();
            }

            if (_context is not null)
            {
                _context.Close();
                _context.Dispose();
            }

            Repositories = null;
        }

        public void SaveChanges()
        {

            _transaction.Commit();
        }
    }
}
