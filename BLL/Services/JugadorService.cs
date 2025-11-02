using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Factory;
using DAL.Interfaces;
using DomainModel;
using Patrones_3parcial.UnitOfWork;

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
                {   //busco el jugador
                    var jugador = context.Repositories.JugadorRepository.GetById(idJugador);
                    if (jugador == null)
                    {
                        // Si ya no existe, o si GetById falló (ej. ya estaba deshabilitado)
                        // intentamos buscarlo incluyendo deshabilitados.
                        jugador = context.Repositories.JugadorRepository.GetAllIncludingDisabled()
                                        .FirstOrDefault(j => j.Idjugador == idJugador);

                        if (jugador == null)
                            throw new KeyNotFoundException("El jugador no fue encontrado.");
                    }

                    bool estaActualmenteHabilitado = jugador.Habilitado;

                    if (estaActualmenteHabilitado)
                    {
                        //deshabilitar
                        // tiene equipo?
                        if (jugador.IdEquipo != null)
                        {
                            throw new InvalidOperationException("Este jugador no se puede deshabilitar porque está asignado a un equipo. Primero debe quitarlo del plantel en la pantalla de 'Gestión de Equipos'.");
                        }

                        //todo ok
                        context.Repositories.JugadorRepository.CambiarHabilitado(idJugador);
                        context.SaveChanges();
                    }
                    else
                    {
                        //habilitar todo ok
                        context.Repositories.JugadorRepository.CambiarHabilitado(idJugador);
                        context.SaveChanges();
                    }
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

        public List<Jugador> TraerJugadoresSinEquipo()
        {
            try
            {
                using (var context = FactoryDao.UnitOfWork.Create())
                {
                    return context.Repositories.JugadorRepository.GetSinEquipo();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BLL al traer jugadores sin equipo.", ex);
            }
        }

        public List<Jugador> GetAllIncludingDisabled()
        {
            try
            {
                using (var context = FactoryDao.UnitOfWork.Create())
                {
                    var jugadores = context.Repositories.JugadorRepository.GetAllIncludingDisabled().ToList();
                    return jugadores;
                }
                   
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BLL al obtener jugadores (incl. deshabilitados)", ex);
            }
        }
    }
}
