using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.CatalogItems;

namespace DAL.Implementations.SqlServer.Adapters
{
    internal class DbEstadoFixtureAdapter
    {
        private readonly static DbEstadoFixtureAdapter _instance = new DbEstadoFixtureAdapter();

        public static DbEstadoFixtureAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private DbEstadoFixtureAdapter()
        {
            //Implent here the initialization of your singleton
        }

        public CatalogItem Get(object[] values)
        {
            CatalogItem item = new DbEstadoFixture();
            item.Id = Guid.Parse(values[0].ToString());

            item.Descripcion = values[1].ToString();

            return item;
        }
    }
}
