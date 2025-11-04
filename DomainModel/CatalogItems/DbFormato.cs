using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CatalogItems
{
    /// <summary>
    /// Representa un ítem de catálogo para un Formato de Competición (ej: "Liga", "Torneo").
    /// Hereda de <see cref="CatalogItem"/> y reemplaza al <c>enum FormatoEnum</c>.
    /// </summary>
    public class DbFormato : CatalogItem
    {
        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public DbFormato()
        {
        }

        /// <summary>
        /// Constructor para inicializar un nuevo Formato.
        /// </summary>
        /// <param name="id">El ID único.</param>
        /// <param name="descripcion">La descripción del formato.</param>
        public DbFormato(Guid id, string descripcion) : base(id, descripcion)
        {
        }
    }
}
