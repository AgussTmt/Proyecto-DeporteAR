using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Factory;
using DomainModel;

namespace BLL.Services
{
    /// <summary>
    /// Capa de Lógica de Negocio (BLL) para el ABM (CRUD) de Equipos.
    /// </summary>
    /// <remarks>
    /// Se encarga de la lógica pesada, como la sincronización de jugadores
    /// (agregar/quitar) y el borrado seguro (chequeando torneos).
    /// </remarks>
    internal class EquipoService : IEquipoService
    {
        /// <summary>
        /// Incrementa el contador de ausencias de un equipo en 1.
        /// </summary>
        /// <param name="equipo">El equipo (stub) al que se le sumará la ausencia.</param>
        public void AñadirAusencia(Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    var equipoDb = context.Repositories.EquipoRepository.GetById(equipo.IdEquipo);
                    if (equipoDb == null)
                        throw new KeyNotFoundException("El equipo no existe.");


                    equipoDb.CantAusencias += 1;


                    context.Repositories.EquipoRepository.Update(equipoDb);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// Cambia el estado de asistencia de un equipo para su próximo partido.
        /// </summary>
        /// <param name="estadoAsistencia">El nuevo estado (ej: Confirmado, Pendiente).</param>
        /// <param name="equipo">El equipo (stub) a modificar.</param>
        public void CambiarEstadoAsistencia(EstadoAsistencia estadoAsistencia, Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    var equipoDb = context.Repositories.EquipoRepository.GetById(equipo.IdEquipo);
                    if (equipoDb == null)
                        throw new KeyNotFoundException("El equipo no existe.");


                    equipoDb.EstadoProxPartido = estadoAsistencia;


                    context.Repositories.EquipoRepository.Update(equipoDb);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Crea un nuevo equipo y, opcionalmente, le asigna jugadores existentes.
        /// </summary>
        /// <param name="equipo">La entidad <see cref="Equipo"/> a crear.</param>
        /// <remarks>
        /// Es transaccional. Primero crea el equipo (Add) y luego, si la lista
        /// <c>Jugadores</c> no está vacía, recorre esos jugadores y les
        /// actualiza el <c>IdEquipo</c> (Update).
        /// </remarks>
        public void Crear(Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    if (equipo.IdEquipo == Guid.Empty)
                    {
                        equipo.IdEquipo = Guid.NewGuid();
                    }

                    equipo.FechaCreacion = DateTime.Now;
                    equipo.Habilitado = true;
                    // 1. Crea el equipo
                    context.Repositories.EquipoRepository.Add(equipo);

                    // 2. Asigna los jugadores (si los hay)
                    if (equipo.Jugadores != null && equipo.Jugadores.Count > 0)
                    {
                        foreach (var jugador in equipo.Jugadores)
                        {
                            jugador.IdEquipo = equipo.IdEquipo;
                            // ¡Ojo! Esto llama a JugadorRepository.Update
                            // que a su vez hace SyncPuntuacion y SyncSanciones.
                            // Si creás un equipo con 15 jugadores, esto va a
                            // hacer un montón de consultas (delete/insert).
                            context.Repositories.JugadorRepository.Update(jugador);
                        }
                    }

                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de todos los equipos HABILITADOS.
        /// </summary>
        /// <returns>Colección de <see cref="Equipo"/>.</returns>
        public IEnumerable<Equipo> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.EquipoRepository.GetAll().ToList();
            }

        }


        /// <summary>
        /// Obtiene la lista de equipos inscritos en una competición específica.
        /// </summary>
        /// <param name="competicion">La <see cref="Competicion"/> a consultar.</param>
        /// <returns>Lista de <see cref="Equipo"/>.</returns>

        public List<Equipo> ListarPorCompeticion(Competicion competicion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.EquipoRepository.GetByCompeticion(competicion);
            }
        }

