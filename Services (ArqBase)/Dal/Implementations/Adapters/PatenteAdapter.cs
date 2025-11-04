using Services.Dal.Interfaces;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations.Adapters
{
    /// <summary>
    /// Implementación Singleton del <see cref="IAdapter{Patente}"/> para la entidad <see cref="Patente"/>.
    /// Se encarga de convertir los datos crudos (ej: de un SqlDataReader) en un objeto de dominio Patente.
    /// </summary>
    internal class PatenteAdapter : IAdapter<Patente>
    {
        #region Singleton
        private readonly static PatenteAdapter _instance = new PatenteAdapter();

        /// <summary>
        /// Obtiene la instancia única (Singleton) del adaptador de Patente.
        /// </summary>
        public static PatenteAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Constructor privado para implementar el patrón Singleton.
        /// </summary>
        private PatenteAdapter()
        {
            //Implent here the initialization of your singleton
        }

        #endregion

        /// <summary>
        /// Convierte un arreglo de objetos (fila de base de datos) en un objeto de dominio <see cref="Patente"/>.
        /// </summary>
        /// <param name="values">Un arreglo de objetos (object[]) que contiene los campos de la patente en orden: 
        /// [0] IdPatente, [1] DataKey, [2] TipoAcceso, [3] Habilitado.</param>
        /// <returns>Un objeto <see cref="Patente"/> mapeado.</returns>
        public Patente Get(object[] values)
        {
            Patente patente = new Patente();
            patente.Id = Guid.Parse(values[0].ToString());
            patente.DataKey = values[1].ToString();
            patente.TipoAcceso = (TipoAcceso)Enum.Parse(typeof(TipoAcceso), values[2].ToString());
            patente.Habilitado = Convert.ToBoolean(values[3]);
            return patente;
        }

    }
}
