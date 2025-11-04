using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CatalogItems
{
    /// <summary>
    /// Representa un ítem de catálogo para un Estado de Fixture (ej: "Pendiente", "Finalizado").
    /// Hereda de <see cref="CatalogItem"/> y reemplaza al <c>enum EstadoFixture</c>.
    /// </summary>
    public class DbEstadoFixture : CatalogItem
    {
        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public DbEstadoFixture()
        {
        }

        /// <summary>
        /// Constructor para inicializar un nuevo Estado de Fixture.
        /// </summary>
        /// <param name="id">El ID único.</param>
        /// <param name="descripcion">La descripción del estado.</param>
        public DbEstadoFixture(Guid id, string descripcion) : base(id, descripcion)
        {
        }
    }
}
