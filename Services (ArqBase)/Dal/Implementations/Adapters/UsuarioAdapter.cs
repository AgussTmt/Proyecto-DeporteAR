using Services.Dal.Interfaces;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations.Adapters
{
    /// <summary>
    /// Implementación Singleton del <see cref="IAdapter{Usuario}"/> para la entidad <see cref="Usuario"/>.
    /// Se encarga de convertir los datos crudos (ej: de un SqlDataReader) en un objeto de dominio Usuario.
    /// </summary>
    internal class UsuarioAdapter : IAdapter<Usuario>
    {
        #region Singleton
        private readonly static UsuarioAdapter _instance = new UsuarioAdapter();

        /// <summary>
        /// Obtiene la instancia única (Singleton) del adaptador de Usuario.
        /// </summary>
        public static UsuarioAdapter Current
        {
            get
            {
                return _instance;
            }
        }
        /// <summary>
        /// Constructor privado para implementar el patrón Singleton.
        /// </summary>
        private UsuarioAdapter()
        {
            //Implent here the initialization of your singleton
        }

        #endregion

        /// <summary>
        /// Convierte un arreglo de objetos (fila de base de datos) en un objeto de dominio <see cref="Usuario"/>.
        /// </summary>
        /// <param name="values">Un arreglo de objetos (object[]) que contiene los campos del usuario en orden: 
        /// [0] IdUsuario, [1] Email, [2] Password, [3] Nombre, [4] Habilitado, [5] CodigoRecuperacion, [6] CodigoExpiracion.
        /// </param>
        /// <returns>Un objeto <see cref="Usuario"/> completo, incluyendo sus privilegios (Familias y Patentes).</returns>
        public Usuario Get(object[] values)
        {
            Usuario usuario = new Usuario
            (
                Guid.Parse(values[0].ToString()),
                values[1].ToString(),
                values[2].ToString(),
                values[3].ToString(),
                Convert.ToBoolean(values[4].ToString())
            );

            usuario.CodigoRecuperacion = values[5] == DBNull.Value ? null : values[5].ToString();

            usuario.CodigoExpiracion = values[6] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(values[6]);

            usuario.Privilegios = new List<Component>();
            usuario.Privilegios.AddRange(new UsuarioFamiliaRepository().GetByObject(usuario));
           
            usuario.Privilegios.AddRange(new UsuarioPatenteRepository().GetByObject(usuario));

            return usuario;
        }
    }
}
