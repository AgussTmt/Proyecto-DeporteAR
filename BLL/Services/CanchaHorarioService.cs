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
                    // --- LÓGICA DE NEGOCIO ---
                    // 1. Nos aseguramos de tener la última versión del horario
                    var horarioActual = context.Repositories.CanchaHorarioRepository.GetById(canchaHorario.IdCanchaHorario);

                    if (horarioActual == null)
                        throw new KeyNotFoundException("El horario seleccionado no existe.");
                    string estadoAnterior = horarioActual.Estado.ToString();
                    // 2. Validamos la regla de negocio
                    if (horarioActual.Estado != EstadoReserva.Libre)
                        throw new InvalidOperationException("Solo se puede asignar un cliente a un horario que esté 'Libre'.");

                    // 3. Llamamos al método optimizado de la DAL
                    // (Este método ya pone el estado 'Reservada' y suma 1 a 'CantReservas')
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

                    // 4. Persistimos la transacción
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw; // El UoW hará rollback en el Dispose()
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
    }
}
