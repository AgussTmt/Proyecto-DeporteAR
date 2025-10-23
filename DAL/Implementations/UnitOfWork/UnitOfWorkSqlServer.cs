using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrones_3parcial.UnitOfWork.Implementaciones.UnitOfWork
{
    internal class UnitOfWorkSqlServer : IUnitOfWork
    {
        string connectionString = "sqlConnString";
        public IUnitOfWorkAdapter Create()
        {
            return new UnitOfWorkSqlServerAdapter(connectionString);
        }
    }
}
