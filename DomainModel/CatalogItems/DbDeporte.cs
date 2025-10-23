using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CatalogItems
{
    public class DbDeporte : CatalogItem
    {
        public DbDeporte(Guid id, string descripcion) : base(id, descripcion)
        {

        }
        public DbDeporte()
        {
        }

       
    }
}
