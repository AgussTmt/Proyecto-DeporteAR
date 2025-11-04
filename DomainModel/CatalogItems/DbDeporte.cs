using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CatalogItems
{
    /// <summary>
    /// Representa un ítem de catálogo para un Deporte (ej: "Fútbol", "Tenis", "Pádel").
    /// Hereda de <see cref="CatalogItem"/>.
    /// </summary>
    public class DbDeporte : CatalogItem
    {
        /// <summary>
        /// Constructor para inicializar un nuevo Deporte.
        /// </summary>
        /// <param name="id">El ID único.</param>
        /// <param name="descripcion">El nombre del deporte.</param>
        public DbDeporte(Guid id, string descripcion) : base(id, descripcion)
        {

        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public DbDeporte()
        {
        }
    }
}
