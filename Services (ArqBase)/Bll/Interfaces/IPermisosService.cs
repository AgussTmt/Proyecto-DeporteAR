using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services__ArqBase_.Bll.Interfaces
{
    internal interface IPermisosService
    {
        void crearRol(Familia familia);

        void AsignarPermisos<T1, T2>(T1 ObjMain, List<T2> ObjSecu);

        void QuitarPermisos<T1, T2>(T1 ObjMain, List<T2> ObjSecu);

        
    }
}
