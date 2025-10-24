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

                    // 2. Validamos la regla de negocio
                    if (horarioActual.Estado != EstadoReserva.Libre)
                        throw new InvalidOperationException("Solo se puede asignar un cliente a un horario que esté 'Libre'.");

                    // 3. Llamamos al método optimizado de la DAL
                    // (Este método ya pone el estado 'Reservada' y suma 1 a 'CantReservas')
                    context.Repositories.CanchaHorarioRepository.AssignCliente(cliente, horarioActual);

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

                    
                    horarioActual.Estado = estado;

                    
                    if (estado == EstadoReserva.Cancelada)
                        horarioActual.ReservadaPor = null;

                    
                    context.Repositories.CanchaHorarioRepository.Update(horarioActual);

                    
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

        //public bool VerificarDisponible(CanchaHorario canchaHorario)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool VerificarTiempoRestante(CanchaHorario canchaHorario)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
