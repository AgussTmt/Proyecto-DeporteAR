using DomainModel;

using System;

/// <summary>
/// Representa un horario (timeslot) específico para una cancha en una fecha determinada.
/// Es la entidad central para gestionar reservas.
/// </summary>
public class CanchaHorario
{
    /// <summary>
    /// Identificador único del horario de cancha.
    /// </summary>
    public Guid IdCanchaHorario { get; set; }

    /// <summary>
    /// Indica si la reserva para este horario ya ha sido abonada (pagada).
    /// </summary>
    public bool Abonada { get; set; }

    /// <summary>
    /// La fecha y hora exactas de inicio de este turno/horario.
    /// </summary>
    public DateTime FechaHorario { get; set; }

    /// <summary>
    /// Bandera (flag) para indicar si esta reserva ha sido modificada (ej: reprogramada).
    /// </summary>
    public bool FueCambiada { get; set; }

    /// <summary>
    /// El cliente que ha realizado la reserva para este horario.
    /// Es <c>null</c> si el estado es 'Libre'.
    /// </summary>
    public Cliente ReservadaPor { get; set; }

    /// <summary>
    /// El estado actual de este horario (ej: Libre, Reservada, etc.).
    /// </summary>
    public EstadoReserva Estado { get; set; }

    /// <summary>
    /// La cancha a la que pertenece este horario.
    /// </summary>
    public Cancha Cancha { get; set; }
}

/// <summary>
/// Enumera los posibles estados de un <see cref="CanchaHorario"/>.
/// </summary>
public enum EstadoReserva
{
    Libre,
    Espera,
    Reservada,
    Cancelada,
    OcupadoPorTorneo
}