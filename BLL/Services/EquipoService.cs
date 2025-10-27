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
