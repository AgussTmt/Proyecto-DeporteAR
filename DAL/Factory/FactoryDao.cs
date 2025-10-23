using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Implementations.SqlServer;
using Patrones_3parcial.UnitOfWork;
using Patrones_3parcial.UnitOfWork.Implementaciones.UnitOfWork;

namespace DAL.Factory
{
    public static class FactoryDao
    {
        static string backendType;
        public static IUnitOfWork UnitOfWork { get; private set; }

        static FactoryDao()
        {
            backendType = ConfigurationManager.AppSettings["BackendType"];
            UnitOfWork = new UnitOfWorkSqlServer();
        }
        



    }
}
