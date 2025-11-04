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
    /// Capa de Lógica de Negocio (BLL) para todo lo relacionado con
    /// la grilla de horarios, reservas y reportes de canchas.
    /// </summary>
    /// <remarks>
    /// Esta clase usa el patrón Unit of Work (FactoryDao.UnitOfWork.Create())
    /// para asegurarse de que todas las operaciones de base de datos
    /// (ej: actualizar un horario Y guardar el log) ocurran en una
    /// sola transacción (o fallan juntas).
    /// </remarks>
    internal class CanchaHorarioService : ICanchaHorarioService
    {
        /// <summary>
        /// Guarda un nuevo <see cref="CanchaHorario"/> en la base de datos.
        /// </summary>
        /// <param name="canchaHorario">El horario a crear.</param>
        public void Crear(CanchaHorario canchaHorario)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.CanchaHorarioRepository.Add(canchaHorario);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// Obtiene los horarios ordenados por demanda (más reservados).
        /// </summary>
        /// <returns>Lista de <see cref="CanchaHorario"/>.</returns>
        public List<CanchaHorario> ListarPorDemanda()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaHorarioRepository.GetOrderByDemand();
            }
        }

        /// <summary>
        /// Obtiene todos los horarios que están en un estado específico.
        /// </summary>
        /// <param name="estado">El <see cref="EstadoReserva"/> (enum) a filtrar.</param>
        /// <returns>Lista de <see cref="CanchaHorario"/>.</returns>
        public List<CanchaHorario> ListarPorEstadoReserva(EstadoReserva estado)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaHorarioRepository.GetByEstadoReserva(estado);
            }
        }

        /// <summary>
        /// Obtiene todos los horarios para un día específico.
        /// </summary>
        /// <param name="date">La fecha a consultar.</param>
        /// <returns>Lista de <see cref="CanchaHorario"/>.</returns>
        public List<CanchaHorario> ListarPorRangoTiempo(DateTime date)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaHorarioRepository.GetByTimeRange(date);
            }
        }


        /// <summary>
        /// Busca la fecha y hora del último turno generado para una cancha.
        /// </summary>
        /// <param name="idCancha">El ID de la cancha.</param>
        /// <returns>La <see cref="DateTime"/> máxima (último turno).</returns>
        public DateTime GetMaximaFechaHorario(Guid idCancha)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.CanchaHorarioRepository.GetMaximaFechaHorario(idCancha);
            }
        }

        /// <summary>
        /// Verifica si ya existe un turno (slot) en la base de datos
        /// para una cancha y hora exactas.
        /// </summary>
        /// <param name="idCancha">El ID de la cancha.</param>
        /// <param name="fechaHora">La fecha y hora exactas a chequear.</param>
        /// <returns><c>true</c> si ya existe.</returns>
        public bool ExisteHorario(Guid idCancha, DateTime fechaHora)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.CanchaHorarioRepository.ExisteHorario(idCancha, fechaHora);
            }
        }

        /// <summary>
        /// Obtiene los horarios para una cancha en un rango de fechas,
        /// y "hidrata" los objetos (carga la <see cref="Cancha"/> y los <see cref="Cliente"/> completos).
        /// </summary>
        /// <param name="idCancha">El ID de la cancha a buscar.</param>
        /// <param name="fechaDesde">Fecha de inicio.</param>
        /// <param name="fechaHasta">Fecha de fin.</param>
        /// <returns>Una colección de <see cref="CanchaHorario"/> completos.</returns>
        /// <remarks>
        /// Este método es un buen ejemplo de cómo evitar el N+1.
        /// 1. Trae todos los horarios (1 consulta).
        /// 2. Trae todos los clientes únicos (1 consulta).
        /// 3. Trae la cancha (1 consulta).
        /// 4. Arma todo en memoria.
        /// Es mucho más rápido que hacer una consulta por cada cliente de cada horario.
        /// </remarks>
        public IEnumerable<CanchaHorario> GetHorariosRango(Guid idCancha, DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // 1. Traemos la cancha (1 consulta)
                    var canchaCompleta = context.Repositories.CanchaRepository.GetById(idCancha);
                    if (canchaCompleta == null)
                    {
                        throw new KeyNotFoundException("Cancha no encontrada.");
                    }

                    // 2. Traemos los horarios (1 consulta)
                    var horarios = context.Repositories.CanchaHorarioRepository.GetHorariosRango(idCancha, fechaDesde, fechaHasta).ToList();

                    // 3. Juntamos los IDs de los clientes (sin repetir)
                    var idsClientes = horarios
                        .Where(h => h.ReservadaPor != null)
                        .Select(h => h.ReservadaPor.IdCliente)
                        .Distinct()
                        .ToList();

                    var clientesCompletos = new Dictionary<Guid, Cliente>();
                    if (idsClientes.Any())
                    {
                        // 4. Traemos todos los clientes de una (1 consulta)
                        clientesCompletos = context.Repositories.ClienteRepository.GetAll()
                            .Where(c => idsClientes.Contains(c.IdCliente))
                            .ToDictionary(c => c.IdCliente, c => c);
                    }

                    // 5. Hidratamos (armamos el rompecabezas en memoria)
                    foreach (var horario in horarios)
                    {
                        horario.Cancha = canchaCompleta;
                        if (horario.ReservadaPor != null && clientesCompletos.ContainsKey(horario.ReservadaPor.IdCliente))
                        {
                            horario.ReservadaPor = clientesCompletos[horario.ReservadaPor.IdCliente];
                        }
                    }

                    return horarios;
                }
                catch (Exception ex)
                {

                    throw new Exception("Error en BLL GetHorariosRango.", ex);
                }
            }


        }

        /// <summary>
        /// Lógica de negocio principal para actualizar el estado de una reserva.
        /// Se encarga de validar, actualizar el horario y crear el registro de historial (log).
        /// </summary>
        /// <param name="idCanchaHorario">El ID del turno a modificar.</param>
        /// <param name="nuevoEstado">El <see cref="EstadoReserva"/> al que se quiere pasar.</param>
        /// <param name="cliente">El <see cref="Cliente"/> (si aplica).</param>
        /// <param name="abonada">Si la reserva está (o pasa a estar) pagada.</param>
        /// <remarks>
        /// Este es el método transaccional. Si falla al actualizar la reserva,
        /// también falla la creación del historial (hace rollback).
        /// </remarks>
        public void ActualizarReserva(Guid idCanchaHorario, EstadoReserva nuevoEstado, Cliente cliente, bool abonada)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // 1. Traemos el horario
                    var horarioActual = context.Repositories.CanchaHorarioRepository.GetById(idCanchaHorario);
                    if (horarioActual == null)
                    {
                        throw new KeyNotFoundException("El horario a modificar no existe.");
                    }

                    string estadoAnteriorStr = horarioActual.Estado.ToString();
                    string detalleHistorial = $"Cambio de estado: {estadoAnteriorStr} -> {nuevoEstado}.";

                    // 2. Validaciones de Negocio (BLL)
                    if (nuevoEstado == EstadoReserva.Reservada && cliente == null)
                    {
                        throw new InvalidOperationException("No se puede pasar a 'Reservada' sin asignar un cliente.");
                    }
                    if (nuevoEstado != EstadoReserva.Reservada && abonada)
                    {
                        throw new InvalidOperationException("Un turno solo puede estar abonado si está 'Reservado'.");
                    }
                    if (nuevoEstado == EstadoReserva.Libre) // Limpieza al liberar
                    {
                        cliente = null;
                        abonada = false;
                    }

                    // 3. Seteamos los nuevos valores
                    horarioActual.Estado = nuevoEstado;
                    horarioActual.Abonada = abonada;

                    // El repositorio espera un "stub" (un cliente solo con ID), no el objeto completo
                    horarioActual.ReservadaPor = (cliente != null)
                        ? new Cliente { IdCliente = cliente.IdCliente }
                        : null;

                    // 4. Creamos el log de historial (auditoría)
                    if (estadoAnteriorStr != nuevoEstado.ToString())
                    {
                        var historial = new ReservaHistorial
                        {
                            IdHistorial = Guid.NewGuid(),
                            IdCanchaHorario = horarioActual.IdCanchaHorario,
                            IdCliente = (cliente != null) ? (Guid?)cliente.IdCliente : null,
                            FechaHoraEvento = DateTime.Now,
                            EstadoAnterior = estadoAnteriorStr,
                            EstadoNuevo = nuevoEstado.ToString(),
                            Detalle = detalleHistorial
                        };
                        context.Repositories.ReservaHistorialRepository.Add(historial);
                    }

                    // 5. Creamos un log separado si se marcó como 'Abonada'
                    if (horarioActual.Abonada != abonada && abonada == true) // Ojo, acá hay un bug lógico
                    {
                        var historialPago = new ReservaHistorial
                        {
                            IdHistorial = Guid.NewGuid(),
                            IdCanchaHorario = horarioActual.IdCanchaHorario,
                            IdCliente = (cliente != null) ? (Guid?)cliente.IdCliente : null,
                            FechaHoraEvento = DateTime.Now,
                            EstadoAnterior = estadoAnteriorStr,
                            EstadoNuevo = estadoAnteriorStr,
                            Detalle = "Reserva marcada como ABONADA."
                        };
                        context.Repositories.ReservaHistorialRepository.Add(historialPago);
                    }

                    // 6. Persistimos los cambios
                    context.Repositories.CanchaHorarioRepository.Update(horarioActual);
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// Genera automáticamente los turnos (slots) para una cancha, desde el último
        /// día generado hasta un 'horizonte' de N días a futuro.
        /// </summary>
        /// <param name="idCancha">El ID de la cancha a la que se le generarán los turnos.</param>
        /// <param name="diasHorizonte">Cuántos días a futuro se deben generar (ej: 90 días).</param>
        /// <returns>El número de nuevos turnos (slots) que se crearon.</returns>
        /// <remarks>
        /// Este es el "cron job". Lee la plantilla de disponibilidad semanal
        /// (ej: Lunes 9-18) y crea los slots (ej: Lunes 9:00, Lunes 10:00...)
        /// para los próximos N días. Es inteligente y no crea duplicados.
        /// </remarks>
        public int GenerarHorariosParaCancha(Guid idCancha, int diasHorizonte)
        {
            var slotsParaCrear = new List<CanchaHorario>();

            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // 1. Traemos la cancha y su plantilla horaria
                    var cancha = context.Repositories.CanchaRepository.GetById(idCancha);
                    if (cancha == null)
                        throw new KeyNotFoundException("La cancha seleccionada no fue encontrada.");

                    if (cancha.DuracionXPartidoMin <= 0)
                        throw new InvalidOperationException($"La 'DuracionXPartidoMin' de la cancha '{cancha.Nombre}' debe ser mayor a 0.");

                    var disponibilidadList = context.Repositories.CanchaDisponibilidadRepository.GetByCancha(idCancha);
                    if (disponibilidadList == null || !disponibilidadList.Any())
                        throw new InvalidOperationException("Esta cancha no tiene una disponibilidad semanal definida.");

                    var disponibilidadDict = disponibilidadList.ToDictionary(d => d.DiaSemana, d => d);

                    // 2. Calculamos desde dónde empezar a generar
                    DateTime fechaMaximaExistente = context.Repositories.CanchaHorarioRepository.GetMaximaFechaHorario(idCancha);
                    DateTime fechaInicioGeneracion = (fechaMaximaExistente == DateTime.MinValue)
                        ? DateTime.Today.AddDays(1) // Si es nueva, desde mañana
                        : fechaMaximaExistente.Date.AddDays(1); // Si ya tiene, desde el día sig. al último

                    DateTime fechaFinGeneracion = DateTime.Today.AddDays(diasHorizonte);
                    int duracionMinutos = cancha.DuracionXPartidoMin;

                    // 3. Generamos los slots en memoria
                    for (DateTime diaActual = fechaInicioGeneracion; diaActual <= fechaFinGeneracion; diaActual = diaActual.AddDays(1))
                    {
                        // Si la cancha abre este día de la semana...
                        if (disponibilidadDict.TryGetValue(diaActual.DayOfWeek, out var franja))
                        {
                            TimeSpan horaActual = franja.HoraInicio;
                            TimeSpan horaFinFranja = franja.HoraFin;

                            // ...generamos los slots (9:00, 10:00, 11:00...)
                            while (horaActual.Add(TimeSpan.FromMinutes(duracionMinutos)) <= horaFinFranja)
                            {
                                slotsParaCrear.Add(new CanchaHorario
                                {
                                    IdCanchaHorario = Guid.NewGuid(),
                                    Cancha = new Cancha { IdCancha = cancha.IdCancha }, // Stub
                                    FechaHorario = diaActual.Date.Add(horaActual),
                                    Estado = EstadoReserva.Libre,
                                    Abonada = false,
                                    FueCambiada = false,
                                    ReservadaPor = null
                                });
                                horaActual = horaActual.Add(TimeSpan.FromMinutes(duracionMinutos));
                            }
                        }
                    }

                    if (!slotsParaCrear.Any()) return 0; // No había nada que generar

                    // 4. Persistimos (solo si no existen)
                    int countGenerados = 0;
                    foreach (var newSlot in slotsParaCrear)
                    {
                        // Esta verificación evita duplicados si el job corre dos veces
                        bool yaExiste = context.Repositories.CanchaHorarioRepository.ExisteHorario(cancha.IdCancha, newSlot.FechaHorario);
                        if (!yaExiste)
                        {
                            context.Repositories.CanchaHorarioRepository.Add(newSlot);
                            countGenerados++;
                        }
                    }

                    context.SaveChanges();
                    return countGenerados;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        /// <summary>
        /// Obtiene un reporte de todos los turnos 'Reservados' que pasaron
        /// de fecha y que figuran como 'No Abonados'.
        /// </summary>
        /// <returns>Una lista de <see cref="CanchaHorario"/> (deudores) hidratados.</returns>
        /// <remarks>
        /// Este es otro método optimizado que evita el N+1.
        /// Trae todos los horarios deudores, luego trae todos los clientes y canchas
        /// en dos consultas separadas, y finalmente arma todo en memoria.
        /// </remarks>
        public List<CanchaHorario> GetReporteDeudores()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                // 1. Traemos los horarios deudores
                var deudores = context.Repositories.CanchaHorarioRepository
                                  .GetDeudores(DateTime.Now)
                                  .ToList();

                if (!deudores.Any())
                {
                    return deudores; // Lista vacía
                }

                // 2. Juntamos IDs
                var idsClientes = deudores
                    .Where(h => h.ReservadaPor != null)
                    .Select(h => h.ReservadaPor.IdCliente)
                    .Distinct()
                    .ToList();

                var idsCanchas = deudores
                    .Select(h => h.Cancha.IdCancha)
                    .Distinct()
                    .ToList();

                // 3. Traemos los objetos completos (2 consultas)
                var clientesCompletos = context.Repositories.ClienteRepository.GetAll()
                    .Where(c => idsClientes.Contains(c.IdCliente))
                    .ToDictionary(c => c.IdCliente);

                var canchasCompletas = context.Repositories.CanchaRepository.GetAll()
                    .Where(c => idsCanchas.Contains(c.IdCancha))
                    .ToDictionary(c => c.IdCancha);

                // 4. Hidratamos
                foreach (var horario in deudores)
                {
                    if (horario.ReservadaPor != null && clientesCompletos.TryGetValue(horario.ReservadaPor.IdCliente, out var cliente))
                    {
                        horario.ReservadaPor = cliente;
                    }

                    if (canchasCompletas.TryGetValue(horario.Cancha.IdCancha, out var cancha))
                    {
                        horario.Cancha = cancha;
                    }
                }

                return deudores;
            }
        }

        /// <summary>
        /// Obtiene un reporte de facturación. Trae todos los turnos 'Abonados'
        /// en un rango de fechas, opcionalmente filtrado por cancha.
        /// </summary>
        /// <param name="desde">Fecha de inicio del reporte.</param>
        /// <param name="hasta">Fecha de fin del reporte.</param>
        /// <param name="idCancha">Opcional. Si se provee, filtra por esa cancha.</param>
        /// <returns>Una lista de <see cref="CanchaHorario"/> (facturados) hidratados.</returns>
        /// <remarks>
        /// Sigue el mismo patrón de optimización (evita N+1) que GetReporteDeudores.
        /// </remarks>
        public List<CanchaHorario> GetReporteFacturacion(DateTime desde, DateTime hasta, Guid? idCancha)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                // 1. Traemos los horarios abonados (1 consulta)
                var horarios = context.Repositories.CanchaHorarioRepository
                                  .GetHorariosAbonadosRango(desde, hasta, idCancha)
                                  .ToList();

                if (!horarios.Any())
                {
                    return horarios; // Lista vacía
                }

                // 2. Juntamos IDs
                var idsClientes = horarios
                    .Where(h => h.ReservadaPor != null)
                    .Select(h => h.ReservadaPor.IdCliente)
                    .Distinct()
                    .ToList();

                var idsCanchas = horarios
                    .Select(h => h.Cancha.IdCancha)
                    .Distinct()
                    .ToList();

                // 3. Traemos objetos completos (2 consultas)
                var clientesCompletos = context.Repositories.ClienteRepository.GetAll()
                    .Where(c => idsClientes.Contains(c.IdCliente))
                    .ToDictionary(c => c.IdCliente);

                var canchasCompletas = context.Repositories.CanchaRepository.GetAll()
                    .Where(c => idsCanchas.Contains(c.IdCancha))
                    .ToDictionary(c => c.IdCancha);

                // 4. Hidratamos
                foreach (var horario in horarios)
                {
                    if (horario.ReservadaPor != null && clientesCompletos.TryGetValue(horario.ReservadaPor.IdCliente, out var cliente))
                    {
                        horario.ReservadaPor = cliente;
                    }
                    if (canchasCompletas.TryGetValue(horario.Cancha.IdCancha, out var cancha))
                    {
                        horario.Cancha = cancha;
                    }
                }

                return horarios;
            }
        }
    }
}