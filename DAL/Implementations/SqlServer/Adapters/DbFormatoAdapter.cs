using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.CatalogItems;

namespace DAL.Implementations.SqlServer.Adapters
{
    internal class DbFormatoAdapter
    {
        private readonly static DbFormatoAdapter _instance = new DbFormatoAdapter();

        public static DbFormatoAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private DbFormatoAdapter()
        {
            //Implent here the initialization of your singleton
        }

        public CatalogItem Get(object[] values)
        {
            CatalogItem item = new DbFormato();
            item.Id = Guid.Parse(values[0].ToString());

            item.Descripcion = values[1].ToString();

            return item;
        }
    }
}