using Dal.Interfaces;
using DomainModel;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Implementations.Memory
{
    internal class CustomerRepository : ICustomerRepository
    {
        List<Customer> _list = new List<Customer>();

        public CustomerRepository()
        {
            _list.Add(new Customer()
            {
                IdCustomer = Guid.NewGuid(),
                Name = "Test 1"
            });

            _list.Add(new Customer()
            {
                IdCustomer = Guid.NewGuid(),
                Name = "Test 2"
            });
        }
        public void Add(Customer entity)
        {
            //int id = _list.Count + 1;
            entity.IdCustomer = Guid.NewGuid();
            _list.Add(entity);
        }

        public void Delete(Guid id)
        {
            Customer entity = null;

            foreach (Customer customer in _list)
            {
                if(customer.IdCustomer == id)
                {
                    entity = customer;
                    break;
                }
            }

            _list.Remove(entity);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _list;
        }

        public IEnumerable<Customer> GetByCreationDate(DateTime date)
        {
            //User.GetInstance().Email = "keeke";
            

            throw new NotImplementedException();
        }

        public Customer GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            Customer current = null;

            foreach (Customer customer in _list)
            {
                if (customer.IdCustomer == entity.IdCustomer)
                {
                    current = customer;
                    break;
                }
            }

            current.Name = entity.Name;
            //current.Code = entity.Code;
        }
    }
}
