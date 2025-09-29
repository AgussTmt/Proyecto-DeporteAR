using Services.Dal.Implementations;
using Services.Dal.Interfaces;
using Services.DomainModel;
using Services__ArqBase_.Bll.Interfaces;
using Services__ArqBase_.Dal.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services__ArqBase_.Bll
{
    public class PermisosService : IPermisosService
    {

        public PermisosService()
        {

        }
        public void AsignarPermisos<T1, T2>(T1 ObjMain, T2 ObjSecu)
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



                repository.Add(ObjMain, ObjSecu);

            }
            else
            {
                Console.WriteLine("No se instancio correctamente");
            }

        }

        public List<Patente> GetPatentesDeFamilia(Familia familia)
        {
            FamiliaPatenteRepository repo = new FamiliaPatenteRepository();
            return repo.GetByObject(familia);
        }
        public void CrearRol(Familia familia)
        {
            FamiliaRepository repository = new FamiliaRepository();
            repository.Add(familia);
        }

        public void CambiarHabilitado<T1, T2>(T1 ObjMain, T2 ObjSecu)
        {
            UpdateGenericRepository repository = new UpdateGenericRepository();
            repository.UpdateHabilitadoJoin(ObjMain, ObjSecu);
        }


        public List<Patente> GetPatentes()
        {
            PatenteRepository patenteRepository = new PatenteRepository();
            return patenteRepository.GetAll();
        }

        public List<Familia> GetFamilias()
        {
            FamiliaRepository familiaRepository = new FamiliaRepository();
            return familiaRepository.GetAll();
        }

        public void cambiarPermisosAUsuario(Usuario usuario, List<Component> RolPatentes)
        {
            var patentesActuales = usuario.Privilegios.OfType<Patente>().ToList();
            var familiasActuales = usuario.Privilegios.OfType<Familia>().ToList();


            foreach (var item in RolPatentes)
            {
                if (item is Patente)
                {
                    var patente = patentesActuales.FirstOrDefault(p => p.Id == item.Id);


                    if (patente != null && patente.Habilitado != item.Habilitado)
                    {
                        CambiarHabilitado<Usuario, Patente>(usuario, item as Patente);
                    }

                    else if (patente == null && item.Habilitado)
                    {
                        AsignarPermisos<Usuario, Patente>(usuario, item as Patente);
                    }
                }
                else if (item is Familia)
                {
                    var familia = familiasActuales.FirstOrDefault(f => f.Id == item.Id);

                    if (familia != null && familia.Habilitado != item.Habilitado)
                    {
                        CambiarHabilitado<Usuario, Familia>(usuario, item as Familia);
                    }

                    else if (familia == null && item.Habilitado)
                    {
                        AsignarPermisos<Usuario, Familia>(usuario, item as Familia);
                    }
                }
                else
                {
                    throw new Exception("pincho");
                }

            }

            foreach (var patenteActual in patentesActuales.Where(p => p.Habilitado))
            {
                if (!RolPatentes.OfType<Patente>().Any(p => p.Id == patenteActual.Id))
                {
                    CambiarHabilitado<Usuario, Patente>(usuario, patenteActual);
                }
            }

            // Deshabilitar familias que ya no están en la lista
            foreach (var familiaActual in familiasActuales.Where(f => f.Habilitado))
            {
                if (!RolPatentes.OfType<Familia>().Any(f => f.Id == familiaActual.Id))
                {
                    
                    CambiarHabilitado<Usuario, Familia>(usuario, familiaActual);
                }
            }


        }
        public void CambiarPermisosFamilia(Familia familia, List<Patente> patentes)
        {
            FamiliaPatenteRepository repository = new FamiliaPatenteRepository();
            var patentesActuales = repository.GetByObject(familia);

            foreach (var item in patentes)
            {
                var patente = patentesActuales.FirstOrDefault(p => p.Id == item.Id);

                if (patente != null && patente.Habilitado != item.Habilitado)
                {
                    CambiarHabilitado<Familia, Patente>(familia, item);
                }

                else if (patente == null && item.Habilitado)
                {
                    AsignarPermisos<Familia, Patente>(familia, item);
                }

            }

        }
    }
}
