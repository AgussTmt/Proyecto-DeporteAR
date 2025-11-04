using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Bll;

namespace Services__ArqBase_.Facade
{
    /// <summary>
    /// Fachada (Facade) estática que provee una API simple
    /// para el proceso de recuperación y reseteo de contraseñas.
    /// Delega toda la lógica de negocio a <see cref="UsuarioBll"/>.
    /// </summary>
    public static class PassRecoveryService
    {
        /// <summary>
        /// Inicia el proceso de recuperación de contraseña para un email dado.
        /// Genera un código, lo guarda en la BD y envía un email al usuario.
        /// </summary>
        /// <param name="email">El email del usuario que solicita la recuperación.</param>
        /// <returns>Booleano indicando el éxito (ver <see cref="UsuarioBll.SolicitarRecuperacion"/> para detalles de seguridad).</returns>
        public static bool SolicitarRecuperacion(string email)
        {
            return UsuarioBll.SolicitarRecuperacion(email);
        }

        /// <summary>
        /// Completa el proceso de reseteo de contraseña validando el código y la expiración.
        /// </summary>
        /// <param name="email">El email del usuario.</param>
        /// <param name="codigo">El código de 6 dígitos enviado por correo.</param>
        /// <param name="nuevoPassword">La nueva contraseña en **texto plano**.</param>
        public static void ResetearPassword(string email, string codigo, string nuevoPassword)
        {
            UsuarioBll.ResetearPassword(email, codigo, nuevoPassword);
        }
    }
}
