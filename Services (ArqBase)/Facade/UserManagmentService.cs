using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Bll;
using Services.DomainModel;
using Services__ArqBase_.Bll;
using Services__ArqBase_.Bll.Interfaces;

namespace Services__ArqBase_.Facade
{
    /// <summary>
    /// Fachada (Facade) estática que centraliza la lógica de negocio (BLL)
    /// para la administración (ABM) de Usuarios, Roles (Familias) y Permisos (Patentes).
    /// </summary>
    public static class UserManagmentService
    {
        private static readonly IPermisosBll _permisosBll = new PermisosBll();

        /// <summary>
        /// Obtiene una lista de todos los usuarios registrados en el sistema.
        /// </summary>
        /// <returns>Una <see cref="List{Usuario}"/>.</returns>
        public static List<Usuario> TraerUsuarios()
        {
            return UsuarioBll.TraerUsuarios();
        }

        /// <summary>
        /// Obtiene todas las familias (roles) disponibles en el sistema.
        /// </summary>
        /// <returns>Lista de <see cref="Familia"/>.</returns>
        public static List<Familia> TraerFamilias()
        {
            return _permisosBll.GetFamilias();
        }

        /// <summary>
        /// Obtiene todas las patentes (permisos) disponibles en el sistema.
        /// </summary>
        /// <returns>Lista de <see cref="Patente"/>.</returns>
        public static List<Patente> traerPatentes()
        {
            return _permisosBll.GetPatentes();
        }

        /// <summary>
        /// Sincroniza los privilegios (roles y patentes) de un usuario con una nueva lista.
        /// (Ver <see cref="PermisosBll.cambiarPermisosAUsuario"/> para la lógica de 'delta').
        /// </summary>
        /// <param name="user">El <see cref="Usuario"/> a modificar.</param>
        /// <param name="permisos">La lista completa y actualizada de <see cref="Component"/> que el usuario DEBE tener.</param>
        public static void CambiarPermisosAUsuario(Usuario user, List<Component> permisos)
        {
            _permisosBll.cambiarPermisosAUsuario(user, permisos);
        }


        /// <summary>
        /// Obtiene un usuario específico por su identificador único.
        /// </summary>
        /// <param name="id">El <see cref="Guid"/> del usuario.</param>
        /// <returns>El <see cref="Usuario"/> encontrado o <c>null</c>.</returns>
        public static Usuario TraerUsuarioPorId(Guid id)
        {
            return UsuarioBll.GetById(id);
        }


        /// <summary>
        /// Crea un nuevo Rol (Familia) en la base de datos.
        /// </summary>
        /// <param name="familia">El objeto <see cref="Familia"/> con (al menos) el Nombre poblado.</param>
        /// <returns>La <see cref="Familia"/> persistida con su Id y Hash.</returns>
        public static Familia CrearRol(Familia familia)
        {
            return _permisosBll.CrearRol(familia);
        }

        /// <summary>
        /// Obtiene la lista de patentes (hojas) asignadas directamente a una familia (rama).
        /// </summary>
        /// <param name="familia">La familia (rol) a consultar.</param>
        /// <returns>Una lista de <see cref="Patente"/>.</returns>
        public static List<Patente> TraerPatentesDeFamilia(Familia familia)
        {
            return _permisosBll.GetPatentesDeFamilia(familia);
        }


        /// <summary>
        /// Sincroniza las patentes asignadas a una familia (rol).
        /// </summary>
        /// <param name="nuevaFamilia">La <see cref="Familia"/> a modificar.</param>
        /// <param name="patentes">La lista de <see cref="Patente"/> que la familia DEBE tener.</param>
        /// <remarks>Ver <see cref="PermisosBll.CambiarPermisosFamilia(Familia, List{Patente})"/> para detalles.</remarks>
        public static void CambiarPermisosFamilia(Familia nuevaFamilia, List<Patente> patentes)
        {
            _permisosBll.CambiarPermisosFamilia(nuevaFamilia, patentes);
        }

        /// <summary>
        /// Obtiene un usuario específico por su dirección de email.
        /// </summary>
        /// <param name="email">El email a buscar.</param>
        /// <returns>El <see cref="Usuario"/> encontrado o <c>null</c>.</returns>
        public static Usuario GetByEmail(string email)
        {
           return UsuarioBll.GetByEmail(email);
        }
    }
}
