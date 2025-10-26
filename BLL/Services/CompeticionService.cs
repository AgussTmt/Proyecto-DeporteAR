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

        // EN: BLL/Services/CompeticionService.cs

        public List<string> CrearFixture(Competicion competicion)
        {
            List<string> conflictos = new List<string>(); // Lista para guardar errores
            List<Action> operacionesPendientes = new List<Action>(); // Para guardar cambios si todo OK

            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // --- Validaciones Previas (igual que antes) ---
                    var comp = context.Repositories.CompeticionRepository.GetById(competicion.IdCompeticion);
                    if (comp == null)
                        throw new KeyNotFoundException("La competición no fue encontrada.");

                    if (comp.Estado != EstadoCompeticion.SinFixture)
                        throw new InvalidOperationException("El fixture para esta competición ya fue creado o está en curso.");

                    if (comp.ListaEquipos.Count < comp.CuposMinimos)
                        throw new InvalidOperationException($"No se alcanzan los cupos mínimos ({comp.CuposMinimos}). Equipos inscriptos: {comp.ListaEquipos.Count}.");

                    if (comp.ListaEquipos.Count < 2)
                        throw new InvalidOperationException("Se necesitan al menos 2 equipos para crear un fixture.");

                    if (comp.canchaAsignada == null)
                        throw new InvalidOperationException("La competición no tiene una cancha asignada.");
                    
                    var canchaCompleta = context.Repositories.CanchaRepository.GetById(comp.canchaAsignada.IdCancha);
                    if (canchaCompleta == null)
                        throw new KeyNotFoundException("La cancha asignada a la competición no fue encontrada.");

                    comp.canchaAsignada = canchaCompleta;
                    var partidosGenerados = GenerarPartidosRoundRobin(comp, comp.ListaEquipos);

                    // --- Bucle de Verificación y Preparación ---
                    foreach (var partido in partidosGenerados)
                    {
                        var horarioExistente = context.Repositories.CanchaHorarioRepository.GetByCanchaYHora(
                            comp.canchaAsignada.IdCancha, partido.Horario);

                        if (horarioExistente != null)
                        {
                            // Si existe pero NO está libre, REGISTRAR CONFLICTO
                            if (horarioExistente.Estado != EstadoReserva.Libre)
                            {
                                conflictos.Add($"El horario {partido.Horario:g} ya está '{horarioExistente.Estado}'.");
                                continue; // Pasar al siguiente partido, no se puede usar este slot
                            }
                            // Si existe Y está libre, PREPARAR la actualización
                            operacionesPendientes.Add(() =>
                            {
                                var newSlot = new CanchaHorario
                                {
                                    IdCanchaHorario = Guid.NewGuid(),
                                    Cancha = comp.canchaAsignada, 
                                    FechaHorario = partido.Horario,
                                    Estado = EstadoReserva.OcupadoPorTorneo,
                                    ReservadaPor = null, 
                                    Abonada = false,
                                    FueCambiada = false
                                };
                                context.Repositories.CanchaHorarioRepository.Update(horarioExistente);
                                context.Repositories.FixtureRepository.Add(partido);
                            });
                        }
                        else // El horario no existe
                        {
                            // Validar si la hora es válida según la plantilla
                            bool esValido = BLLFacade.Current.CanchaService.EsHorarioValido(comp.canchaAsignada.IdCancha, partido.Horario);
                            if (!esValido)
                            {
                                // Registrar conflicto si la hora es inválida
                                conflictos.Add($"El horario {partido.Horario:g} cae fuera de la disponibilidad de la cancha.");
                                continue; // Pasar al siguiente partido
                            }
                            // Si es válido pero no existe, PREPARAR la creación
                            operacionesPendientes.Add(() =>
                            {
                                var newSlot = new CanchaHorario
                                {
                                    IdCanchaHorario = Guid.NewGuid(),
                                    Cancha = comp.canchaAsignada,      
                                    FechaHorario = partido.Horario,  
                                    Estado = EstadoReserva.OcupadoPorTorneo,
                                    ReservadaPor = null, 
                                    Abonada = false,
                                    FueCambiada = false
                                };
                                context.Repositories.CanchaHorarioRepository.Add(newSlot);
                                context.Repositories.FixtureRepository.Add(partido);
                            });
                        }
                    } // Fin del foreach


                    if (conflictos.Any())
                    {
                        // No llamamos a SaveChanges, el UoW hará Rollback al salir del using
                        return conflictos;
                    }

                    // Si NO hay conflictos, ejecutar todas las operaciones preparadas
                    foreach (var operacion in operacionesPendientes)
                    {
                        operacion.Invoke(); // Ejecuta el Update/Add
                    }

                    // Actualizar estado de la competición
                    comp.Estado = EstadoCompeticion.ConFixture;
                    context.Repositories.CompeticionRepository.Update(comp);

                    // Guardar TODO
                    context.SaveChanges();

                    return null; // O return new List<string>(); para indicar éxito
                }
                catch (InvalidOperationException opEx)
                {
                    // Devolvemos el mensaje de error como un conflicto
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

        private List<Fixture> GenerarPartidosRoundRobin(Competicion comp, List<Equipo> equiposParaFixture)
        {
            var partidos = new List<Fixture>();
            var equipos = new List<Equipo>(equiposParaFixture);

            if (equipos.Count % 2 != 0)
            {
                equipos.Add(new Equipo { IdEquipo = Guid.Empty, Nombre = "DESCANSA" });
            }

            int numEquipos = equipos.Count;
            int numRondas = numEquipos - 1;
            int partidosPorRonda = numEquipos / 2;

            DateTime fechaPartido = comp.FechaInicio;

            // --- INICIO DE LA CORRECCIÓN ---
            string[] franja = comp.FranjaHoraria.Split('-');
            if (franja.Length < 2)
                throw new InvalidOperationException($"La franja horaria '{comp.FranjaHoraria}' no tiene el formato HH-HH (ej: 10-12).");

            TimeSpan horaInicioRonda = TimeSpan.Parse(franja[0] + ":00");
            // HoraFinRonda es la hora LÍMITE (el partido debe terminar ANTES de esta hora)
            TimeSpan horaFinRonda = TimeSpan.Parse(franja[1] + ":00");
            // --- FIN DE LA CORRECCIÓN ---

            int duracionMinutos = comp.canchaAsignada.DuracionXPartidoMin;
            if (duracionMinutos <= 0)
                throw new InvalidOperationException("La duración del partido (DuracionXPartidoMin) debe ser mayor a 0.");

            for (int r = 0; r < numRondas; r++)
            {
                TimeSpan horaPartidoActual = horaInicioRonda; // Resetea la hora para cada nueva fecha
                int partidosAgendadosEnRonda = 0; // Contador de partidos reales agendados

                for (int i = 0; i < partidosPorRonda; i++)
                {
                    Equipo local = equipos[i];
                    Equipo visitante = equipos[numEquipos - 1 - i];

                    // Si ninguno es el equipo "DESCANSA", se crea el partido
                    if (local.IdEquipo != Guid.Empty && visitante.IdEquipo != Guid.Empty)
                    {
                        // --- INICIO DE LA CORRECCIÓN ---
                        // Verificamos si el partido (con su duración) cabe en la franja
                        TimeSpan horaFinPartido = horaPartidoActual.Add(TimeSpan.FromMinutes(duracionMinutos));

                        // Si la hora de FIN del partido es MAYOR que la hora FIN de la franja, no se agenda
                        // Ej: Franja 10-12 (horaFinRonda = 12:00)
                        // - Partido 11:00 (dura 60min) -> termina 12:00. (12:00 > 12:00 es FALSO). OK.
                        // - Partido 12:00 (dura 60min) -> termina 13:00. (13:00 > 12:00 es VERDADERO). ERROR.
                        if (horaFinPartido > horaFinRonda)
                        {
                            // No hay más espacio en esta jornada (franja)
                            break; // Rompe el bucle 'for (int i...'
                        }
                        // --- FIN DE LA CORRECCIÓN ---

                        var partido = new Fixture
                        {
                            IdFixture = Guid.NewGuid(),
                            IdCompeticion = comp.IdCompeticion,
                            Estado = EstadoFixture.Pendiente,
                            Resultado = null,
                            Horario = fechaPartido.Date.Add(horaPartidoActual),
                            Equipos = new List<Equipo> { local, visitante }
                        };
                        partidos.Add(partido);
                        partidosAgendadosEnRonda++; // Incrementa contador

                        // Avanza la hora para el siguiente partido
                        horaPartidoActual = horaFinPartido; // El sig. partido empieza cuando termina este
                    }
                }

                // --- INICIO DE LA CORRECCIÓN ---
                // Contamos cuántos partidos DEBERÍAN haberse agendado (sin contar descansos)
                int partidosRequeridos = 0;
                for (int i = 0; i < partidosPorRonda; i++)
                {
                    if (equipos[i].IdEquipo != Guid.Empty && equipos[numEquipos - 1 - i].IdEquipo != Guid.Empty)
                        partidosRequeridos++;
                }

                // Verificamos si todos los partidos de la ronda entraron en la franja
                if (partidosAgendadosEnRonda < partidosRequeridos)
                {
                    // ¡ERROR! No entraron todos los partidos en la franja.
                    // Lanzamos una excepción que será capturada por CrearFixture
                    throw new InvalidOperationException($"La franja horaria '{comp.FranjaHoraria}' no es suficiente para '{partidosRequeridos}' partidos de {duracionMinutos} min c/u. Solo entraron {partidosAgendadosEnRonda} partidos en esa franja.");
                }
                // --- FIN DE LA CORRECCIÓN ---


                // Rotar los equipos (dejando el primero fijo)
                var equipoRotativo = equipos[1];
                equipos.RemoveAt(1);
                equipos.Add(equipoRotativo);

                // Avanzar la fecha para la siguiente ronda
                fechaPartido = fechaPartido.AddDays(comp.Frecuencia);
            }

            return partidos;
        }
    }
}

