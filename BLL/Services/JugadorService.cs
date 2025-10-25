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
    internal class JugadorService : IJugadorService
    {
        public void Add(Jugador entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    if (entity.IdEquipo == Guid.Empty)
                        throw new InvalidOperationException("El jugador debe tener un IdEquipo asignado.");

                    var equipo = context.Repositories.EquipoRepository.GetById(entity.IdEquipo);
                    if (equipo == null)
                        throw new KeyNotFoundException("El equipo asignado al jugador no existe.");

                    context.Repositories.JugadorRepository.Add(entity);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void CambiarHabilitado(Guid idJugador)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    context.Repositories.JugadorRepository.CambiarHabilitado(idJugador);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw; 
                }
            }
        }

        public IEnumerable<Jugador> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.JugadorRepository.GetAll();
            }
        }

        public Jugador GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.JugadorRepository.GetById(id);
            }
        }

        public IEnumerable<Jugador> GetByEquipo(Guid idEquipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.JugadorRepository.GetByEquipo(idEquipo);
            }
        }

        public void Update(Jugador entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.JugadorRepository.Update(entity);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void AddPuntuacion(Guid idJugador, string tipo, int cantidad)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    
                    var jugador = context.Repositories.JugadorRepository.GetById(idJugador);
                    if (jugador == null)
                        throw new KeyNotFoundException("El jugador no existe.");

                    
                    if (jugador.Puntuacion.ContainsKey(tipo))
                    {
                        jugador.Puntuacion[tipo] += cantidad; 
                    }
                    else
                    {
                        jugador.Puntuacion.Add(tipo, cantidad);
                    }

                    
                    context.Repositories.JugadorRepository.Update(jugador);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void AddSancion(Guid idJugador, string tipo, int cantidad)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                   
                    var jugador = context.Repositories.JugadorRepository.GetById(idJugador);
                    if (jugador == null)
                        throw new KeyNotFoundException("El jugador no existe.");

                    
                    if (jugador.Sanciones.ContainsKey(tipo))
                    {
                        jugador.Sanciones[tipo] += cantidad;
                    }
                    else
                    {
                        jugador.Sanciones.Add(tipo, cantidad);
                    }

                    
                    context.Repositories.JugadorRepository.Update(jugador);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
