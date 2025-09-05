using Services.Dal.Implementations;
using Services.Dal.Interfaces;
using Services.DomainModel;
using Services__ArqBase_.Bll.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services__ArqBase_.Bll
{
    internal class PemisosService : IPermisosService
    {

        public PemisosService()
        {

        }
        public void AsignarPermisos<T1, T2>(T1 ObjMain, List<T2> ObjSecu)
        {
            IJoinRepository<T1, T2> repository = null;
            Assembly dalAssembly = typeof(FamiliaRepository).Assembly;

            //Type[] typeRepo = dalAssembly.GetTypes();

            string NombreRepository = $"{typeof(T1).Name}{typeof(T2).Name}Repository";

            Type tipoRepositorio = dalAssembly.GetTypes()
                .Where(t => t.IsClass && t.Name == NombreRepository)
                .FirstOrDefault();
            if (tipoRepositorio != null)
            {
                Console.WriteLine(tipoRepositorio.Name);
                repository = Activator.CreateInstance(tipoRepositorio) as IJoinRepository<T1, T2>;
            }

            if (repository != null)
            {
                Console.WriteLine($"Instancia del repositorio '{repository.GetType().Name}' creada exitosamente.");

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


        public void crearRol(Familia familia)
        {
        }

        public void QuitarPermisos<T1, T2>(T1 ObjMain, List<T2> ObjSecu)
        {
            throw new NotImplementedException();
        }


    }
}
