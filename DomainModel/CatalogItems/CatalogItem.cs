using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CatalogItems
{
    public abstract class CatalogItem
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public CatalogItem(Guid id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }

        public CatalogItem()
        {
        }

    }
}
