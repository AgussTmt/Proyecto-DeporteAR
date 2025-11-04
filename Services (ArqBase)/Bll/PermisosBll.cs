using Services.Dal.Implementations;
using Services.Dal.Interfaces;
using Services.DomainModel;
using Services.Facade;
using Services__ArqBase_.Bll.Interfaces;
using Services__ArqBase_.Dal.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services__ArqBase_.Bll
{
    /// <summary>
    /// Capa de Lógica de Negocio (BLL) para la gestión de Permisos,
    /// incluyendo Roles (Familias) y Patentes.
    /// Orquesta los diferentes repositorios de datos (DAL).
    /// </summary>
    public class PermisosBll : IPermisosBll
    {

        private readonly ILogger _logger;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PermisosBll"/>,
        /// obteniendo una instancia del logger.
        /// </summary>
        public PermisosBll()
        {
            _logger = LoggerService.GetLogger();
        }

        /// <summary>
        /// Asigna un permiso/rol (ObjSecu) a una entidad principal (ObjMain) de forma genérica.
        /// Utiliza reflexión para instanciar el repositorio de 'Join' adecuado.
        /// </summary>
        /// <typeparam name="T1">El tipo de la entidad principal (ej: Usuario, Familia).</typeparam>
        /// <typeparam name="T2">El tipo de la entidad secundaria (ej: Patente, Familia).</typeparam>
        /// <param name="ObjMain">La instancia de la entidad principal.</param>
        /// <param name="ObjSecu">La instancia de la entidad secundaria a asignar.</param>
        /// <remarks>
        /// ¡PRECAUCIÓN! Este método utiliza reflexión (Reflection) y es sensible a convenciones de nombres.
        /// Asume que existe un repositorio en el ensamblado 'Services.Dal'
        /// cuyo nombre es exactamente 'T1.Name + T2.Name + Repository'.
        /// (ej: T1=Usuario, T2=Patente => 'UsuarioPatenteRepository').
        /// </remarks>
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

        /// <summary>
        /// Obtiene la lista de patentes (hojas) asignadas directamente a una familia (rama).
        /// </summary>
        /// <param name="familia">La familia (rol) a consultar.</param>
        /// <returns>Una lista de <see cref="Patente"/>.</returns>
        public List<Patente> GetPatentesDeFamilia(Familia familia)
        {
            FamiliaPatenteRepository repo = new FamiliaPatenteRepository();
            return repo.GetByObject(familia);
        }

        /// <summary>
        /// Crea un nuevo Rol (Familia) en la base de datos, asignando un nuevo Guid
        /// y calculando su hash de integridad.
        /// </summary>
        /// <param name="familia">El objeto <see cref="Familia"/> con (al menos) el Nombre poblado.</param>
        /// <returns>La <see cref="Familia"/> persistida con su Id y Hash.</returns>
        public Familia CrearRol(Familia familia)
        {
            try
            {
                familia.Id = Guid.NewGuid();
                familia.Habilitado = true;
                familia.VerificadorHash = CalcularHash(familia);
                FamiliaRepository repository = new FamiliaRepository();
                _logger.Information($"Rol {familia.Nombre} creado exitosamente.");
                return repository.Add(familia);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al crear el rol {familia.Nombre}", ex);
                throw;
            }
        }

        /// <summary>
        /// Invierte el estado 'Habilitado' (toggle) de una relación en una tabla de unión
        /// (ej: deshabilitar una patente a un usuario).
        /// </summary>
        /// <typeparam name="T1">El tipo de la entidad principal (ej: Usuario).</typeparam>
        /// <typeparam name="T2">El tipo de la entidad secundaria (ej: Patente).</typeparam>
        /// <param name="ObjMain">La instancia de la entidad principal.</param>
        /// <param name="ObjSecu">La instancia de la entidad secundaria.</param>
        public void CambiarHabilitado<T1, T2>(T1 ObjMain, T2 ObjSecu)
        {
            UpdateGenericRepository repository = new UpdateGenericRepository();
            repository.UpdateHabilitadoJoin(ObjMain, ObjSecu);
        }

        /// <summary>
        /// Obtiene todas las patentes (permisos) disponibles en el sistema.
        /// </summary>
        /// <returns>Lista de <see cref="Patente"/>.</returns>
        public List<Patente> GetPatentes()
        {
            PatenteRepository patenteRepository = new PatenteRepository();
            return patenteRepository.GetAll();
        }

        /// <summary>
        /// Obtiene todas las familias (roles) disponibles en el sistema.
        /// </summary>
        /// <returns>Lista de <see cref="Familia"/>.</returns>
        public List<Familia> GetFamilias()
        {
            FamiliaRepository familiaRepository = new FamiliaRepository();
            return familiaRepository.GetAll();
        }


        /// <summary>
        /// Sincroniza los privilegios (roles y patentes) de un usuario con una nueva lista.
        /// Calcula la diferencia (delta) y aplica los cambios (añade, habilita, deshabilita).
        /// </summary>
        /// <param name="usuario">El <see cref="Usuario"/> a modificar.</param>
        /// <param name="RolPatentes">La lista completa y actualizada de <see cref="Component"/>
        /// (Roles y Patentes) que el usuario DEBE tener.</param>
        /// <remarks>
        /// Lógica de Sincronización:
        /// 1. Añade nuevos permisos/roles (si Habilitado=true).
        /// 2. Habilita/Deshabilita permisos/roles existentes si su estado cambió.
        /// 3. Deshabilita permisos/roles que el usuario tenía pero que no están en la nueva lista.
        /// </remarks>
        public void cambiarPermisosAUsuario(Usuario usuario, List<Component> RolPatentes)
        {
            var patentesActuales = usuario.Privilegios.OfType<Patente>().ToList();
            var familiasActuales = usuario.Privilegios.OfType<Familia>().ToList();

            try
            {
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


                string detalle = $"Patentes antiguas: {String.Join(",", patentesActuales.Where(p => p.Habilitado).Select(p => p.DataKey))} " +
                    $"Roles antiguos: {String.Join(",", familiasActuales.Where(p => p.Habilitado).Select(p => p.Nombre))}" +
                    $"\n" +
                    $"Patentes nuevas: {String.Join(",", RolPatentes.OfType<Patente>().Where(p => p.Habilitado).Select(p => p.DataKey))} " +
                    $"Roles nuevos: {String.Join(",", RolPatentes.OfType<Familia>().Where(p => p.Habilitado).Select(p => p.Nombre))}";
                _logger.Information($"Usuario '{usuario.Nombre}' (ID: {usuario.IdUsuario}) - Permisos actualizados");
                _logger.Information($"Detalle de cambios:\n{detalle}");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al actualizar permisos al usuario {usuario.Nombre}", ex);
                throw;
            }
            

           


        }

        /// <summary>
        /// Sincroniza las patentes asignadas a una familia (rol).
        /// </summary>
        /// <param name="familia">La <see cref="Familia"/> a modificar.</param>
        /// <param name="patentes">La lista de <see cref="Patente"/> que la familia DEBE tener.</param>
        public void CambiarPermisosFamilia(Familia familia, List<Patente> patentes)
        {
            FamiliaPatenteRepository repository = new FamiliaPatenteRepository();
            var patentesActuales = repository.GetByObject(familia);

            try
            {
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

                _logger.Information($"Permisos del rol {familia.Nombre} actualizados correctamente ({patentes.Count} permisos).");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al actualizar permisos a la familia {familia.Nombre}", ex);
                throw;
            }
            

        }

        /// <summary>
        /// Método de ayuda privado para calcular el hash MD5 de una familia.
        /// </summary>
        /// <param name="familia">La instancia de <see cref="Familia"/>.</param>
        /// <returns>Un string que representa el hash MD5 calculado.</returns>
        private string CalcularHash(Familia familia)
        {
            string datosConcatenados = $"{familia.Id}-{familia.Nombre}-{familia.Habilitado}";

            return CryptographyService.HashMd5(datosConcatenados);
        }

        /// <summary>
        /// Verifica la integridad de todos los registros en la tabla [Familia]
        /// comparando sus hashes.
        /// </summary>
        /// <returns>Una lista de strings que describe cada fila corrupta.</returns>
        public List<string> VerificarIntegridadFamilias()
        {
            try
            {
                FamiliaRepository familiaRepository = new FamiliaRepository();
                return familiaRepository.VerificarIntegridadHash();
            }
            catch (Exception ex)
            {
                // Si falla la conexión a la BD, lo reportamos como un error de integridad
                return new List<string> { $"Error fatal al auditar la base de datos: {ex.Message}" };
            }
        }
    }
}
