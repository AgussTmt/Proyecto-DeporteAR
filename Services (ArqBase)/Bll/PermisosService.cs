using Services.Dal.Implementations;
using Services.Dal.Interfaces;
using Services.DomainModel;
using Services__ArqBase_.Bll.Interfaces;
using Services__ArqBase_.Dal.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services__ArqBase_.Bll
{
    public class PermisosService : IPermisosService
    {

        public PermisosService()
        {

        }
        public void AsignarPermisos<T1, T2>(T1 ObjMain, List<T2> ObjSecu)
        {
            IJoinRepository<T1, T2> repository = null;
            Assembly dalAssembly = typeof(FamiliaRepository).Assembly;

            string NombreRepository = $"{typeof(T1).Name}{typeof(T2).Name}Repository";

            Type tipoRepositorio = dalAssembly.GetTypes()
                .Where(t => t.IsClass && t.Name == NombreRepository)
                .FirstOrDefault();
            if (tipoRepositorio != null)
            {
                //Console.WriteLine(tipoRepositorio.Name);
                repository = Activator.CreateInstance(tipoRepositorio) as IJoinRepository<T1, T2>;
            }

            if (repository != null)
            {
                //Console.WriteLine($"Instancia del repositorio '{repository.GetType().Name}' creada exitosamente.");

                foreach (var obj in ObjSecu)
                {
                    repository.Add(ObjMain, obj);
                }
            }
            else
            {
                Console.WriteLine("No se instancio correctamente");
            }

        }


        public void CrearRol(Familia familia)
        {
            FamiliaRepository repository = new FamiliaRepository();
            repository.Add(familia);
        }

        public void CambiarHabilitado<T1, T2>(T1 ObjMain, List<T2> ObjSecu)
        {
            UpdateGenericRepository repository = new UpdateGenericRepository();

            foreach (var item in ObjSecu)
            {
                repository.UpdateHabilitadoJoin(ObjMain, item);
            }
        }

        public List<Patente> GetPatentes()
        {
            PatenteRepository patenteRepository = new PatenteRepository();
            return patenteRepository.GetAll();
        }

        public List<Familia> GetFamilias()
        {
            FamiliaRepository familiaRepository = new FamiliaRepository();
            return familiaRepository.GetAll(
        }
    }
}
