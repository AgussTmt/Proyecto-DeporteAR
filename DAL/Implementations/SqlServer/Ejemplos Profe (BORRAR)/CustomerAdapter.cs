using Dal.Interfaces;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Implementations.SqlServer.Adapters
{

    public sealed class CustomerAdapter : IAdapter<Customer>
    {
        #region Singleton
        private readonly static CustomerAdapter _instance = new CustomerAdapter();

        public static CustomerAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private CustomerAdapter()
        {
            //Implent here the initialization of your singleton
        }

        #endregion

        public Customer Get(object[] values)
        {
            return new Customer()
            {
                IdCustomer = Guid.Parse(values[(int)CustomerFields.IdCustomer].ToString()),
                Name = values[(int)CustomerFields.Name].ToString()
            };

            /* return new Product()
            {
                IdCustomer = values[(int)CustomerFields.IdCustomer] != DBNull.Value ? values[(int)CustomerFields.IdCustomer].ToString() : string.Empty,
                Description = values[(int)CustomerFields.Description] != DBNull.Value ? values[(int)CustomerFields.Description].ToString() : string.Empty,
                Categoria = CategoryRepository.GetById(values[(int)CustomerFields.IdCategory]),
            };*/
        }
    }

    internal enum CustomerFields
    {
        IdCustomer = 0,
        Name = 1
    }
}
