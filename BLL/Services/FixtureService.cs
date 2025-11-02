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
                    if (fixtureDb == null)
                        throw new KeyNotFoundException("El partido no existe.");

                    if (fixtureDb.Estado == EstadoFixture.Finalizado)
                        throw new InvalidOperationException("Este partido ya fue finalizado.");

                    var (golesLocal, golesVisitante) = ParseResultado(fixture.Resultado);
                    var equipoLocal = context.Repositories.EquipoRepository.GetById(fixtureDb.Equipos.ElementAt(0).IdEquipo);
                    var equipoVisitante = context.Repositories.EquipoRepository.GetById(fixtureDb.Equipos.ElementAt(1).IdEquipo);

                    if (equipoLocal == null || equipoVisitante == null)
                        throw new KeyNotFoundException("No se pudieron cargar los equipos completos del partido.");

                    var compStub = new Competicion { IdCompeticion = fixtureDb.IdCompeticion };
                    var clasifLocal = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(compStub, equipoLocal);
                    var clasifVisitante = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(compStub, equipoVisitante);

                    if (clasifLocal == null || clasifVisitante == null)
                        throw new InvalidOperationException("No se encontraron las filas de clasificación para los equipos. (Asegúrese de que los equipos estén inscriptos en la competición).");

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
                        else 
                        {
                            clasifLocal.Empates += 1;
                            clasifLocal.Puntos += 1;
                            clasifVisitante.Empates += 1;
                            clasifVisitante.Puntos += 1;
                        }
                    }

                    fixtureDb.Resultado = fixture.Resultado;
                    fixtureDb.Estado = EstadoFixture.Finalizado;


                    //valido si es el ultimo fixture, asi marco finalizado el partido
                    int partidosPendientesCount = context.Repositories.FixtureRepository.CountPartidosPendientes(fixtureDb.IdCompeticion);
                    if (partidosPendientesCount == 1)
                    {
                        var comp = context.Repositories.CompeticionRepository.GetById(fixtureDb.IdCompeticion);
                        if (comp != null)
                        {
                            comp.Estado = EstadoCompeticion.Finalizado; //
                            context.Repositories.CompeticionRepository.Update(comp);
                        }
                    }

                    context.Repositories.ClasificacionRepository.Update(clasifLocal);
                    context.Repositories.ClasificacionRepository.Update(clasifVisitante);
                    context.Repositories.FixtureRepository.Update(fixtureDb);

                    if (jugadoresActualizados != null && !localAusente && !visitanteAusente) // Solo guardar stats si no hubo ausencias
                    {
                        foreach (var jugador in jugadoresActualizados)
                        {
                            context.Repositories.JugadorRepository.Update(jugador);
                        }
                    }

                    //si falta alguno
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
        public IEnumerable<Fixture> GetByCompeticion(Guid idCompeticion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    //traigo los fixture
                    var fixtures = context.Repositories.FixtureRepository
                                        .GetByCompeticion(new Competicion { IdCompeticion = idCompeticion })
                                        .ToList();

                    if (!fixtures.Any()) return fixtures;


                    var idsEquipos = fixtures.Select(f => f.Equipos[0].IdEquipo)
                        .Concat(fixtures.Select(f => f.Equipos[1].IdEquipo))
                        .Distinct().ToList();

                    var idsCanchaHorario = fixtures.Select(f => f.CanchaHorario.IdCanchaHorario) 
                        .Distinct().ToList();

                    // 3. Traemos los datos completos
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
