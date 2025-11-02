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

        public Equipo TraerPorId(Equipo entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // 1. El repositorio trae el equipo CON los jugadores (¡BIEN!)
                    var equipo = context.Repositories.EquipoRepository.GetById(entity.IdEquipo);

                    if (equipo != null)
                    {
                        equipo.Jugadores = context.Repositories.JugadorRepository.GetByEquipo(equipo.IdEquipo).ToList();
                    }
                    return equipo;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

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
                        
                        return;
                    }
                    //está en un torneo activo?
                    var competicionesDelEquipo = context.Repositories.CompeticionRepository.GetByEquipo(idEquipo);

                    bool estaEnTorneoActivo = competicionesDelEquipo.Any(c =>
                        c.Estado == EstadoCompeticion.ConFixture);

                    if (estaEnTorneoActivo)
                    {
                        throw new InvalidOperationException("Este equipo no se puede deshabilitar porque está participando en una competición activa. Primero debe ser eliminado de la competición.");
                    }

                    // si esta en sin fixture no hay problema
                    var competicionesSinEmpezar = competicionesDelEquipo
                        .Where(c => c.Estado == EstadoCompeticion.SinFixture)
                        .ToList();

                    foreach (var comp in competicionesSinEmpezar)
                    { 
 
                        context.Repositories.CompeticionRepository.RemoveEquipo(comp.IdCompeticion, idEquipo);
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

        public IEnumerable<Equipo> GetAllIncludingDisabled()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                
                return context.Repositories.EquipoRepository.GetAllIncludingDisabled().ToList();
            }
        }


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


                    // Update equipo
                    equipoDb.Nombre = equipoActualizado.Nombre;
                    equipoDb.CantAusencias = equipoActualizado.CantAusencias;
                    equipoDb.EstadoProxPartido = equipoActualizado.EstadoProxPartido;
                    equipoDb.Capitan = equipoActualizado.Capitan; 
                    equipoDb.Habilitado = equipoActualizado.Habilitado; 
 
                    context.Repositories.EquipoRepository.Update(equipoDb);


                    //actualizar jugadores
                    var idsJugadoresActualizados = equipoActualizado.Jugadores.Select(j => j.Idjugador).ToHashSet();
                    var idsJugadoresDb = equipoDb.Jugadores.Select(j => j.Idjugador).ToHashSet();
                    //agregar
                    foreach (var jugadorParaAsignar in equipoActualizado.Jugadores)
                    {
                        if (!idsJugadoresDb.Contains(jugadorParaAsignar.Idjugador))
                        {
                            jugadorParaAsignar.IdEquipo = equipoDb.IdEquipo;
                            context.Repositories.JugadorRepository.Update(jugadorParaAsignar);
                        }

                    }
                    //desvincular jugadores
                    foreach (var jugadorViejo in equipoDb.Jugadores)
                    {
                        
                        if (!idsJugadoresActualizados.Contains(jugadorViejo.Idjugador))
                        {
                            
                            
                            jugadorViejo.IdEquipo = null;
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
