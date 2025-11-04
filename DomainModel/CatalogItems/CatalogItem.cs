using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CatalogItems
{
    /// <summary>
    /// Clase base abstracta para el patrón "Type Object" o "Catálogo".
    /// Define una entidad genérica que consiste en un ID y una Descripción,
    /// usada para poblar tablas de "lookup" (ej: Tipos de Deporte, Formatos).
    /// </summary>
    public abstract class CatalogItem
    {
        /// <summary>
        /// Identificador único (PK) del ítem de catálogo.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// El texto descriptivo, legible por humanos (ej: "Fútbol 5", "Liga").
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Constructor para inicializar un ítem de catálogo con sus valores.
        /// </summary>
        /// <param name="id">El ID único.</param>
        /// <param name="descripcion">El texto descriptivo.</param>
        public CatalogItem(Guid id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public CatalogItem()
        {
        }
    }
}
