using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Factory;
using DomainModel;

namespace BLL.Services
{
    internal class EquipoService : IEquipoService
    {
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

        public void AñadirMiembro(Jugador jugador)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    if (jugador.IdEquipo == Guid.Empty)
                        throw new InvalidOperationException("El jugador debe tener un IdEquipo asignado.");

                    
                    var equipo = context.Repositories.EquipoRepository.GetById(jugador.IdEquipo);
                    if (equipo == null)
                        throw new KeyNotFoundException("El equipo al que se intenta agregar el jugador no existe.");

                    
                    context.Repositories.JugadorRepository.Add(jugador);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

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

                    context.Repositories.EquipoRepository.Add(equipo);

                    
                    if (equipo.Jugadores != null && equipo.Jugadores.Count > 0)
                    {
                        foreach (var jugador in equipo.Jugadores)
                        {
                            
                            jugador.IdEquipo = equipo.IdEquipo;

                            
                            context.Repositories.JugadorRepository.Add(jugador);
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

        public IEnumerable<Equipo> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.EquipoRepository.GetAll().ToList();
            }
            
        }
        

        public List<Equipo> ListarPorCompeticion(Competicion competicion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                
                return context.Repositories.EquipoRepository.GetByCompeticion(competicion);
            }
        }

        public Equipo TraerPorId(Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                
                return context.Repositories.EquipoRepository.GetById(equipo.IdEquipo);
            }
        }

        public void CambiarHabilitado(Guid idEquipo, bool habilitado)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    context.Repositories.EquipoRepository.CambiarHabilitado(idEquipo, habilitado);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public IEnumerable<Equipo> GetAllIncludingDisabled()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                
                return context.Repositories.EquipoRepository.GetAllIncludingDisabled().ToList();
            }
        }


        public void Update(Equipo equipoActualizado)
        {
            // Validaciones básicas
            if (equipoActualizado == null || equipoActualizado.IdEquipo == Guid.Empty)
                throw new ArgumentException("El equipo a actualizar no es válido.");

            // Asegurarse de que la lista de jugadores no sea null
            if (equipoActualizado.Jugadores == null)
                equipoActualizado.Jugadores = new List<Jugador>();

            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // 1. Obtener el estado actual del equipo desde la BD
                    //    Usamos GetById para asegurarnos de traer la lista de jugadores actual
                    var equipoDb = context.Repositories.EquipoRepository.GetById(equipoActualizado.IdEquipo);
                    if (equipoDb == null)
                        throw new KeyNotFoundException($"El equipo con ID {equipoActualizado.IdEquipo} no existe.");

                    // Asegurarse de que la lista de jugadores de la BD no sea null
                    if (equipoDb.Jugadores == null)
                        equipoDb.Jugadores = new List<Jugador>();


                    // 2. Actualizar las propiedades simples del equipo
                    equipoDb.Nombre = equipoActualizado.Nombre;
                    equipoDb.CantAusencias = equipoActualizado.CantAusencias;
                    equipoDb.EstadoProxPartido = equipoActualizado.EstadoProxPartido;
                    equipoDb.Capitan = equipoActualizado.Capitan; 
                    equipoDb.Habilitado = equipoActualizado.Habilitado; 


                    // 3. LLAMAR AL UPDATE DEL REPOSITORIO DE EQUIPO (solo para DbEquipo)
                    context.Repositories.EquipoRepository.Update(equipoDb);


                    // --- 4. LÓGICA PARA ACTUALIZAR JUGADORES ---

                    // IDs de jugadores que vienen del formulario
                    var idsJugadoresActualizados = equipoActualizado.Jugadores.Select(j => j.Idjugador).ToHashSet();
                    // IDs de jugadores actualmente en la BD para este equipo
                    var idsJugadoresDb = equipoDb.Jugadores.Select(j => j.Idjugador).ToHashSet();

                    // 4a. JUGADORES A AGREGAR:
                    //     Iteramos sobre los jugadores que vienen del formulario.
                    foreach (var jugadorNuevo in equipoActualizado.Jugadores)
                    {
                        // Si el ID es nuevo (Guid.Empty) O no estaba antes en la BD para este equipo...
                        if (jugadorNuevo.Idjugador == Guid.Empty || !idsJugadoresDb.Contains(jugadorNuevo.Idjugador))
                        {
                            // ¡Importante! Asignar el IdEquipo correcto
                            jugadorNuevo.IdEquipo = equipoDb.IdEquipo;
                            // Si es realmente nuevo, generar un ID
                            if (jugadorNuevo.Idjugador == Guid.Empty)
                                jugadorNuevo.Idjugador = Guid.NewGuid();

                            // Llamar al Add del JugadorRepository
                            context.Repositories.JugadorRepository.Add(jugadorNuevo);
                        }
                        // Opcional: Podrías añadir lógica aquí para actualizar datos
                        // de jugadores existentes si cambiaste algo en FrmEquipoDetalle
                        // else { context.Repositories.JugadorRepository.Update(jugadorNuevo); }
                    }


                    // 4b. JUGADORES A QUITAR:
                    //     Iteramos sobre los jugadores que estaban en la BD.
                    foreach (var jugadorViejo in equipoDb.Jugadores)
                    {
                        // Si un jugador de la BD NO está en la lista que viene del formulario...
                        if (!idsJugadoresActualizados.Contains(jugadorViejo.Idjugador))
                        {
                            // --- Decisión Importante: ¿Borrado físico o lógico? ---
                            // Opción A: Borrado Físico (lo quita de la tabla DbJugador)
                            context.Repositories.JugadorRepository.CambiarHabilitado(jugadorViejo.Idjugador);

                            // Opción B: Borrado Lógico (requiere modificar JugadorRepository/Service)
                            // jugadorViejo.Habilitado = false; // O un campo similar
                            // context.Repositories.JugadorRepository.Update(jugadorViejo);

                            // Opción C: Desvincular (si un jugador puede estar libre o en otro equipo)
                            // jugadorViejo.IdEquipo = Guid.Empty; // O null si tu BD lo permite
                            // context.Repositories.JugadorRepository.Update(jugadorViejo);
                            // --------------------------------------------------------
                        }
                    }

                    // 5. GUARDAR TODOS LOS CAMBIOS (Equipo y Jugadores)
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
