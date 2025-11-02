using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Facade;
using BLL.Interfaces;
using DAL.Factory;
using DomainModel;

namespace BLL.Services
{
    internal class CompeticionService : ICompeticionService
    {
        public void Add(Competicion entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    entity.Estado = EstadoCompeticion.SinFixture;
                    context.Repositories.CompeticionRepository.Add(entity);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void AñadirEquipo(Competicion competicion, Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {


                    var comp = context.Repositories.CompeticionRepository.GetById(competicion.IdCompeticion);
                    if (comp == null)
                        throw new KeyNotFoundException("La competición no existe.");

                    //No se puede inscribir si el fixture ya esta creado
                    if (comp.Estado != EstadoCompeticion.SinFixture)
                        throw new InvalidOperationException("Las inscripciones están cerradas, el fixture ya fue creado.");

                    //No se puede inscribir si está lleno
                    if (comp.ListaEquipos.Count >= comp.Cupos)
                        throw new InvalidOperationException("La competición ha alcanzado su cupo máximo de equipos.");

                    var equipoCompleto = context.Repositories.EquipoRepository.GetById(equipo.IdEquipo);
                    if (equipoCompleto == null || !equipoCompleto.Habilitado)
                    {
                        throw new InvalidOperationException($"El equipo '{equipo.Nombre}' no existe o está deshabilitado y no puede ser inscripto.");
                    }

                    //El equipo ya está inscripto
                    if (comp.ListaEquipos.Any(e => e.IdEquipo == equipo.IdEquipo))
                        throw new InvalidOperationException("El equipo ya está inscripto en esta competición.");


                    context.Repositories.CompeticionRepository.AddEquipo(comp.IdCompeticion, equipo.IdEquipo);


                    var clasificacion = new Clasificacion
                    {
                        IdClasificacion = Guid.NewGuid(),
                        IdCompeticion = comp.IdCompeticion,
                        Equipo = equipo.Nombre,
                        Derrotas = 0,
                        Empates = 0,
                        Victorias = 0,
                        GolesAFavor = 0,
                        PartidosJugados = 0,
                        Puntos = 0
                    };
                    context.Repositories.ClasificacionRepository.Add(clasificacion);


                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public List<string> CrearFixture(Competicion competicion)
        {
            List<string> conflictos = new List<string>();
            List<Action> operacionesPendientes = new List<Action>();

            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    //validar
                    var comp = context.Repositories.CompeticionRepository.GetById(competicion.IdCompeticion);
                    if (comp == null)
                        throw new KeyNotFoundException("La competición no fue encontrada.");
                    if (comp.Estado != EstadoCompeticion.SinFixture)
                        throw new InvalidOperationException("El fixture para esta competición ya fue creado.");
                    if (comp.ListaEquipos.Count < comp.CuposMinimos)
                        throw new InvalidOperationException($"No se alcanzan los cupos mínimos ({comp.CuposMinimos}).");
                    if (comp.ListaEquipos.Count < 2)
                        throw new InvalidOperationException("Se necesitan al menos 2 equipos.");
                    if (comp.canchaAsignada == null)
                        throw new InvalidOperationException("La competición no tiene una cancha asignada.");

                    var canchaCompleta = context.Repositories.CanchaRepository.GetById(comp.canchaAsignada.IdCancha);
                    if (canchaCompleta == null)
                        throw new KeyNotFoundException("La cancha asignada no fue encontrada.");

                    comp.canchaAsignada = canchaCompleta;
                    var partidosPlaneados = GenerarPartidosRoundRobin(comp, comp.ListaEquipos);

                    foreach (var partidoPlaneado in partidosPlaneados)
                    {
                        //busco slot disponible
                        var slotRequerido = context.Repositories.CanchaHorarioRepository.GetByCanchaYHora(
                            comp.canchaAsignada.IdCancha,
                            partidoPlaneado.HorarioRequerido);

                        if (slotRequerido != null)
                        {
                            //valido q este libre
                            if (slotRequerido.Estado != EstadoReserva.Libre)
                            {
                                conflictos.Add($"El horario {partidoPlaneado.HorarioRequerido:g} ya está '{slotRequerido.Estado}'.");
                                continue;
                            }
                            else
                            {
                                operacionesPendientes.Add(() =>
                                {
                                    slotRequerido.Estado = EstadoReserva.OcupadoPorTorneo;
                                    context.Repositories.CanchaHorarioRepository.Update(slotRequerido);
                                    //creo fixture
                                    var partido = new Fixture
                                    {
                                        IdFixture = Guid.NewGuid(),
                                        IdCompeticion = comp.IdCompeticion,
                                        Estado = EstadoFixture.Pendiente,
                                        Resultado = null,
                                        Equipos = new List<Equipo> { partidoPlaneado.EquipoLocal, partidoPlaneado.EquipoVisitante },
                                        CanchaHorario = slotRequerido // ¡Enlace correcto!
                                    };
                                    context.Repositories.FixtureRepository.Add(partido);
                                });
                            }
                        }
                        else
                        {
                            bool esValido = BLLFacade.Current.CanchaService.EsHorarioValido(comp.canchaAsignada.IdCancha, partidoPlaneado.HorarioRequerido);
                            if (!esValido)
                            {
                                // CONFLICTO: El horario cae fuera de la disponibilidad
                                conflictos.Add($"El horario {partidoPlaneado.HorarioRequerido:g} cae fuera de la disponibilidad de la cancha.");
                                continue;
                            }
                            else
                            {
                                // ÉXITO: El horario es válido.
                                operacionesPendientes.Add(() =>
                                {
                                    // 1. Crear el nuevo slot (CanchaHorario)
                                    var newSlot = new CanchaHorario
                                    {
                                        IdCanchaHorario = Guid.NewGuid(),
                                        Cancha = comp.canchaAsignada,
                                        FechaHorario = partidoPlaneado.HorarioRequerido,
                                        Estado = EstadoReserva.OcupadoPorTorneo,
                                        ReservadaPor = null,
                                        Abonada = false,
                                        FueCambiada = false
                                    };
                                    context.Repositories.CanchaHorarioRepository.Add(newSlot);

                                    // 2. Crear el Fixture y enlazarlo al slot
                                    var partido = new Fixture
                                    {
                                        IdFixture = Guid.NewGuid(),
                                        IdCompeticion = comp.IdCompeticion,
                                        Estado = EstadoFixture.Pendiente,
                                        Resultado = null,
                                        Equipos = new List<Equipo> { partidoPlaneado.EquipoLocal, partidoPlaneado.EquipoVisitante },
                                        CanchaHorario = newSlot
                                    };
                                    context.Repositories.FixtureRepository.Add(partido);
                                });
                            }
                        }
                    }

                    if (conflictos.Any())
                    {
                        return conflictos;
                    }
                    // Si NO hay conflictos, ejecutar todo
                    foreach (var operacion in operacionesPendientes)
                    {
                        operacion.Invoke();
                    }

                    comp.Estado = EstadoCompeticion.ConFixture;
                    context.Repositories.CompeticionRepository.Update(comp);

                    context.SaveChanges();
                    return null;
                }
                catch (InvalidOperationException opEx)
                {
                    return new List<string> { $"Error de configuración: {opEx.Message}" };
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }


        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Competicion> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetAll().ToList();
            }
        }

        public Competicion GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetById(id);
            }
        }

