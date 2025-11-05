using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Factory;
using DomainModel.CatalogItems;

namespace BLL.Services
{
    /// <summary>
    /// Capa de Lógica de Negocio (BLL) para obtener las "Tablas de Catálogo".
    /// </summary>
    /// <remarks>
    /// Es una capa fina (thin wrapper) que usa el Unit of Work para
    /// exponer los métodos del <c>CatalogRepository</c>.
    /// Básicamente, es el que le pasa los datos a los ComboBoxes.
    /// </remarks>
    internal class CatalogService : ICatalogService
    {
        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public CatalogService()
        {
        }

        /// <summary>
        /// Obtiene la lista de todos los Deportes (ej: "Fútbol 5", "Tenis").
        /// </summary>
        /// <returns>Una colección de <see cref="CatalogItem"/>.</returns>
        public IEnumerable<CatalogItem> GetDeportes()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                // Solo pasa la llamada al repo
                return context.Repositories.CatalogRepository.GetDeportes();
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los Formatos de Torneo (ej: "Liga", "Torneo").
        /// </summary>
        /// <returns>Una colección de <see cref="CatalogItem"/>.</returns>
        public IEnumerable<CatalogItem> GetFormatos()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CatalogRepository.GetFormatos();
            }
        }

    }
}