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
    internal class CanchaHorarioService : ICanchaHorarioService
    {


        public void AsignarCliente(Cliente cliente, CanchaHorario canchaHorario)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    var horarioActual = context.Repositories.CanchaHorarioRepository.GetById(canchaHorario.IdCanchaHorario);

                    if (horarioActual == null)
                        throw new KeyNotFoundException("El horario seleccionado no existe.");
                    string estadoAnterior = horarioActual.Estado.ToString();

                    if (horarioActual.Estado != EstadoReserva.Libre)
                        throw new InvalidOperationException("Solo se puede asignar un cliente a un horario que esté 'Libre'.");

                    context.Repositories.CanchaHorarioRepository.AssignCliente(cliente, horarioActual);

                    var historial = new ReservaHistorial
                    {
                        IdHistorial = Guid.NewGuid(),
                        IdCanchaHorario = horarioActual.IdCanchaHorario,
                        IdCliente = cliente.IdCliente, // El cliente que reserva
                        FechaHoraEvento = DateTime.Now,
                        EstadoAnterior = estadoAnterior,
                        EstadoNuevo = EstadoReserva.Reservada.ToString(), // El estado al que cambió
                        Detalle = $"Reservado por cliente {cliente.Nombre}"
                    };
                    context.Repositories.ReservaHistorialRepository.Add(historial);


                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void AsignarEstado(CanchaHorario canchaHorario, EstadoReserva estado)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    var horarioActual = context.Repositories.CanchaHorarioRepository.GetById(canchaHorario.IdCanchaHorario);

                    if (horarioActual == null)
                        throw new KeyNotFoundException("El horario seleccionado no existe.");

                    string estadoAnterior = horarioActual.Estado.ToString();
                    horarioActual.Estado = estado;
                    Guid? idClienteActual = horarioActual.ReservadaPor?.IdCliente;


                    if (estado == EstadoReserva.Cancelada)
                    {
                        horarioActual.ReservadaPor = null;
                        horarioActual.Abonada = false;
                    }

                    context.Repositories.CanchaHorarioRepository.Update(horarioActual);

                    var historial = new ReservaHistorial
                    {
                        IdHistorial = Guid.NewGuid(),
                        IdCanchaHorario = horarioActual.IdCanchaHorario,
                        IdCliente = idClienteActual,
                        FechaHoraEvento = DateTime.Now,
                        EstadoAnterior = estadoAnterior,
                        EstadoNuevo = estado.ToString(),
                        Detalle = $"Estado cambiado a {estado}"
                    };
                    context.Repositories.ReservaHistorialRepository.Add(historial);


                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

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

        public List<CanchaHorario> ListarPorCliente(Cliente cliente)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.CanchaHorarioRepository.GetByCliente(cliente);
            }
        }

        public List<CanchaHorario> ListarPorDemanda()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaHorarioRepository.GetOrderByDemand();
            }
        }

        public List<CanchaHorario> ListarPorEstadoReserva(EstadoReserva estado)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaHorarioRepository.GetByEstadoReserva(estado);
            }
        }

        public List<CanchaHorario> ListarPorRangoTiempo(DateTime date)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaHorarioRepository.GetByTimeRange(date);
            }
        }

        public void AbonarReserva(Guid idCanchaHorario)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    var horarioActual = context.Repositories.CanchaHorarioRepository.GetById(idCanchaHorario);

                    //Validaciones
                    if (horarioActual == null)
                    {
                        throw new KeyNotFoundException("La reserva especificada no existe.");
                    }
                    if (horarioActual.Estado != EstadoReserva.Reservada)
                    {

                        throw new InvalidOperationException($"Solo se pueden abonar reservas en estado 'Reservada'. Estado actual: '{horarioActual.Estado}'.");
                    }
                    if (horarioActual.Abonada)
                    {
                        throw new InvalidOperationException("Esta reserva ya se encuentra abonada.");
                    }
                    if (horarioActual.ReservadaPor == null)
                    {
                        throw new InvalidOperationException("Esta reserva no tiene un cliente asignado para abonar.");
                    }

                    string estadoAnterior = horarioActual.Estado.ToString();


                    horarioActual.Abonada = true;
                    context.Repositories.CanchaHorarioRepository.Update(horarioActual);


                    var historial = new ReservaHistorial
                    {
                        IdHistorial = Guid.NewGuid(),
                        IdCanchaHorario = horarioActual.IdCanchaHorario,
                        IdCliente = horarioActual.ReservadaPor.IdCliente,
                        FechaHoraEvento = DateTime.Now,
                        EstadoAnterior = estadoAnterior,
                        EstadoNuevo = "Pagada",
                        Detalle = "Reserva marcada como abonada."
                    };


                    context.Repositories.ReservaHistorialRepository.Add(historial);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    // Rollback automático si SaveChanges no se llamó o falló
                    throw;
                }
            }
        }

        public DateTime GetMaximaFechaHorario(Guid idCancha)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.CanchaHorarioRepository.GetMaximaFechaHorario(idCancha);
            }
        }

        public bool ExisteHorario(Guid idCancha, DateTime fechaHora)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.CanchaHorarioRepository.ExisteHorario(idCancha, fechaHora);
            }
        }

        //dentro de un rango busco horarios en base, y les hidrato el cliente y la cancha
        public IEnumerable<CanchaHorario> GetHorariosRango(Guid idCancha, DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    var canchaCompleta = context.Repositories.CanchaRepository.GetById(idCancha);
                    if (canchaCompleta == null)
                    {
                        throw new KeyNotFoundException("Cancha no encontrada.");
                    }


                    var horarios = context.Repositories.CanchaHorarioRepository.GetHorariosRango(idCancha, fechaDesde, fechaHasta).ToList();

                    //agarro los id q necesito
                    var idsClientes = horarios
                        .Where(h => h.ReservadaPor != null)
                        .Select(h => h.ReservadaPor.IdCliente)
                        .Distinct()
                        .ToList();

                    var clientesCompletos = new Dictionary<Guid, Cliente>();
                    if (idsClientes.Any())
                    {
                        clientesCompletos = context.Repositories.ClienteRepository.GetAll()
                            .Where(c => idsClientes.Contains(c.IdCliente))
                            .ToDictionary(c => c.IdCliente, c => c);
                    }

                    //Hidrato horarios
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

        public void ActualizarReserva(Guid idCanchaHorario, EstadoReserva nuevoEstado, Cliente cliente, bool abonada)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    var horarioActual = context.Repositories.CanchaHorarioRepository.GetById(idCanchaHorario);
                    if (horarioActual == null)
                    {
                        throw new KeyNotFoundException("El horario a modificar no existe.");
                    }

                    string estadoAnteriorStr = horarioActual.Estado.ToString();
                    string detalleHistorial = $"Cambio de estado: {estadoAnteriorStr} -> {nuevoEstado}.";

                    //validaciones
                    if (nuevoEstado == EstadoReserva.Reservada && cliente == null)
                    {
                        throw new InvalidOperationException("No se puede pasar a 'Reservada' sin asignar un cliente.");
                    }
                    if (nuevoEstado != EstadoReserva.Reservada && abonada)
                    {
                        throw new InvalidOperationException("Un turno solo puede estar abonado si está 'Reservado'.");
                    }
                    if (nuevoEstado == EstadoReserva.Libre)
                    {
                        cliente = null;
                        abonada = false;
                    }

                    horarioActual.Estado = nuevoEstado;
                    horarioActual.Abonada = abonada;


                    horarioActual.ReservadaPor = (cliente != null)
                        ? new Cliente { IdCliente = cliente.IdCliente }
                        : null;


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


                    if (horarioActual.Abonada != abonada && abonada == true)
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

                    context.Repositories.CanchaHorarioRepository.Update(horarioActual);
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
