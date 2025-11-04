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
    /// Capa de Lógica de Negocio (BLL) para gestionar las entidades <see cref="Cancha"/>.
    /// Se encarga del ABM (CRUD) de la cancha "física" y su disponibilidad.
    /// </summary>
    /// <remarks>
    /// Esta clase usa la Unidad de Trabajo (Unit of Work) para asegurar que la
    /// creación de una Cancha, su Disponibilidad Semanal y la generación
    /// inicial de sus turnos (slots) se hagan en una sola transacción.
    /// </remarks>
    internal class CanchaService : ICanchaService
    {
        /// <summary>
        /// Agrega una nueva <see cref="Cancha"/>, su plantilla de disponibilidad semanal,
        /// y pre-genera la primera semana de turnos (slots) para reservar.
        /// </summary>
        /// <param name="entity">La <see cref="Cancha"/> a crear.</param>
        /// <param name="disponibilidad">Un diccionario con la plantilla de horarios (ej: Lunes, 09:00-22:00).</param>
        public void Add(Cancha entity, Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)> disponibilidad)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // Validación de BLL (agregada)
                    if (entity.DuracionXPartidoMin <= 0)
                        throw new InvalidOperationException("La duración del partido (DuracionXPartidoMin) debe ser mayor a 0.");

                    // 1. Agrega la cancha
                    context.Repositories.CanchaRepository.Add(entity);

                    // 2. Agrega la plantilla de disponibilidad semanal
                    foreach (var kvp in disponibilidad)
                    {
                        var disp = new CanchaDisponibilidad
                        {
                            IdDisponibilidad = Guid.NewGuid(),
                            IdCancha = entity.IdCancha,
                            DiaSemana = kvp.Key,
                            HoraInicio = kvp.Value.start,
                            HoraFin = kvp.Value.end
                        };

                        context.Repositories.CanchaDisponibilidadRepository.Add(disp);
                    }

                    // 3. Genera los slots (turnos) para la primera semana
                    int duracionMinutos = entity.DuracionXPartidoMin; //
                    DateTime proximaSemana = DateTime.Today.AddDays(7); // Horizonte de 7 días

                    for (DateTime diaActual = DateTime.Today.AddDays(1); diaActual < proximaSemana; diaActual = diaActual.AddDays(1))
                    {
                        // Si la cancha abre este día de la semana...
                        if (disponibilidad.TryGetValue(diaActual.DayOfWeek, out var franja))
                        {
                            TimeSpan horaActual = franja.start; // <--- Usamos el TimeSpan completo
                            TimeSpan horaFinFranja = franja.end;

                            //
                            while (horaActual.Add(TimeSpan.FromMinutes(duracionMinutos)) <= horaFinFranja)
                            {
                                var slotTime = diaActual.Date.Add(horaActual);
                                var newSlot = new CanchaHorario
                                {
                                    IdCanchaHorario = Guid.NewGuid(),
                                    Cancha = new Cancha { IdCancha = entity.IdCancha }, // Stub
                                    FechaHorario = slotTime,
                                    Estado = EstadoReserva.Libre,
                                    Abonada = false,
                                    FueCambiada = false,
                                    ReservadaPor = null
                                };
                                context.Repositories.CanchaHorarioRepository.Add(newSlot);

                                horaActual = horaActual.Add(TimeSpan.FromMinutes(duracionMinutos)); // <--- Avanzamos el slot
                            }
                        }
                    }

                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }

        /// <summary>
        /// Cambia el estado de habilitación de una cancha (Habilitada / Deshabilitada).
        /// </summary>
        /// <param name="id">El ID de la cancha a modificar.</param>
        /// <remarks>
        /// Esta no es un simple toggle. Tiene lógica de negocio pesada.
        /// Antes de *deshabilitar* una cancha, chequea dos cosas:
        /// 1. Que no esté asignada a un torneo activo.
        /// 2. Que no tenga reservas o partidos a futuro.
        /// Si alguna de esas es verdadera, tira una excepción.
        /// Para *habilitar* no hay chequeos, simplemente la habilita.
        /// </remarks>
        public void CambiarHabilitado(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    //busco cancha
                    var cancha = context.Repositories.CanchaRepository.GetById(id);
                    if (cancha == null)
                        throw new KeyNotFoundException("La cancha no fue encontrada.");

                    bool estaActualmenteHabilitada = cancha.Estado;


                    if (estaActualmenteHabilitada)
                    {    // Lógica para DESHABILITAR

                        // Check 1: ¿es usada por competiciones?
                        var competicionesAsignadas = context.Repositories.CompeticionRepository.GetByCancha(id);

                        bool tieneCompeticionActiva = competicionesAsignadas.Any(c =>
                            c.Estado == EstadoCompeticion.SinFixture ||
                            c.Estado == EstadoCompeticion.ConFixture);

                        if (tieneCompeticionActiva)
                        {
                            throw new InvalidOperationException("No se puede deshabilitar. Esta cancha está asignada a una o más competiciones activas.");
                        }

                        // Check 2: ¿algun fulano ya la reservo?
                        int slotsOcupadosFuturos = context.Repositories.CanchaHorarioRepository.CountSlotsOcupadosFuturos(id);

                        if (slotsOcupadosFuturos > 0)
                        {
                            throw new InvalidOperationException($"No se puede deshabilitar. Esta cancha tiene {slotsOcupadosFuturos} reservas o partidos programados a futuro.");
                        }

                        //todo ok
                        context.Repositories.CanchaRepository.CambiarHabilitado(id);
                        context.SaveChanges();
                    }
                    else
                    {
                        // Lógica para HABILITAR
                        // no hay chequeos, simplemente la habilita
                        context.Repositories.CanchaRepository.CambiarHabilitado(id);
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
        /// Obtiene una lista de todas las canchas HABILITADAS.
        /// </summary>
        /// <returns>Colección de <see cref="Cancha"/>.</returns>
        public IEnumerable<Cancha> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaRepository.GetAll();
            }
        }

        /// <summary>
        /// Obtiene una cancha específica por su ID.
        /// </summary>
        /// <param name="id">El ID de la cancha.</param>
        /// <returns>La <see cref="Cancha"/>.</returns>
        public Cancha GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaRepository.GetById(id);
            }
        }

        /// <summary>
        /// Actualiza una <see cref="Cancha"/> y su plantilla de disponibilidad semanal.
        /// </summary>
        /// <param name="entity">La <see cref="Cancha"/> con los datos actualizados.</param>
        /// <param name="disponibilidad">El *nuevo* diccionario de disponibilidad semanal.</param>
        /// <remarks>
        /// Esta es una operación transaccional. Primero actualiza la cancha (ej: precio, nombre)
        /// y luego usa una estrategia "delete-then-insert": borra *toda* la
        /// disponibilidad semanal vieja y guarda la nueva.
        /// Ojo: Esto *no* afecta los <c>CanchaHorario</c> (turnos) ya generados.
        /// </remarks>
        public void Update(Cancha entity, Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)> disponibilidad)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // 1. Actualiza la entidad Cancha
                    context.Repositories.CanchaRepository.Update(entity);

                    // 2. Borra la disponibilidad vieja
                    context.Repositories.CanchaDisponibilidadRepository.DeleteByCancha(entity.IdCancha);

                    // 3. Inserta la disponibilidad nueva
                    foreach (var kvp in disponibilidad)
                    {
                        var disp = new CanchaDisponibilidad
                        {
                            IdDisponibilidad = Guid.NewGuid(),
                            IdCancha = entity.IdCancha,
                            DiaSemana = kvp.Key,
                            HoraInicio = kvp.Value.start,
                            HoraFin = kvp.Value.end
                        };
                        context.Repositories.CanchaDisponibilidadRepository.Add(disp);
                    }

                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }


        /// <summary>
        /// Obtiene la plantilla de disponibilidad semanal (horario comercial) de una cancha.
        /// </summary>
        /// <param name="idCancha">El ID de la cancha.</param>
        /// <returns>Un Diccionario (Día -> Tupla de Horas) para fácil acceso.</returns>
        /// <remarks>
        /// Es un método helper muy útil. Llama al repositorio, que devuelve una
        /// <c>List</c>, y este método la convierte en un <c>Dictionary</c>
        /// que es mucho más fácil de consultar en la BLL.
        /// </remarks>
        public Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)> GetDisponibilidadSemanal(Guid idCancha)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                var listaDb = context.Repositories.CanchaDisponibilidadRepository.GetByCancha(idCancha);


                return listaDb.ToDictionary(
                    disp => disp.DiaSemana,
                    disp => (disp.HoraInicio, disp.HoraFin)
                );
            }
        }

        /// <summary>
        /// Obtiene una lista de TODAS las canchas, incluyendo las Deshabilitadas.
        /// (Ideal para paneles de administración).
        /// </summary>
        /// <returns>Colección de <see cref="Cancha"/>.</returns>
        public IEnumerable<Cancha> GetAllIncludingDisabled()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaRepository.GetAllIncludingDisabled();
            }
        }

        /// <summary>
        /// Chequeo de lógica de negocio: ¿La cancha está abierta (según su
        /// plantilla de disponibilidad) en esta fecha y hora?
        /// </summary>
        /// <param name="idCancha">El ID de la cancha a chequear.</param>
        /// <param name="fechaHora">La fecha y hora a validar.</param>
        /// <returns><c>true</c> si la cancha está abierta, <c>false</c> si está cerrada.</returns>
        public bool EsHorarioValido(Guid idCancha, DateTime fechaHora)
        {
            try
            {
                // Llama al helper de esta misma clase
                var disponibilidad = GetDisponibilidadSemanal(idCancha);

                // Chequea si hay config para ese día
                if (disponibilidad.TryGetValue(fechaHora.DayOfWeek, out var franja))
                {
                    // Chequea si la hora está en el rango [Inicio, Fin)
                    TimeSpan horaDelDia = fechaHora.TimeOfDay;
                    return horaDelDia >= franja.start && horaDelDia < franja.end;
                }
                else
                {
                    // No abre ese día de la semana
                    return false;
                }
            }
            catch
            {
                // Si falla (ej: la cancha no existe), asumimos que no es válido
                return false;
            }
        }
    }
}