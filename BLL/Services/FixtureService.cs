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
    /// <summary>
    /// Capa de Lógica de Negocio (BLL) para gestionar los partidos (<see cref="Fixture"/>).
    /// </summary>
    /// <remarks>
    /// Acá está la lógica más pesada del sistema: Cargar el resultado de un partido.
    /// También tiene métodos de lectura optimizados para evitar el N+1.
    /// </remarks>
    internal class FixtureService : IFixtureService
    {
        /// <summary>
        /// Agrega un nuevo <see cref="Fixture"/> (partido) a la base de datos.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Fixture"/> a insertar.</param>
        public void Add(Fixture entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.FixtureRepository.Add(entity);
                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }

        /// <summary>
        /// Lógica de negocio principal para cargar el resultado de un partido finalizado.
        /// </summary>
        /// <param name="fixture">El <see cref="Fixture"/> (stub) con el <c>Resultado</c> cargado.</param>
        /// <param name="jugadoresActualizados">La lista de <see cref="Jugador"/> con sus estadísticas actualizadas (goles, MVP, etc.).</param>
        /// <param name="localAusente">Flag: ¿El equipo local faltó?</param>
        /// <param name="visitanteAusente">Flag: ¿El equipo visitante faltó?</param>
        /// <remarks>
        /// 1. Parsea el resultado (ej: "3-1").
        /// 2. Carga el fixture, los equipos y sus filas de Clasificación (tabla de posiciones).
        /// 3. Actualiza la Clasificación (Puntos, PG, PE, PP, GF).
        /// 4. Marca el Fixture como 'Finalizado'.
        /// 5. Si era el último partido, marca la Competición como 'Finalizada'.
        /// 6. Actualiza las estadísticas de todos los jugadores (si no hubo ausentes).
        /// 7. Suma ausencias (W.O.) si es necesario.
        /// 8. Guarda todo.
        /// </remarks>
        public void CargarResul(Fixture fixture, List<Jugador> jugadoresActualizados, bool localAusente, bool visitanteAusente)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // --- 1. Cargar todas las entidades necesarias ---
                    var fixtureDb = context.Repositories.FixtureRepository.GetById(fixture.IdFixture);
                    if (fixtureDb == null)
                        throw new KeyNotFoundException("El partido no existe.");

                    if (fixtureDb.Estado == EstadoFixture.Finalizado)
                        throw new InvalidOperationException("Este partido ya fue finalizado.");

                    var (golesLocal, golesVisitante) = ParseResultado(fixture.Resultado);

                    // Ojo que GetById de Equipo ya trae jugadores (N+2)
                    var equipoLocal = context.Repositories.EquipoRepository.GetById(fixtureDb.Equipos.ElementAt(0).IdEquipo);
                    var equipoVisitante = context.Repositories.EquipoRepository.GetById(fixtureDb.Equipos.ElementAt(1).IdEquipo);

                    if (equipoLocal == null || equipoVisitante == null)
                        throw new KeyNotFoundException("No se pudieron cargar los equipos completos del partido.");

                    var compStub = new Competicion { IdCompeticion = fixtureDb.IdCompeticion };
                    var clasifLocal = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(compStub, equipoLocal);
                    var clasifVisitante = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(compStub, equipoVisitante);

                    if (clasifLocal == null || clasifVisitante == null)
                        throw new InvalidOperationException("No se encontraron las filas de clasificación para los equipos. (Asegúrese de que los equipos estén inscriptos en la competición).");

                    // --- 2. Lógica de Negocio: Actualizar Clasificación ---
                    clasifLocal.PartidosJugados += 1;
                    clasifVisitante.PartidosJugados += 1;
                    clasifLocal.GolesAFavor += golesLocal;
                    clasifVisitante.GolesAFavor += golesVisitante;

                    if (localAusente && visitanteAusente)
                    {
                        // Doble ausencia: Empate 0-0, 0 puntos para ambos
                        clasifLocal.Empates += 1;
                        clasifVisitante.Empates += 1;
                    }
                    else
                    {
                        // Lógica normal de puntos
                        if (golesLocal > golesVisitante) // Gana Local
                        {
                            clasifLocal.Victorias += 1;
                            clasifLocal.Puntos += 3;
                            clasifVisitante.Derrotas += 1;
                        }
                        else if (golesVisitante > golesLocal) // Gana Visitante
                        {
                            clasifVisitante.Victorias += 1;
                            clasifVisitante.Puntos += 3;
                            clasifLocal.Derrotas += 1;
                        }
                        else // Empate
                        {
                            clasifLocal.Empates += 1;
                            clasifLocal.Puntos += 1;
                            clasifVisitante.Empates += 1;
                            clasifVisitante.Puntos += 1;
                        }
                    }

                    fixtureDb.Resultado = fixture.Resultado;
                    fixtureDb.Estado = EstadoFixture.Finalizado;

                    // BLL Check: ¿Era el último partido?
                    // El repo cuenta los que *eran* pendientes. Si solo quedaba este, da 1.
                    int partidosPendientesCount = context.Repositories.FixtureRepository.CountPartidosPendientes(fixtureDb.IdCompeticion);
                    if (partidosPendientesCount == 1)
                    {
                        var comp = context.Repositories.CompeticionRepository.GetById(fixtureDb.IdCompeticion);
                        if (comp != null)
                        {
                            comp.Estado = EstadoCompeticion.Finalizado;
                            context.Repositories.CompeticionRepository.Update(comp);
                        }
                    }

                    // --- 3. Persistir todo en la transacción ---
                    context.Repositories.ClasificacionRepository.Update(clasifLocal);
                    context.Repositories.ClasificacionRepository.Update(clasifVisitante);
                    context.Repositories.FixtureRepository.Update(fixtureDb);

                    // 4. Actualizar Jugadores (si no hubo W.O.)
                    if (jugadoresActualizados != null && !localAusente && !visitanteAusente)
                    {
                        foreach (var jugador in jugadoresActualizados)
                        {
                            // ¡OJO! Esto llama a JugadorRepository.Update
                            // que hace delete/insert en Puntuacion y Sanciones.
                            // Si actualizás 22 jugadores, esto son MUCHAS consultas.
                            context.Repositories.JugadorRepository.Update(jugador);
                        }
                    }

                    // 5. Actualizar Ausencias
                    if (localAusente)
                    {
                        equipoLocal.CantAusencias += 1;
                        context.Repositories.EquipoRepository.Update(equipoLocal);
                    }
                    if (visitanteAusente)
                    {
                        equipoVisitante.CantAusencias += 1;
                        context.Repositories.EquipoRepository.Update(equipoVisitante);
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
        /// (No implementado) Borra un partido.
        /// </summary>
        public void Delete(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // Ojo, el repo tira NotImplementedException
                    context.Repositories.FixtureRepository.Delete(id);
                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }

        /// <summary>
        /// Obtiene una lista de todos los partidos (fixtures) en el sistema.
        /// </summary>
        /// <returns>Colección de <see cref="Fixture"/>.</returns>
        public IEnumerable<Fixture> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.FixtureRepository.GetAll();
            }
        }

        /// <summary>
        /// Obtiene un partido (fixture) específico por su ID.
        /// </summary>
        /// <param name="id">El ID del fixture.</param>
        /// <returns>El <see cref="Fixture"/>.</returns>
        public Fixture GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.FixtureRepository.GetById(id);
            }
        }

        /// <summary>
        /// Obtiene todos los partidos programados para una fecha específica.
        /// </summary>
        /// <param name="dateTime">La fecha a consultar.</param>
        /// <returns>Una lista de <see cref="Fixture"/>.</returns>
        public List<Fixture> ListarPorRangoTiempo(DateTime dateTime)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.FixtureRepository.GetByTimeRange(dateTime);
            }
        }


        /// <summary>
        /// Actualiza un partido (fixture) existente.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Fixture"/> con los datos modificados.</param>
        public void Update(Fixture entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.FixtureRepository.Update(entity);
                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }

        /// <summary>
        /// Método de ayuda (privado) para convertir un string "X-Y" en goles.
        /// </summary>
        /// <param name="resultado">El string del resultado (ej: "3-1").</param>
        /// <returns>Una tupla (int golesLocal, int golesVisitante).</returns>
        /// <exception cref="FormatException">Si el formato no es válido.</exception>
        private (int golesLocal, int golesVisitante) ParseResultado(string resultado)
        {
            try
            {
                var parts = resultado.Split('-');
                if (parts.Length != 2)
                    throw new FormatException("El resultado debe tener el formato 'GolesLocal-GolesVisitante'.");

                return (int.Parse(parts[0]), int.Parse(parts[1]));
            }
            catch (Exception ex)
            {
                throw new FormatException($"Error al parsear el resultado '{resultado}'. Asegúrese de que tenga el formato 'X-Y'.", ex);
            }
        }

        /// <summary>
        /// Obtiene todos los partidos de una competición, pero "hidratados".
        /// </summary>
        /// <param name="idCompeticion">El ID de la competición.</param>
        /// <returns>Una lista de <see cref="Fixture"/> completos.</returns>
        /// <remarks>
        /// ¡Este método SÍ está optimizado! Arregla el N+1 que tenían
        /// los repositorios. En lugar de hacer N consultas, hace:
        /// 1. Una consulta para los Fixtures.
        /// 2. Una consulta para TODOS los Equipos de esos fixtures.
        /// 3. Una consulta para TODOS los Horarios de esos fixtures.
        /// 4. Una consulta para TODAS las Canchas de esos horarios.
        /// 5. Arma el rompecabezas en memoria.
        /// </remarks>
        public IEnumerable<Fixture> GetByCompeticion(Guid idCompeticion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // 1. Traigo los fixtures (1 consulta)
                    var fixtures = context.Repositories.FixtureRepository
                                        .GetByCompeticion(new Competicion { IdCompeticion = idCompeticion })
                                        .ToList();

                    if (!fixtures.Any()) return fixtures; // Lista vacía

                    // 2. Junto todos los IDs que necesito hidratar
                    var idsEquipos = fixtures.Select(f => f.Equipos[0].IdEquipo)
                        .Concat(fixtures.Select(f => f.Equipos[1].IdEquipo))
                        .Distinct().ToList();

                    var idsCanchaHorario = fixtures.Select(f => f.CanchaHorario.IdCanchaHorario)
                        .Distinct().ToList();

                    // 3. Traemos los datos completos (3 consultas más, pero NO N)
                    var todosEquipos = context.Repositories.EquipoRepository.GetAllIncludingDisabled()
                                        .Where(e => idsEquipos.Contains(e.IdEquipo))
                                        .ToDictionary(e => e.IdEquipo, e => e);

                    var todosHorarios = context.Repositories.CanchaHorarioRepository.GetAll()
                                         .Where(ch => idsCanchaHorario.Contains(ch.IdCanchaHorario))
                                         .ToDictionary(ch => ch.IdCanchaHorario, ch => ch);


                    var idsCanchas = todosHorarios.Values
                                              .Where(ch => ch.Cancha != null)
                                              .Select(ch => ch.Cancha.IdCancha)
                                              .Distinct().ToList();


                    var todasCanchas = context.Repositories.CanchaRepository.GetAll()
                                        .Where(c => idsCanchas.Contains(c.IdCancha))
                                        .ToDictionary(c => c.IdCancha, c => c);

                    // 4. Hidratamos (armamos el rompecabezas en memoria)
                    foreach (var f in fixtures)
                    {
                        //hidratar equipos
                        if (f.Equipos != null)
                        {
                            if (f.Equipos.Count > 0 && todosEquipos.TryGetValue(f.Equipos[0].IdEquipo, out Equipo equipo0Completo))
                            {
                                f.Equipos[0] = equipo0Completo;
                            }

                            if (f.Equipos.Count > 1 && todosEquipos.TryGetValue(f.Equipos[1].IdEquipo, out Equipo equipo1Completo))
                            {
                                f.Equipos[1] = equipo1Completo;
                            }
                        }
                        //Hidratar Horario y Cancha
                        if (f.CanchaHorario != null && todosHorarios.TryGetValue(f.CanchaHorario.IdCanchaHorario, out CanchaHorario horarioCompleto))
                        {
                            f.CanchaHorario = horarioCompleto;
                            if (f.CanchaHorario.Cancha != null && todasCanchas.TryGetValue(f.CanchaHorario.Cancha.IdCancha, out Cancha canchaCompleta))
                            {
                                f.CanchaHorario.Cancha = canchaCompleta;
                            }
                        }
                    }

                    return fixtures;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en BLL al obtener y hidratar fixtures por competición.", ex);
                }
            }
        }
    }
}
