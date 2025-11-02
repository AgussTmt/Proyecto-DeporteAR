using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Factory;
using DomainModel;

namespace BLL.Services
{
    internal class ClienteService : IClienteService
    {
        public void Add(Cliente entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    var existente = context.Repositories.ClienteRepository.GetByNumero(entity.Telefono);
                    if (existente != null)
                    {
                        throw new InvalidOperationException($"Ya existe un cliente registrado con el teléfono '{entity.Telefono}'.");
                    }

                    if (entity.IdCliente == Guid.Empty)
                    {
                        entity.IdCliente = Guid.NewGuid();
                    }


                    context.Repositories.ClienteRepository.Add(entity);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                
                return context.Repositories.ClienteRepository.GetAll();
            }
        }

        public Cliente GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.ClienteRepository.GetById(id);
            }
        }

        public void Update(Cliente entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    var existente = context.Repositories.ClienteRepository.GetByNumero(entity.Telefono);
                    if (existente != null && existente.IdCliente != entity.IdCliente)
                    {
                        throw new InvalidOperationException($"El teléfono '{entity.Telefono}' ya está asignado a otro cliente.");
                    }

                    context.Repositories.ClienteRepository.Update(entity);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
