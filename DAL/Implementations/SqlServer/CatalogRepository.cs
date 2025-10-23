using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Tools;
using DAL.Interfaces;
using DomainModel.CatalogItems;
using Services.DomainModel;
using System.Data.Common;
using DAL.Implementations.SqlServer.Adapters;
using DAL.Implementations.SqlServer.Helper;

namespace DAL.Implementations.SqlServer
{
    internal class CatalogRepository : SqlTransactRepository, ICatalogRepository
    {
        public CatalogRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {

        }

        public IEnumerable<CatalogItem> GetDeportes()
        {
            var items = new List<CatalogItem>();
            string SelectAllStatement = $"SELECT IdDeporte, Descripcion FROM DbDeporte";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectAllStatement,
                                                                    CommandType.Text,
                                                                    new SqlParameter[] { }))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    CatalogItem item = DbDeporteAdapter.Current.Get(data);
                    items.Add(item);
                }
            }

            return items;
        }
        

        public IEnumerable<CatalogItem> GetEstadosFixture()
        {
            var items = new List<CatalogItem>();
            string SelectAllStatement = $"SELECT IdEstadoFixture, Descripcion FROM DbEstadoFixture";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectAllStatement,
                                                                    CommandType.Text,
                                                                    new SqlParameter[] { }))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    CatalogItem item = DbEstadoFixtureAdapter.Current.Get(data);
                    items.Add(item);
                }
            }

            return items;
        }

        public IEnumerable<CatalogItem> GetFormatos()
        {
            var items = new List<CatalogItem>();
            string SelectAllStatement = $"SELECT IdFormato, Descripcion FROM DbFormato";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectAllStatement,
                                                                    CommandType.Text,
                                                                    new SqlParameter[] { }))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    CatalogItem item = DbFormatoAdapter.Current.Get(data);
                    items.Add(item);
                }
            }

            return items;

        }

    }
}
