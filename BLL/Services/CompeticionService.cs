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
    /// <summary>
    /// Capa de Lógica de Negocio (BLL) para gestionar las <see cref="Competicion"/>.
    /// Este es el cerebro de los torneos.
    /// </summary>
    /// <remarks>
    /// Esta clase maneja toda la lógica compleja: inscribir equipos,
    /// generar el fixture (algoritmo Round Robin),
    /// y asegurarse de que los horarios de los partidos no se pisen
    /// con las reservas normales. Es el orquestador principal.
    /// </remarks>
    internal class CompeticionService : ICompeticionService
    {
        /// <summary>
        /// Agrega una nueva Competición (Torneo/Liga) a la base de datos.
        /// </summary>
        /// <param name="entity">La <see cref="Competicion"/> a crear.</param>
        /// <remarks>
        /// Al crearla, le clava el estado 'SinFixture' por defecto,
        /// lo que la deja lista para empezar a recibir inscripciones.
        /// </remarks>
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

        /// <summary>
        /// Inscribe un <see cref="Equipo"/> en una <see cref="Competicion"/>.
        /// </summary>
        /// <param name="competicion">La competición (stub).</param>
        /// <param name="equipo">El equipo (stub) a inscribir.</param>
        /// <exception cref="InvalidOperationException">Si la inscripción está cerrada, llena, o el equipo ya está inscripto.</exception>
        /// <remarks>
        /// Esta es una operación transaccional pesada.
        /// 1. Valida que se pueda inscribir (cupos, estado, etc.).
        /// 2. Lo agrega a la tabla de unión (CompeticionRepository.AddEquipo).
        /// 3. Lo agrega a la tabla de posiciones (ClasificacionRepository.Add) con todo en cero.
        /// Si falla el paso 3, hace rollback del 2.
        /// </remarks>
        public void AñadirEquipo(Competicion competicion, Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    // Traemos la versión "posta" de la competición
                    var comp = context.Repositories.CompeticionRepository.GetById(competicion.IdCompeticion);
                    if (comp == null)
                        throw new KeyNotFoundException("La competición no existe.");

                    // BLL Check 1: ¿Ya se creó el fixture?
                    if (comp.Estado != EstadoCompeticion.SinFixture)
                        throw new InvalidOperationException("Las inscripciones están cerradas, el fixture ya fue creado.");

                    // BLL Check 2: ¿Hay cupo?
                    if (comp.ListaEquipos.Count >= comp.Cupos)
                        throw new InvalidOperationException("La competición ha alcanzado su cupo máximo de equipos.");

                    var equipoCompleto = context.Repositories.EquipoRepository.GetById(equipo.IdEquipo);
                    if (equipoCompleto == null || !equipoCompleto.Habilitado)
                    {
                        throw new InvalidOperationException($"El equipo '{equipo.Nombre}' no existe o está deshabilitado y no puede ser inscripto.");
                    }

                    // BLL Check 3: ¿Ya está adentro?
                    if (comp.ListaEquipos.Any(e => e.IdEquipo == equipo.IdEquipo))
                        throw new InvalidOperationException("El equipo ya está inscripto en esta competición.");

                    // OK, pasó los checks.
                    // 1. Inscribir equipo
                    context.Repositories.CompeticionRepository.AddEquipo(comp.IdCompeticion, equipo.IdEquipo);

                    // 2. Crear su fila en la tabla de posiciones
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
        /// <summary>
        /// Intenta generar el fixture completo para una competición.
        /// </summary>
        /// <param name="competicion">La competición (stub) a la que se le creará el fixture.</param>
        /// <returns>
        /// <c>null</c> si tuvo éxito.
        /// Una <c>List&lt;string&gt;</c> con los conflictos (ej: "horario ocupado") si falló.
        /// </returns>
        /// <remarks>
        /// Este es el método más complejo.
        /// 1. Valida que la competición esté lista (cupos mínimos, cancha asignada, etc.).
        /// 2. Llama a 'GenerarPartidosRoundRobin' para armar el fixture en memoria.
        /// 3. Recorre los partidos y chequea los horarios:
        ///    - Si el turno (slot) existe y está Libre: lo marca como 'OcupadoPorTorneo'.
        ///    - Si el turno existe y está Ocupado: ¡CONFLICTO!
        ///    - Si el turno NO existe: chequea si es válido (BLLFacade) y lo crea de cero.
        /// 4. Si hubo conflictos, frena y devuelve la lista de errores.
        /// 5. Si no hubo conflictos, ejecuta todas las operaciones pendientes (crear fixtures, ocupar slots)
        ///    y cambia el estado de la competición a 'ConFixture'.
        /// </remarks>
        public List<string> CrearFixture(Competicion competicion)
        {
            List<string> conflictos = new List<string>();
            // Lista de "cosas para hacer" si todo sale bien
            List<Action> operacionesPendientes = new List<Action>();

            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // 1. Validaciones de BLL
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

                    comp.canchaAsignada = canchaCompleta; // Hidratamos la cancha

                    // 2. Generar el fixture en memoria
                    var partidosPlaneados = GenerarPartidosRoundRobin(comp, comp.ListaEquipos);

                    // 3. Chequear conflictos de horarios
                    foreach (var partidoPlaneado in partidosPlaneados)
                    {
                        // 3a. ¿Ya existe el slot de horario?
                        var slotRequerido = context.Repositories.CanchaHorarioRepository.GetByCanchaYHora(
                            comp.canchaAsignada.IdCancha,
                            partidoPlaneado.HorarioRequerido);

                        if (slotRequerido != null)
                        {
                            // 3b. Existe. ¿Está libre?
                            if (slotRequerido.Estado != EstadoReserva.Libre)
                            {
                                // CONFLICTO: Alguien ya lo reservó
                                conflictos.Add($"El horario {partidoPlaneado.HorarioRequerido:g} ya está '{slotRequerido.Estado}'.");
                                continue;
                            }
                            else
                            {
                                // ÉXITO: Existe y está libre. Lo marcamos para ocupar
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
                            // 3c. No existe el slot. ¿Es un horario válido (ej: dentro del horario comercial)?
                            bool esValido = BLLFacade.Current.CanchaService.EsHorarioValido(comp.canchaAsignada.IdCancha, partidoPlaneado.HorarioRequerido);
                            if (!esValido)
                            {
                                // CONFLICTO: El horario cae fuera de la disponibilidad
                                conflictos.Add($"El horario {partidoPlaneado.HorarioRequerido:g} cae fuera de la disponibilidad de la cancha.");
                                continue;
                            }
                            else
                            {
                                // ÉXITO: El horario es válido. Lo marcamos para CREAR
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

                    // 4. Decisión final
                    if (conflictos.Any())
                    {
                        return conflictos; // Hubo bardo, devolvemos los errores
                    }
                    // Si NO hay conflictos, ejecutar todo
                    foreach (var operacion in operacionesPendientes)
                    {
                        operacion.Invoke(); // Mandamos todo a la DAL
                    }

                    // 5. Actualizar estado del torneo
                    comp.Estado = EstadoCompeticion.ConFixture;
                    context.Repositories.CompeticionRepository.Update(comp);

                    context.SaveChanges();
                    return null; // Éxito
                }
                catch (InvalidOperationException opEx)
                {
                    // Errores de validación (ej: franja horaria mal formateada)
                    return new List<string> { $"Error de configuración: {opEx.Message}" };
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// (No implementado) Borra una competición.
        /// </summary>
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene una lista de todas las competiciones.
        /// </summary>
        /// <returns>Colección de <see cref="Competicion"/>.</returns>
        public IEnumerable<Competicion> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetAll().ToList();
            }
        }

        /// <summary>
        /// Obtiene una competición específica por su ID, hidratada con sus equipos.
        /// </summary>
        /// <param name="id">El ID de la competición.</param>
        /// <returns>La <see cref="Competicion"/>.</returns>
        public Competicion GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetById(id);
            }
        }

        /// <summary>
        /// Obtiene una lista de competiciones que todavía tienen cupos libres.
        /// </summary>
        /// <param name="competicion">(Parámetro no usado) La competición a consultar.</param>
        /// <returns>Lista de <see cref="Competicion"/> con vacantes.</returns>
        /// <remarks>
        /// El repo que llama (<c>GetWithVacancies</c>) también sufre de N+1.
        /// </remarks>
        public List<Competicion> ListarConVacantes(Competicion competicion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetWithVacancies();
            }
        }

        /// <summary>
        /// Obtiene una lista de competiciones donde un cliente es capitán.
        /// </summary>
        /// <param name="cliente">El <see cref="Cliente"/> (capitán).</param>
        /// <returns>Lista de <see cref="Competicion"/>.</returns>
        /// <remarks>
        /// El repo que llama (<c>GetByClient</c>) también sufre de N+1.
        /// </remarks>
        public List<Competicion> ListarPorCliente(Cliente cliente)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetByClient(cliente);
            }
        }

        /// <summary>
        /// Quita un <see cref="Equipo"/> de una <see cref="Competicion"/>.
        /// </summary>
        /// <param name="competicion">La competición (stub).</param>
        /// <param name="equipo">El equipo (stub) a quitar.</param>
        /// <exception cref="InvalidOperationException">Si el fixture ya está creado.</exception>
        /// <remarks>
        /// Es la operación inversa a 'AñadirEquipo'. Es transaccional.
        /// 1. Valida que el fixture no esté creado.
        /// 2. Lo saca de la tabla de unión (RemoveEquipo).
        /// 3. Lo borra de la tabla de posiciones (ClasificacionRepository.Delete).
        /// </remarks>
        public void QuitarEquipo(Competicion competicion, Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    var comp = context.Repositories.CompeticionRepository.GetById(competicion.IdCompeticion);
                    if (comp == null)
                        throw new KeyNotFoundException("La competición no existe.");

                    // BLL Check: No se puede si ya empezó
                    if (comp.Estado != EstadoCompeticion.SinFixture)
                        throw new InvalidOperationException("No se puede quitar un equipo una vez que el fixture está creado.");

                    // 1. Quitar de la N:N (DbEquipoCompeticion)
                    context.Repositories.CompeticionRepository.RemoveEquipo(comp.IdCompeticion, equipo.IdEquipo);

                    // 2. Quitar de la tabla de posiciones
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

        /// <summary>
        /// Actualiza los datos de una <see cref="Competicion"/> (ej: nombre, cupos, precio).
        /// </summary>
        /// <param name="entity">La <see cref="Competicion"/> con los datos modificados.</param>
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

        /// <summary>
        /// Algoritmo Round Robin (privado) para generar la lista de partidos en memoria.
        /// </summary>
        /// <param name="comp">La competición (para las fechas y horarios).</param>
        /// <param name="equiposParaFixture">La lista de equipos a mezclar.</param>
        /// <returns>Una lista de <see cref="PartidoGenerado"/> (un DTO interno).</returns>
        /// <exception cref="InvalidOperationException">Si la franja horaria es inválida o no alcanza.</exception>
        /// <remarks>
        /// Este es el corazón del fixture. Si el número de equipos es impar,
        /// agrega un equipo "fantasma" (DESCANSA). Luego rota la lista
        /// para generar todas las rondas. También calcula la fecha y hora
        /// de cada partido y valida si entran en la franja horaria.
        /// </remarks>
        private List<PartidoGenerado> GenerarPartidosRoundRobin(Competicion comp, List<Equipo> equiposParaFixture)
        {
            var partidosPlaneados = new List<PartidoGenerado>();
            var equipos = new List<Equipo>(equiposParaFixture);

            // Si es impar, agrego un "Bye" o "Descansa"
            if (equipos.Count % 2 != 0)
            {
                equipos.Add(new Equipo { IdEquipo = Guid.Empty, Nombre = "DESCANSA" });
            }

            int numEquipos = equipos.Count;
            int numRondas = numEquipos - 1;
            int partidosPorRonda = numEquipos / 2;
            DateTime fechaPartido = comp.FechaInicio;

            // Validación de la franja horaria
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

                    // Si no es un partido contra "DESCANSA"
                    if (local.IdEquipo != Guid.Empty && visitante.IdEquipo != Guid.Empty)
                    {
                        TimeSpan horaFinPartido = horaPartidoActual.Add(TimeSpan.FromMinutes(duracionMinutos));

                        // BLL Check: ¿Nos pasamos de la hora de cierre?
                        if (horaFinPartido > horaFinRonda)
                        {
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
                        horaPartidoActual = horaFinPartido; // Avanzamos el reloj
                    }
                }

                // La magia del Round Robin: rotar la lista
                var equipoRotativo = equipos[1];
                equipos.RemoveAt(1);
                equipos.Add(equipoRotativo);

                // Avanzamos al próximo día de partido (ej: próximo sábado)
                fechaPartido = fechaPartido.AddDays(comp.Frecuencia); // Asumo Frecuencia=7
            }

            return partidosPlaneados;
        }

        /// <summary>
        /// Lógica de negocio para inactivar o archivar una competición.
        /// </summary>
        /// <param name="idCompeticion">El ID de la competición a modificar.</param>
        /// <exception cref="InvalidOperationException">Si el estado no permite la acción.</exception>
        /// <remarks>
        /// Este método es una máquina de estados.
        /// - Si está 'SinFixture': La puede 'Cancelar'. Borra equipos y clasificaciones.
        /// - Si está 'Finalizado': La puede 'Archivar' (solo si no quedan partidos pendientes).
        /// - Si está 'ConFixture' (en curso): ¡ERROR! No se puede tocar.
        /// - Si ya está 'Cancelado' o 'Archivado': ¡ERROR! Ya está inactivo.
        /// </desc>
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
                        // REGLA 1: Cancelar un torneo (SIN FIXTURE)
                        case EstadoCompeticion.SinFixture:
                            var equiposInscriptos = new List<Equipo>();
                            //
                            foreach (var equipoStub in comp.ListaEquipos)
                            {
                                var equipoFull = context.Repositories.EquipoRepository.GetById(equipoStub.IdEquipo);
                                if (equipoFull != null)
                                    equiposInscriptos.Add(equipoFull);
                            }

                            // Borro las clasifaciones de los equipos en el torneo
                            foreach (var equipo in equiposInscriptos)
                            {
                                var clasif = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(comp, equipo);
                                if (clasif != null)
                                {
                                    context.Repositories.ClasificacionRepository.Delete(clasif.IdClasificacion);
                                }
                            }

                            // Quito los equipos
                            context.Repositories.CompeticionRepository.RemoveAllEquipos(idCompeticion);

                            // Cambio estado
                            comp.Estado = EstadoCompeticion.Cancelado;
                            context.Repositories.CompeticionRepository.Update(comp);

                            context.SaveChanges();
                            break;

                        // REGLA 2: Archivar un torneo (FINALIZADO)
                        case EstadoCompeticion.Finalizado:
                            // BLL Check: ¿Seguro que terminó? ¿No quedan partidos?
                            var partidosPendientes = context.Repositories.FixtureRepository.GetByCompeticionPendientes(idCompeticion);
                            if (partidosPendientes.Any())
                            {
                                throw new InvalidOperationException("No se puede archivar. Esta competición está marcada como 'Finalizada', pero todavía tiene partidos pendientes de disputar.");
                            }
                            comp.Estado = EstadoCompeticion.Archivado;
                            context.Repositories.CompeticionRepository.Update(comp);
                            context.SaveChanges();
                            break;

                        // REGLA 3: Prohibir (EN CURSO)
                        case EstadoCompeticion.ConFixture:
                            throw new InvalidOperationException("No se puede inactivar una competición que ya tiene un fixture generado o está en curso.");

                        // REGLA 4: Ya está inactivo
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

        // Esta clase es un DTO interno (un "ayudante")
        // solo para que 'GenerarPartidosRoundRobin' sea más legible.
        private class PartidoGenerado
        {
            public Equipo EquipoLocal { get; set; }
            public Equipo EquipoVisitante { get; set; }
            public DateTime HorarioRequerido { get; set; }
        }
    }
}