        public List<Competicion> ListarConVacantes(Competicion competicion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetWithVacancies();
            }
        }

        public List<Competicion> ListarPorCliente(Cliente cliente)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetByClient(cliente);
            }
        }

        public void QuitarEquipo(Competicion competicion, Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    var comp = context.Repositories.CompeticionRepository.GetById(competicion.IdCompeticion);
                    if (comp == null)
                        throw new KeyNotFoundException("La competición no existe.");


                    if (comp.Estado != EstadoCompeticion.SinFixture)
                        throw new InvalidOperationException("No se puede quitar un equipo una vez que el fixture está creado.");


                    context.Repositories.CompeticionRepository.RemoveEquipo(comp.IdCompeticion, equipo.IdEquipo);


                    var clasificacion = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(comp, equipo);
                    if (clasificacion != null)
                    {

                        context.Repositories.ClasificacionRepository.Delete(clasificacion.IdClasificacion);
                    }


                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Update(Competicion entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.CompeticionRepository.Update(entity);
                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }

        private List<PartidoGenerado> GenerarPartidosRoundRobin(Competicion comp, List<Equipo> equiposParaFixture)
        {
            var partidosPlaneados = new List<PartidoGenerado>();
            var equipos = new List<Equipo>(equiposParaFixture);

            if (equipos.Count % 2 != 0)
            {
                equipos.Add(new Equipo { IdEquipo = Guid.Empty, Nombre = "DESCANSA" });
            }

            int numEquipos = equipos.Count;
            int numRondas = numEquipos - 1;
            int partidosPorRonda = numEquipos / 2;
            DateTime fechaPartido = comp.FechaInicio;

            string[] franja = comp.FranjaHoraria.Split('-');
            if (franja.Length < 2)
                throw new InvalidOperationException($"La franja horaria '{comp.FranjaHoraria}' no tiene el formato HH-HH.");

            TimeSpan horaInicioRonda = TimeSpan.Parse(franja[0] + ":00");
            TimeSpan horaFinRonda = TimeSpan.Parse(franja[1] + ":00");
            int duracionMinutos = comp.canchaAsignada.DuracionXPartidoMin;
            if (duracionMinutos <= 0)
                throw new InvalidOperationException("La duración del partido (DuracionXPartidoMin) debe ser mayor a 0.");

            for (int r = 0; r < numRondas; r++)
            {
                TimeSpan horaPartidoActual = horaInicioRonda;
                int partidosAgendadosEnRonda = 0;
                int partidosRequeridosEnRonda = 0;

                // 1. Contamos cuántos partidos reales (no descansos) hay en esta ronda
                for (int i = 0; i < partidosPorRonda; i++)
                {
                    if (equipos[i].IdEquipo != Guid.Empty && equipos[numEquipos - 1 - i].IdEquipo != Guid.Empty)
                        partidosRequeridosEnRonda++;
                }

                // 2. Agendamos los partidos
                for (int i = 0; i < partidosPorRonda; i++)
                {
                    Equipo local = equipos[i];
                    Equipo visitante = equipos[numEquipos - 1 - i];

                    if (local.IdEquipo != Guid.Empty && visitante.IdEquipo != Guid.Empty)
                    {
                        TimeSpan horaFinPartido = horaPartidoActual.Add(TimeSpan.FromMinutes(duracionMinutos));

                        if (horaFinPartido > horaFinRonda)
                        {
                            // ¡ERROR! No entraron todos los partidos en la franja.
                            throw new InvalidOperationException($"La franja horaria '{comp.FranjaHoraria}' no es suficiente para '{partidosRequeridosEnRonda}' partidos de {duracionMinutos} min c/u. Solo entraron {partidosAgendadosEnRonda} partidos en esa franja.");
                        }

                        var plan = new PartidoGenerado
                        {
                            EquipoLocal = local,
                            EquipoVisitante = visitante,
                            HorarioRequerido = fechaPartido.Date.Add(horaPartidoActual)
                        };
                        partidosPlaneados.Add(plan);


                        partidosAgendadosEnRonda++;
                        horaPartidoActual = horaFinPartido;
                    }
                }

                // Rotar los equipos
                var equipoRotativo = equipos[1];
                equipos.RemoveAt(1);
                equipos.Add(equipoRotativo);

                fechaPartido = fechaPartido.AddDays(comp.Frecuencia);
            }

            return partidosPlaneados;
        }

        public void ActivarODesactivar(Guid idCompeticion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    var comp = context.Repositories.CompeticionRepository.GetById(idCompeticion);
                    if (comp == null)
                        throw new KeyNotFoundException("La competición no existe.");

                    switch (comp.Estado)
                    {
                        //sino tiene fixture todo ok, saco lo equipos
                        case EstadoCompeticion.SinFixture:
                            var equiposInscriptos = new List<Equipo>();
                            foreach (var equipoStub in comp.ListaEquipos)
                            {
                                var equipoFull = context.Repositories.EquipoRepository.GetById(equipoStub.IdEquipo);
                                if (equipoFull != null)
                                    equiposInscriptos.Add(equipoFull);
                            }

                            //Borro las clasifaciones de los equipos en el torneo
                            foreach (var equipo in equiposInscriptos)
                            {
                                var clasif = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(comp, equipo);
                                if (clasif != null)
                                {
                                    context.Repositories.ClasificacionRepository.Delete(clasif.IdClasificacion);
                                }
                            }

                            //quito los equipos
                            context.Repositories.CompeticionRepository.RemoveAllEquipos(idCompeticion);

                            //cambio estado
                            comp.Estado = EstadoCompeticion.Cancelado;
                            context.Repositories.CompeticionRepository.Update(comp);

                            context.SaveChanges();
                            break;

                        // --- REGLA 2: Archivar un torneo (FINALIZADO) ---
                        case EstadoCompeticion.Finalizado:
                            var partidosPendientes = context.Repositories.FixtureRepository.GetByCompeticionPendientes(idCompeticion);
                            if (partidosPendientes.Any())
                            {
                                // ¡Bloqueamos la acción!
                                throw new InvalidOperationException("No se puede archivar. Esta competición está marcada como 'Finalizada', pero todavía tiene partidos pendientes de disputar.");
                            }
                            comp.Estado = EstadoCompeticion.Archivado;
                            context.Repositories.CompeticionRepository.Update(comp);
                            context.SaveChanges();
                            break;

                        // --- REGLA 3: Prohibir (EN CURSO) ---
                        case EstadoCompeticion.ConFixture:
                            throw new InvalidOperationException("No se puede inactivar una competición que ya tiene un fixture generado o está en curso.");

                        // --- REGLA 4: Ya está inactivo ---
                        case EstadoCompeticion.Cancelado:
                        case EstadoCompeticion.Archivado:
                            throw new InvalidOperationException("Esta competición ya se encuentra inactiva.");

                        default:
                            throw new InvalidOperationException($"Estado de competición no reconocido: {comp.Estado}");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}

