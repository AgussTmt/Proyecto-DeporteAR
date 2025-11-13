using Services.Bll;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade
{
    /// <summary>
    /// Fachada (Facade) estática que provee una API simple
    /// para las operaciones de Login y Registro de usuarios.
    /// Delega toda la lógica de negocio a <see cref="UsuarioBll"/>.
    /// </summary>
    public static class LoginService
    {
        /// <summary>
        /// Valida las credenciales de un usuario para el inicio de sesión.
        /// </summary>
        /// <param name="user">El nombre de usuario (o email) a validar.</param>
        /// <param name="password">La contraseña en texto plano a validar.</param>
        /// <returns>El objeto <see cref="Usuario"/> si la validación es exitosa.</returns>
        /// <exception cref="Exception">Propaga las excepciones de negocio desde <see cref="UsuarioBll"/>
        /// (ej: "Usuario o contraseña incorrectos", "Usuario no habilitado").</exception>
        public static Usuario ValidarCredenciales(string user, string password)
        {
            try
            {
                return UsuarioBll.ValidarCredenciales(user, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">El objeto <see cref="Usuario"/> a registrar.</param>
        /// <exception cref="Exception">Propaga las excepciones de negocio desde <see cref="UsuarioBll"/>
        /// (ej: "El usuario no puede ser nulo").</exception>
        public static void RegistrarUsuario(Usuario usuario)
        {
            UsuarioBll.RegistrarUsuario(usuario);
        }
    }
}
