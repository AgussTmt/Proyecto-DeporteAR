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
    internal class FixtureService : IFixtureService
    {
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

        public void CargarResul(Fixture fixture, List<Jugador> jugadoresActualizados, bool localAusente, bool visitanteAusente)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // --- 1. Lógica de Fixture y Equipos (SIN CAMBIOS) ---
                    var fixtureDb = context.Repositories.FixtureRepository.GetById(fixture.IdFixture);
                    // ... (validaciones) ...
                    var (golesLocal, golesVisitante) = ParseResultado(fixture.Resultado);
                    var equipoLocal = context.Repositories.EquipoRepository.GetById(fixtureDb.Equipos.ElementAt(0).IdEquipo);
                    var equipoVisitante = context.Repositories.EquipoRepository.GetById(fixtureDb.Equipos.ElementAt(1).IdEquipo);
                    // ... (validaciones) ...
                    var compStub = new Competicion { IdCompeticion = fixtureDb.IdCompeticion };
                    var clasifLocal = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(compStub, equipoLocal);
                    var clasifVisitante = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(compStub, equipoVisitante);
                    // ... (validaciones) ...

                    // --- 2. Lógica de Puntos (¡¡MODIFICADA!!) ---
                    clasifLocal.PartidosJugados += 1;
                    clasifVisitante.PartidosJugados += 1;
                    clasifLocal.GolesAFavor += golesLocal;
                    clasifVisitante.GolesAFavor += golesVisitante;

                    if (localAusente && visitanteAusente)
                    {
                        // Doble ausencia: Empate 0-0, 0 puntos para ambos
                        clasifLocal.Empates += 1;
                        clasifVisitante.Empates += 1;
                        // No se suman puntos
                    }
                    else
                    {
                        // Lógica normal de puntos (si uno o ninguno faltó)
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

                    context.Repositories.ClasificacionRepository.Update(clasifLocal);
                    context.Repositories.ClasificacionRepository.Update(clasifVisitante);
                    context.Repositories.FixtureRepository.Update(fixtureDb);

                    // --- 3. Lógica de Jugadores (SIN CAMBIOS) ---
                    if (jugadoresActualizados != null && !localAusente && !visitanteAusente) // Solo guardar stats si no hubo ausencias
                    {
                        foreach (var jugador in jugadoresActualizados)
                        {
                            context.Repositories.JugadorRepository.Update(jugador);
                        }
                    }

                    // --- 4. Lógica de Ausencias (¡¡MODIFICADA!!) ---
                    if (localAusente)
                    {
                        equipoLocal.CantAusencias += 1;
                        context.Repositories.EquipoRepository.Update(equipoLocal);
                    }
                    if (visitanteAusente) // Cambiado de 'else if' a 'if'
                    {
                        equipoVisitante.CantAusencias += 1;
                        context.Repositories.EquipoRepository.Update(equipoVisitante);
                    }

                    // --- 5. Guardar TODO ---
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw; // Rollback
                }
            }
        }

        public void Delete(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.FixtureRepository.Delete(id);
                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }

        public IEnumerable<Fixture> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.FixtureRepository.GetAll();
            }
        }

        public Fixture GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.FixtureRepository.GetById(id);
            }
        }

        public List<Fixture> ListarPorRangoTiempo(DateTime dateTime)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.FixtureRepository.GetByTimeRange(dateTime);
            }
        }


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

        public IEnumerable<Fixture> GetPartidosPendientes(Guid idCompeticion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // 1. (1 query) Traemos los fixtures (con stubs de Equipos y CanchaHorario)
                    var fixtures = context.Repositories.FixtureRepository
                                        .GetByCompeticionPendientes(idCompeticion)
                                        .ToList();

                    if (!fixtures.Any()) return fixtures; // Lista vacía, no hay nada que hidratar

                    // 2. Juntamos todos los IDs que necesitamos hidratar
                    var idsEquipos = fixtures.Select(f => f.Equipos[0].IdEquipo)
                                             .Concat(fixtures.Select(f => f.Equipos[1].IdEquipo))
                                             .Distinct().ToList();

                    var idsCanchaHorario = fixtures.Select(f => f.CanchaHorario.IdCanchaHorario)
                                                   .Distinct().ToList();

                    // 3. Traemos los datos completos (2 queries)
                    var todosEquipos = context.Repositories.EquipoRepository.GetAllIncludingDisabled()
                                        .Where(e => idsEquipos.Contains(e.IdEquipo))
                                        .ToDictionary(e => e.IdEquipo, e => e);

                    var todosHorarios = context.Repositories.CanchaHorarioRepository.GetAll()
                                         .Where(ch => idsCanchaHorario.Contains(ch.IdCanchaHorario))
                                         .ToDictionary(ch => ch.IdCanchaHorario, ch => ch);

                    // 4. (Opcional pero recomendado) Hidratar la Cancha (que está dentro del Horario)
                    var idsCanchas = todosHorarios.Values.Select(ch => ch.Cancha.IdCancha).Distinct().ToList();
                    var todasCanchas = context.Repositories.CanchaRepository.GetAll()
                                        .Where(c => idsCanchas.Contains(c.IdCancha))
                                        .ToDictionary(c => c.IdCancha, c => c);

                    // 5. "Hidratamos" la lista final (reemplazamos stubs por objetos completos)
                    foreach (var f in fixtures)
                    {
                        // Hidratar Equipos
                        if (todosEquipos.ContainsKey(f.Equipos[0].IdEquipo))
                            f.Equipos[0] = todosEquipos[f.Equipos[0].IdEquipo];
                        if (todosEquipos.ContainsKey(f.Equipos[1].IdEquipo))
                            f.Equipos[1] = todosEquipos[f.Equipos[1].IdEquipo];

                        // Hidratar CanchaHorario
                        if (todosHorarios.ContainsKey(f.CanchaHorario.IdCanchaHorario))
                        {
                            f.CanchaHorario = todosHorarios[f.CanchaHorario.IdCanchaHorario];

                            // Hidratar la Cancha (que está dentro del CanchaHorario)
                            if (f.CanchaHorario.Cancha != null && todasCanchas.ContainsKey(f.CanchaHorario.Cancha.IdCancha))
                            {
                                f.CanchaHorario.Cancha = todasCanchas[f.CanchaHorario.Cancha.IdCancha];
                            }
                        }
                    }

                    return fixtures;
                }
                catch (Exception ex)
                {
                    // Relanzamos con más contexto
                    throw new Exception("Error en BLL al obtener y hidratar partidos pendientes.", ex);
                }
            }
        }

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

        // EN: BLL/Services/FixtureService.cs

        public IEnumerable<Fixture> GetByCompeticion(Guid idCompeticion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // 1. (1 query) Traemos los fixtures (con stubs)
                    var fixtures = context.Repositories.FixtureRepository
                                        .GetByCompeticion(new Competicion { IdCompeticion = idCompeticion })
                                        .ToList();

                    if (!fixtures.Any()) return fixtures;

                    // --- INICIO: Lógica de Hidratación (Corregida) ---

                    // 2. Juntamos todos los IDs que necesitamos hidratar
                    var idsEquipos = fixtures
                        // ¡¡FILTRO 1!!: Asegurarse que la lista exista y tenga 2 equipos
                        .Where(f => f.Equipos != null && f.Equipos.Count == 2)
                        .Select(f => f.Equipos[0].IdEquipo)
                        .Concat(fixtures
                            // ¡¡FILTRO 1 (repetido)!!
                            .Where(f => f.Equipos != null && f.Equipos.Count == 2)
                            .Select(f => f.Equipos[1].IdEquipo))
                        .Distinct().ToList();

                    var idsCanchaHorario = fixtures
                        // ¡¡FILTRO 2!!: Asegurarse que el CanchaHorario no sea null
                        .Where(f => f.CanchaHorario != null)
                        .Select(f => f.CanchaHorario.IdCanchaHorario)
                        .Distinct().ToList();

                    // 3. Traemos los datos completos
                    var todosEquipos = context.Repositories.EquipoRepository.GetAllIncludingDisabled()
                                        .Where(e => idsEquipos.Contains(e.IdEquipo))
                                        .ToDictionary(e => e.IdEquipo, e => e);

                    var todosHorarios = context.Repositories.CanchaHorarioRepository.GetAll()
                                         .Where(ch => idsCanchaHorario.Contains(ch.IdCanchaHorario))
                                         .ToDictionary(ch => ch.IdCanchaHorario, ch => ch);

                    // 4. Hidratar la Cancha (esto ya tenía el filtro de seguridad)
                    var idsCanchas = todosHorarios.Values
                                              .Where(ch => ch.Cancha != null)
                                              .Select(ch => ch.Cancha.IdCancha)
                                              .Distinct().ToList();

                    var todasCanchas = context.Repositories.CanchaRepository.GetAll()
                                        .Where(c => idsCanchas.Contains(c.IdCancha))
                                        .ToDictionary(c => c.IdCancha, c => c);

                    // 5. "Hidratamos" la lista final
                    foreach (var f in fixtures)
                    {
                        // ¡¡FILTRO 1 (de nuevo)!!
                        if (f.Equipos != null && f.Equipos.Count > 0 && todosEquipos.ContainsKey(f.Equipos[0].IdEquipo))
                            f.Equipos[0] = todosEquipos[f.Equipos[0].IdEquipo];
                        if (f.Equipos != null && f.Equipos.Count > 1 && todosEquipos.ContainsKey(f.Equipos[1].IdEquipo))
                            f.Equipos[1] = todosEquipos[f.Equipos[1].IdEquipo];

                        // ¡¡FILTRO 2 (de nuevo)!!
                        if (f.CanchaHorario != null && todosHorarios.ContainsKey(f.CanchaHorario.IdCanchaHorario))
                        {
                            f.CanchaHorario = todosHorarios[f.CanchaHorario.IdCanchaHorario];

                            if (f.CanchaHorario.Cancha != null && todasCanchas.ContainsKey(f.CanchaHorario.Cancha.IdCancha))
                            {
                                f.CanchaHorario.Cancha = todasCanchas[f.CanchaHorario.Cancha.IdCancha];
                            }
                        }
                    }
                    // --- FIN: Lógica de Hidratación ---

                    return fixtures;
                }
                catch (Exception ex)
                {
                    // Ahora, si falla, al menos veremos el error real en la consola de depuración
                    System.Diagnostics.Debug.WriteLine($"Error de hidratación: {ex.Message}");
                    throw new Exception("Error en BLL al obtener y hidratar fixtures por competición.", ex);
                }
            }
        }
    }
}
