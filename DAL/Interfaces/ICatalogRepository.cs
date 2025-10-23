using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.CatalogItems;

namespace DAL.Interfaces
{
    internal interface ICatalogRepository
    {
        IEnumerable<CatalogItem> GetDeportes();
        IEnumerable<CatalogItem> GetFormatos();
        IEnumerable<CatalogItem> GetEstadosFixture();
    }
}
