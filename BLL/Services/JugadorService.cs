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
    /// <summary>
    /// Capa de Lógica de Negocio (BLL) para el ABM (CRUD) de Jugadores.
    /// </summary>
    internal class JugadorService : IJugadorService
    {
        /// <summary>
        /// Agrega un nuevo Jugador a la base de datos (y sus stats).
        /// </summary>
        /// <param name="entity">El <see cref="Jugador"/> a insertar.</param>
        /// <remarks>
        /// Es un 'pass-through'. Solo llama al <c>JugadorRepository.Add</c>,
        /// que es el que hace el <c>delete-then-insert</c> de Puntuacion y Sanciones.
        /// </remarks>
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

        /// <summary>
        /// Habilita o deshabilita un jugador, con validaciones de negocio.
        /// </summary>
        /// <param name="idJugador">El ID del jugador a modificar.</param>
        /// <exception cref="InvalidOperationException">Si se intenta deshabilitar un jugador que pertenece a un equipo.</exception>
        /// <remarks>
        /// Esta es la lógica de "borrado seguro" para jugadores.
        /// - Si es para HABILITAR: Lo habilita sin chistar.
        /// - Si es para DESHABILITAR: Se fija si el pibe está en un equipo
        ///   (<c>IdEquipo != null</c>). Si está, te frena el carro y te
        ///   tira una excepción.
        /// </remarks>
        public void CambiarHabilitado(Guid idJugador)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {   //busco el jugador
                    var jugador = context.Repositories.JugadorRepository.GetById(idJugador);
                    if (jugador == null)
                    {
                        // Si no lo encuentra (porque GetById solo trae habilitados),
                        // lo busca en la lista completa.
                        jugador = context.Repositories.JugadorRepository.GetAllIncludingDisabled()
                                        .FirstOrDefault(j => j.Idjugador == idJugador);

                        if (jugador == null)
                            throw new KeyNotFoundException("El jugador no fue encontrado.");
                    }

                    bool estaActualmenteHabilitado = jugador.Habilitado;

                    if (estaActualmenteHabilitado)
                    {
                        // Lógica para DESHABILITAR
                        // BLL Check: ¿tiene equipo?
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
                        // Lógica para HABILITAR
                        // no hay chequeos, lo habilita de una
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

        /// <summary>
        /// Obtiene una lista de todos los jugadores HABILITADOS.
        /// </summary>
        /// <returns>Colección de <see cref="Jugador"/>.</returns>
        /// <remarks>
        public IEnumerable<Jugador> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.JugadorRepository.GetAll();
            }
        }

        /// <summary>
        /// Obtiene un jugador HABILITADO por su ID, con sus stats.
        /// </summary>
        /// <param name="id">El ID del jugador.</param>
        /// <returns>El <see cref="Jugador"/>.</returns>
        public Jugador GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.JugadorRepository.GetById(id);
            }
        }

        /// <summary>
        /// Obtiene los jugadores HABILITADOS de un equipo.
        /// </summary>
        /// <param name="idEquipo">El ID del equipo.</param>
        /// <returns>Colección de <see cref="Jugador"/>.</returns>
        public IEnumerable<Jugador> GetByEquipo(Guid idEquipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.JugadorRepository.GetByEquipo(idEquipo);
            }
        }

        /// <summary>
        /// Actualiza un jugador y todas sus estadísticas (Puntuacion/Sanciones).
        /// </summary>
        /// <param name="entity">El <see cref="Jugador"/> con los datos modificados.</param>
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

        /// <summary>
        /// Agrega o suma una estadística (ej: "Goles") a un jugador.
        /// </summary>
        /// <param name="idJugador">ID del jugador.</param>
        /// <param name="tipo">La clave (ej: "Goles", "Asistencias").</param>
        /// <param name="cantidad">El número a sumar (ej: 1).</param>
        public void AddPuntuacion(Guid idJugador, string tipo, int cantidad)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    // 1. Lectura (N+2 consultas)
                    var jugador = context.Repositories.JugadorRepository.GetById(idJugador);
                    if (jugador == null)
                        throw new KeyNotFoundException("El jugador no existe.");


                    // 2. Modificación en memoria
                    if (jugador.Puntuacion.ContainsKey(tipo))
                    {
                        jugador.Puntuacion[tipo] += cantidad;
                    }
                    else
                    {
                        jugador.Puntuacion.Add(tipo, cantidad);
                    }


                    // 3. Escritura (Múltiples delete/insert)
                    context.Repositories.JugadorRepository.Update(jugador);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Agrega o suma una sanción (ej: "Amarillas") a un jugador.
        /// </summary>
        /// <param name="idJugador">ID del jugador.</param>
        /// <param name="tipo">La clave (ej: "Amarillas", "Rojas").</param>
        /// <param name="cantidad">El número a sumar (ej: 1).</param>
        public void AddSancion(Guid idJugador, string tipo, int cantidad)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    // 1. Lectura (N+2 consultas)
                    var jugador = context.Repositories.JugadorRepository.GetById(idJugador);
                    if (jugador == null)
                        throw new KeyNotFoundException("El jugador no existe.");


                    // 2. Modificación en memoria
                    if (jugador.Sanciones.ContainsKey(tipo))
                    {
                        jugador.Sanciones[tipo] += cantidad;
                    }
                    else
                    {
                        jugador.Sanciones.Add(tipo, cantidad);
                    }


                    // 3. Escritura (Múltiples delete/insert)
                    context.Repositories.JugadorRepository.Update(jugador);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// (No implementado) Borra un jugador.
        /// </summary>
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene la lista de jugadores "libres" (sin equipo).
        /// </summary>
        /// <returns>Lista de <see cref="Jugador"/>.</returns>>
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

        /// <summary>
        /// Obtiene TODOS los jugadores (habilitados y deshabilitados).
        /// </summary>
        /// <returns>Lista de <see cref="Jugador"/>.</returns>
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