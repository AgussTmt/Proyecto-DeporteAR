using Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Factory
{
    public static class Repository
    {
        static string backendType = ConfigurationManager.AppSettings["backendType"];

        public static ICustomerRepository GetCustomerInstance()
        {
            if (backendType == "memory")
            {
                return new Dal.Implementations.Memory.CustomerRepository();
            }
            else if (backendType == "sqlserver")
            {
                return new Dal.Implementations.SqlServer.CustomerRepository();
            }
            throw new Exception("PROBLEMAS");
        }

        public static IProductRepository GetProductInstance()
        {
            if (backendType == "memory")
            {
                return new Dal.Implementations.Memory.ProductRepository();
            }
            else if (backendType == "sqlserver")
            {
                return new Dal.Implementations.SqlServer.ProductRepository();
            }
            throw new Exception("PROBLEMAS");
        }
    }
}
