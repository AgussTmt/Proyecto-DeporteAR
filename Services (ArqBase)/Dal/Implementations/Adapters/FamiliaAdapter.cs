using Services.Dal.Interfaces;
using Services.DomainModel;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations.Adapters
{
    /// <summary>
    /// Implementación Singleton del <see cref="IAdapter{Familia}"/> para la entidad <see cref="Familia"/>.
    /// Se encarga de convertir los datos crudos en un objeto <see cref="Familia"/>
    /// y verifica la integridad de los datos (hash) durante la hidratación (mapeo).
    /// </summary>
    internal class FamiliaAdapter : IAdapter<Familia>
    {
        #region Singleton
        private readonly static FamiliaAdapter _instance = new FamiliaAdapter();

        /// <summary>
        /// Obtiene la instancia única (Singleton) del adaptador de Familia.
        /// </summary>
        public static FamiliaAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Constructor privado para implementar el patrón Singleton.
        /// </summary>
        private FamiliaAdapter()
        {
            //Implent here the initialization of your singleton
        }

        #endregion

        /// <summary>
        /// Convierte un arreglo de objetos (fila de base de datos) en un objeto de dominio <see cref="Familia"/>.
        /// Durante el proceso, verifica la integridad del registro comparando el hash almacenado con uno recién calculado.
        /// También carga recursivamente las familias y patentes hijas.
        /// </summary>
        /// <param name="values">Un arreglo de objetos (object[]) que contiene los campos de la familia en orden: 
        /// [0] IdFamilia, [1] Nombre, [2] Habilitado, [3] VerificadorHash.</param>
        /// <returns>Un objeto <see cref="Familia"/> completo, incluyendo sus componentes hijos (otras familias y patentes).</returns>
        /// <exception cref="SecurityException">Se lanza si el hash almacenado en la base de datos no coincide con el hash calculado,
        /// indicando una posible manipulación de datos.</exception>
        public Familia Get(object[] values)
        {
            Familia familia = new Familia();
            familia.Id = Guid.Parse(values[0].ToString());
            familia.Nombre = values[1].ToString();
            familia.Habilitado = Convert.ToBoolean(values[2]);


            string hashGuardado = values[3] == DBNull.Value ? null : values[3].ToString();
            string hashCalculado = CalcularHash(familia);
            if (hashGuardado != hashCalculado)
            {

                throw new SecurityException($"¡Datos corruptos! La fila para la familia '{familia.Nombre}' ha sido manipulada.");
            }

            familia.VerificadorHash = hashGuardado;



            familia.AddRange(new FamiliaFamiliaRepository().GetByObject(familia));

            familia.AddRange(new FamiliaPatenteRepository().GetByObject(familia));

            return familia;
        }


        /// <summary>
        /// Método de ayuda privado para calcular el hash MD5 de una familia.
        /// La fórmula de concatenación debe ser idéntica a la utilizada al guardar
        /// (vista en <c>FamiliaRepository.VerificarIntegridadHash</c>).
        /// </summary>
        /// <param name="familia">La instancia de <see cref="Familia"/> con sus propiedades (Id, Nombre, Habilitado) ya mapeadas.</param>
        /// <returns>Un string que representa el hash MD5 calculado.</returns>
        private string CalcularHash(Familia familia)
        {
            
            string datosConcatenados = $"{familia.Id}-{familia.Nombre}-{familia.Habilitado}";

            return CryptographyService.HashMd5(datosConcatenados);
        }

    }
}
