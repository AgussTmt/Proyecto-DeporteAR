using Services.Dal.Implementations;
using Services.Dal.Interfaces;
using Services.DomainModel;
using Services__ArqBase_.Bll.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services__ArqBase_.Bll
{
    internal class PemisosService : IPermisosService
    {
        private static IFamiliaRepository _familiaRepository;
        private static IJoinRepository<Familia, Patente> _familiaPatenteRepository;
        public PemisosService()
        {
            _familiaRepository = new FamiliaRepository();
            _familiaPatenteRepository = new FamiliaPatenteRepository();
        }

        public void AsignarPatentesAUser(Usuario usuario, List<Patente> patentes)
        {
            throw new NotImplementedException();
        }

        public void AsignarRolAUser(Usuario usuario, List<Familia> familia)
        {
            throw new NotImplementedException();
        }

        public void AñadirPatentesARol(Familia familia, List<Patente> patentes)
        {
            foreach (Patente patente in patentes)
            {
                _familiaPatenteRepository.Add(familia, patente);
            }
        }

        public void crearRol(Familia familia)
        {
            _familiaRepository.Add(familia);
        }

        public void crearRolConPatentes(Familia familia, List<Patente> patentes)
        {
            throw new NotImplementedException();
        }

        public void QuitarPatentesARol(Familia familia, List<Patente> patente)
        {
            throw new NotImplementedException();
        }

        public void QuitarPatentesAUser(Usuario usuario, List<Patente> patentes)
        {
            throw new NotImplementedException();
        }

        public void QuitarRolAUSer(Usuario usuario, List<Familia> familia)
        {
            throw new NotImplementedException();
        }
    }
}
