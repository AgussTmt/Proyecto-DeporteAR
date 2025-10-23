using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.CatalogItems;
using Services.DomainModel;

namespace DAL.Implementations.SqlServer.Adapters
{
    internal class DbDeporteAdapter
    {
        private readonly static DbDeporteAdapter _instance = new DbDeporteAdapter();

        public static DbDeporteAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private DbDeporteAdapter()
        {
            //Implent here the initialization of your singleton
        }

        public CatalogItem Get(object[] values)
        {
            CatalogItem item = new DbDeporte();
            item.Id = Guid.Parse(values[0].ToString());

            item.Descripcion = values[1].ToString();

            return item;

        }
    }
}
