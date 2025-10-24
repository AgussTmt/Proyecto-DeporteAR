using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrones_3parcial.UnitOfWork.Implementaciones.UnitOfWork
{
    internal class UnitOfWorkSqlServer : IUnitOfWork
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessString"].ConnectionString;
        public IUnitOfWorkAdapter Create()
        {
            return new UnitOfWorkSqlServerAdapter(connectionString);
        }
    }
}
