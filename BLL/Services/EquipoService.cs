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
               
                return context.Repositories.EquipoRepository.GetAll();
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
                    // (Opcional: BLL podría verificar si el equipo está en una competición activa antes de deshabilitar)
                    context.Repositories.EquipoRepository.CambiarHabilitado(idEquipo, habilitado);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw; // Rollback automático
                }
            }
        }
    }
}
