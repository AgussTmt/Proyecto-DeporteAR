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
        void AñadirPatentesARol(Familia familia, List<Patente> patentes);

        void QuitarPatentesARol(Familia familia, List<Patente> patente);

        void AsignarPatentesAUser(Usuario usuario, List<Patente> patentes);

        void AsignarRolAUser(Usuario usuario, List<Familia> familia);

        void QuitarRolAUSer(Usuario usuario, List<Familia> familia);

        void QuitarPatentesAUser(Usuario usuario,  List<Patente> patentes);

        
    }
}
