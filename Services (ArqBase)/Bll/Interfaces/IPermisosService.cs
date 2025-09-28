using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services__ArqBase_.Bll.Interfaces
{
    public interface IPermisosService
    {
        void CrearRol(Familia familia);

        void AsignarPermisos<T1, T2>(T1 ObjMain, T2 ObjSecu);

        void CambiarHabilitado<T1, T2>(T1 ObjMain, T2 ObjSecu);

        public List<Patente> GetPatentes();

        public List<Familia> GetFamilias();

        public void cambiarPermisosAUsuario(Usuario usuario, List<Component> RolPatentes);

        public void CambiarPermisosFamilia(Familia familia, List<Patente> patentes);

    }
}