        /// <summary>
        /// Obtiene un equipo específico por su ID, hidratado con sus jugadores.
        /// </summary>
        /// <param name="entity">El equipo (stub) con el ID a buscar.</param>
        /// <returns>El <see cref="Equipo"/> completo.</returns>
        public Equipo TraerPorId(Equipo entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    var equipo = context.Repositories.EquipoRepository.GetById(entity.IdEquipo);
                    return equipo;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Habilita o deshabilita un equipo, con validaciones de negocio.
        /// </summary>
        /// <param name="idEquipo">El ID del equipo.</param>
        /// <param name="habilitado">El nuevo estado (true para habilitar, false para deshabilitar).</param>
        /// <exception cref="InvalidOperationException">Si se intenta deshabilitar un equipo en un torneo activo.</exception>
        /// <remarks>
        /// Lógica de "borrado seguro" muy potente.
        /// - Si es para HABILITAR: Lo habilita sin preguntar.
        /// - Si es para DESHABILITAR:
        ///   1. Chequea si está en un torneo 'ConFixture'. Si es así, explota.
        ///   2. Chequea si está en un torneo 'SinFixture'. Si es así, lo saca
        ///      del torneo (RemoveEquipo) y lo borra de la tabla de posiciones (Delete Clasificacion).
        ///   3. Recién ahí lo deshabilita.
        /// </remarks>
        public void CambiarHabilitado(Guid idEquipo, bool habilitado)
        {
            // Solo valido al deshabilitar
            if (habilitado == false)
            {
                using (var context = FactoryDao.UnitOfWork.Create())
                {
                    //hidrato el equipo
                    var equipoCompleto = context.Repositories.EquipoRepository.GetById(idEquipo);
                    if (equipoCompleto == null)
                    {

                        return; // No existe, no hago nada
                    }
                    // BLL Check 1: ¿está en un torneo activo?
                    var competicionesDelEquipo = context.Repositories.CompeticionRepository.GetByEquipo(idEquipo);

                    bool estaEnTorneoActivo = competicionesDelEquipo.Any(c =>
                        c.Estado == EstadoCompeticion.ConFixture);

                    if (estaEnTorneoActivo)
                    {
                        throw new InvalidOperationException("Este equipo no se puede deshabilitar porque está participando en una competición activa. Primero debe ser eliminado de la competición.");
                    }

                    // BLL Check 2: ¿esta en un torneo sin empezar?
                    var competicionesSinEmpezar = competicionesDelEquipo
                        .Where(c => c.Estado == EstadoCompeticion.SinFixture)
                        .ToList();

                    // Si estaba anotado a torneos que no empezaron, lo damos de baja
                    foreach (var comp in competicionesSinEmpezar)
                    {
                        // Lo sacamos de la N:N (EquipoCompeticion)
                        context.Repositories.CompeticionRepository.RemoveEquipo(comp.IdCompeticion, idEquipo);
                        // Lo sacamos de la tabla de posiciones
                        var clasif = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(comp, new Equipo { IdEquipo = idEquipo });
                        if (clasif != null)
                            context.Repositories.ClasificacionRepository.Delete(clasif.IdClasificacion);
                    }

                    //todo ok
                    context.Repositories.EquipoRepository.CambiarHabilitado(idEquipo, false);
                    context.SaveChanges();
                }
            }
            else
            {
                //para habilitar no valido
                using (var context = FactoryDao.UnitOfWork.Create())
                {
                    context.Repositories.EquipoRepository.CambiarHabilitado(idEquipo, true);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de TODOS los equipos (habilitados y deshabilitados).
        /// </summary>
        /// <returns>Colección de <see cref="Equipo"/>.</returns>
        public IEnumerable<Equipo> GetAllIncludingDisabled()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.EquipoRepository.GetAllIncludingDisabled().ToList();
            }
        }


        /// <summary>
        /// Actualiza un equipo existente y sincroniza su lista de jugadores.
        /// </summary>
        /// <param name="equipoActualizado">El <see cref="Equipo"/> con los datos nuevos.</param>
        /// <remarks>
        /// Lógica de Sincronización (Delta):
        /// 1. Actualiza los datos del equipo (Nombre, Capitán, etc.).
        /// 2. Compara la lista de jugadores nueva con la vieja.
        /// 3. AGREGAR: Si un jugador está en la lista nueva pero no en la vieja,
        ///    le actualiza el <c>IdEquipo</c> (lo asigna al equipo).
        /// 4. QUITAR: Si un jugador está en la lista vieja pero no en la nueva,
        ///    le setea <c>IdEquipo = null</c> (lo desvincula, lo deja "libre").
        /// </remarks>
        public void Update(Equipo equipoActualizado)
        {
            if (equipoActualizado == null || equipoActualizado.IdEquipo == Guid.Empty)
                throw new ArgumentException("El equipo a actualizar no es válido.");

            if (equipoActualizado.Jugadores == null)
                equipoActualizado.Jugadores = new List<Jugador>();

            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    var equipoDb = context.Repositories.EquipoRepository.GetById(equipoActualizado.IdEquipo);
                    if (equipoDb == null)
                        throw new KeyNotFoundException($"El equipo con ID {equipoActualizado.IdEquipo} no existe.");

                    //valido q jugadores no sea null
                    if (equipoDb.Jugadores == null)
                        equipoDb.Jugadores = new List<Jugador>();


                    // 1. Update de las propiedades del equipo
                    equipoDb.Nombre = equipoActualizado.Nombre;
                    equipoDb.CantAusencias = equipoActualizado.CantAusencias;
                    equipoDb.EstadoProxPartido = equipoActualizado.EstadoProxPartido;
                    equipoDb.Capitan = equipoActualizado.Capitan;
                    equipoDb.Habilitado = equipoActualizado.Habilitado;

                    context.Repositories.EquipoRepository.Update(equipoDb);


                    // 2. Sincronización de Jugadores (Delta)
                    var idsJugadoresActualizados = equipoActualizado.Jugadores.Select(j => j.Idjugador).ToHashSet();
                    var idsJugadoresDb = equipoDb.Jugadores.Select(j => j.Idjugador).ToHashSet();

                    // 2a. AGREGAR (Asignar jugadores al equipo)
                    foreach (var jugadorParaAsignar in equipoActualizado.Jugadores)
                    {
                        if (!idsJugadoresDb.Contains(jugadorParaAsignar.Idjugador))
                        {
                            // Traemos el jugador "posta" para actualizarlo
                            var jugadorDb = context.Repositories.JugadorRepository.GetById(jugadorParaAsignar.Idjugador);
                            if (jugadorDb != null)
                            {
                                jugadorDb.IdEquipo = equipoDb.IdEquipo;
                                context.Repositories.JugadorRepository.Update(jugadorDb);
                            }
                        }

                    }
                    // 2b. QUITAR (Desvincular jugadores del equipo)
                    foreach (var jugadorViejo in equipoDb.Jugadores)
                    {
                        if (!idsJugadoresActualizados.Contains(jugadorViejo.Idjugador))
                        {
                            // El 'jugadorViejo' ya es el objeto completo de la BD
                            jugadorViejo.IdEquipo = null; // Lo dejamos "libre"
                            context.Repositories.JugadorRepository.Update(jugadorViejo);
                        }
                    }
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }


        }


    }
}