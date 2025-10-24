using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Factory;
using DomainModel.CatalogItems;

namespace BLL.Services
{
    
        internal class CatalogService : ICatalogService
        {
            

            public CatalogService()
            {
            }

            
            public IEnumerable<CatalogItem> GetDeportes()
            {
                using (var context = FactoryDao.UnitOfWork.Create())
                {
                    
                    return context.Repositories.CatalogRepository.GetDeportes();
                }
            }

            public IEnumerable<CatalogItem> GetFormatos()
            {
                using (var context = FactoryDao.UnitOfWork.Create())
                {
                    return context.Repositories.CatalogRepository.GetFormatos();
                }
            }

            public IEnumerable<CatalogItem> GetEstadosFixture()
            {
                using (var context = FactoryDao.UnitOfWork.Create())
                {
                    return context.Repositories.CatalogRepository.GetEstadosFixture();
                }
            }
            
        }
}

