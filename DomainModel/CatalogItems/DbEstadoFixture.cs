using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CatalogItems
{
    public class DbEstadoFixture : CatalogItem
    {
        public DbEstadoFixture()
        {
        }

        public DbEstadoFixture(Guid id, string descripcion) : base(id, descripcion)
        {
        }
    }
}
