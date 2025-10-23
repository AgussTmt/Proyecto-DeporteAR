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
    public static class UserManagmentService
    {
        private static readonly IPermisosBll _permisosBll = new PermisosBll();


        public static List<Usuario> TraerUsuarios()
        {
            return UsuarioBll.TraerUsuarios();
        }

        public static List<Familia> TraerFamilias()
        {
            return _permisosBll.GetFamilias();
        }

        public static List<Patente> traerPatentes()
        {
            return _permisosBll.GetPatentes();
        }

        public static void CambiarPermisosAUsuario(Usuario user, List<Component> permisos)
        {
            _permisosBll.cambiarPermisosAUsuario(user, permisos);
        }

        public static Usuario TraerUsuarioPorId(Guid id)
        {
            return UsuarioBll.GetById(id);
        }

        public static Familia CrearRol(Familia familia)
        {
            return _permisosBll.CrearRol(familia);
        }

        public static void AsignarPermisos<T, Y>(T obj1, Y obj2)
        {
            _permisosBll.AsignarPermisos<T, Y>(obj1, obj2);
        }

        public static List<Patente> TraerPatentesDeFamilia(Familia familia)
        {
            return _permisosBll.GetPatentesDeFamilia(familia);
        }

        public static void CambiarPermisosFamilia(Familia nuevaFamilia, List<Patente> patentes)
        {
            _permisosBll.CambiarPermisosFamilia(nuevaFamilia, patentes);
        }

        public static Usuario GetByEmail(string email)
        {
           return UsuarioBll.GetByEmail(email);
        }
    }
}
