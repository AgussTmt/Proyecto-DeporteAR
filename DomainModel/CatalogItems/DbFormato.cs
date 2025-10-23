using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CatalogItems
{
    public class DbFormato : CatalogItem
    {
        public DbFormato()
        {
        }

        public DbFormato(Guid id, string descripcion) : base(id, descripcion)
        {
        }
    }
}
