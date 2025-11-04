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
    /// <summary>
    /// Repositorio SQL genérico para leer las "Tablas de Catálogo" (Lookup Tables)
    /// que implementan el patrón <see cref="CatalogItem"/>.
    /// </summary>
    /// <remarks>
    /// Opera dentro de una transacción y conexión SQL existente (Unit of Work).
    /// </remarks>
    internal class CatalogRepository : SqlTransactRepository, ICatalogRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de conexión y transacción de una
        /// Unidad de Trabajo (Unit of Work) existente.
        /// </summary>
        /// <param name="context">La <see cref="SqlConnection"/> activa.</param>
        /// <param name="_transaction">La <see cref="SqlTransaction"/> activa.</param>
        public CatalogRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {

        }

        /// <summary>
        /// Obtiene la lista completa de Deportes (ej: "Fútbol 5", "Tenis")
        /// desde la tabla <c>DbDeporte</c>.
        /// </summary>
        /// <returns>Una colección de <see cref="CatalogItem"/> (específicamente <see cref="DbDeporte"/>).</returns>
        public IEnumerable<CatalogItem> GetDeportes()
        {
            var items = new List<CatalogItem>();
            string sql = "SELECT IdDeporte, Descripcion FROM dbo.DbDeporte ORDER BY Descripcion";


            using (SqlDataReader reader = base.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    // Usa el adapter específico para DbDeporte
                    CatalogItem item = DbDeporteAdapter.Current.Get(data);
                    items.Add(item);
                }
            }
            return items;
        }


        /// <summary>
        /// Obtiene la lista completa de Estados de Fixture (ej: "Pendiente", "Finalizado")
        /// desde la tabla <c>DbEstadoFixture</c>.
        /// </summary>
        /// <returns>Una colección de <see cref="CatalogItem"/> (específicamente <see cref="DbEstadoFixture"/>).</returns>
        public IEnumerable<CatalogItem> GetEstadosFixture()
        {
            var items = new List<CatalogItem>();
            string sql = "SELECT IdEstadoFixture, Descripcion FROM dbo.DbEstadoFixture ORDER BY Descripcion";

            using (SqlDataReader reader = base.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    // Usa el adapter específico para DbEstadoFixture
                    CatalogItem item = DbEstadoFixtureAdapter.Current.Get(data);
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// Obtiene la lista completa de Formatos de Torneo (ej: "Liga", "Torneo")
        /// desde la tabla <c>DbFormato</c>.
        /// </summary>
        /// <returns>Una colección de <see cref="CatalogItem"/> (específicamente <see cref="DbFormato"/>).</returns>
        public IEnumerable<CatalogItem> GetFormatos()
        {
            var items = new List<CatalogItem>();
            string sql = "SELECT IdFormato, Descripcion FROM dbo.DbFormato ORDER BY Descripcion";

            using (SqlDataReader reader = base.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    // Usa el adapter específico para DbFormato
                    CatalogItem item = DbFormatoAdapter.Current.Get(data);

                    items.Add(item);
                }
            }
            return items;
        }
    }

}