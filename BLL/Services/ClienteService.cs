using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Services.Dto;
using DAL.Factory;
using DomainModel;

namespace BLL.Services
{
    internal class ClienteService : IClienteService
    {
        public void Add(Cliente entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    var existente = context.Repositories.ClienteRepository.GetByNumero(entity.Telefono);
                    if (existente != null)
                    {
                        throw new InvalidOperationException($"Ya existe un cliente registrado con el teléfono '{entity.Telefono}'.");
                    }

                    if (entity.IdCliente == Guid.Empty)
                    {
                        entity.IdCliente = Guid.NewGuid();
                    }


                    context.Repositories.ClienteRepository.Add(entity);
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
                    //es capitan de algun equipo?
                    var equiposDelCliente = context.Repositories.EquipoRepository.GetByCapitan(id);

                    if (equiposDelCliente.Any())
                    {
                        string nombresEquipos = string.Join(", ", equiposDelCliente.Select(e => e.Nombre));
                        throw new InvalidOperationException($"No se puede borrar este cliente porque es capitán de lo(s) siguiente(s) equipo(s): {nombresEquipos}. Primero debe cambiar el capitán de esos equipos.");
                    }

                    //tiene reservas activas?
                    int reservasActivas = context.Repositories.CanchaHorarioRepository.CountReservasActivasByCliente(id);
                    if (reservasActivas > 0)
                    {
                        throw new InvalidOperationException($"No se puede borrar. Este cliente tiene {reservasActivas} reserva(s) a futuro.");
                    }


                    context.Repositories.ClienteRepository.Delete(id); 
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public IEnumerable<Cliente> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                
                return context.Repositories.ClienteRepository.GetAll();
            }
        }

        public Cliente GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.ClienteRepository.GetById(id);
            }
        }

        public void Update(Cliente entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    var existente = context.Repositories.ClienteRepository.GetByNumero(entity.Telefono);
                    if (existente != null && existente.IdCliente != entity.IdCliente)
                    {
                        throw new InvalidOperationException($"El teléfono '{entity.Telefono}' ya está asignado a otro cliente.");
                    }

                    context.Repositories.ClienteRepository.Update(entity);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<RankingClienteDTO> GetRankingClientes(int topN)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                // 1. Traemos todos los clientes a un diccionario para lookup rápido.
                var todosLosClientes = context.Repositories.ClienteRepository.GetAll()
                    .ToDictionary(c => c.IdCliente, c => c);

                // 2. Traemos todos los horarios.
                var todosLosHorarios = context.Repositories.CanchaHorarioRepository.GetAll();

                // 3. Magia de LINQ
                var ranking = todosLosHorarios
                    // Solo las reservadas y que tengan cliente
                    .Where(h => h.ReservadaPor != null && h.Estado == EstadoReserva.Reservada)
                    // Agrupamos por ID de cliente
                    .GroupBy(h => h.ReservadaPor.IdCliente)
                    // Proyectamos a un objeto anónimo con el Conteo
                    .Select(g => new
                    {
                        IdCliente = g.Key,
                        CantidadReservas = g.Count()
                    })
                    // Ordenamos
                    .OrderByDescending(x => x.CantidadReservas)
                    // Tomamos el Top N
                    .Take(topN)
                    // Ahora, unimos con el diccionario de clientes para obtener los nombres
                    .Select(r =>
                    {
                        // Buscamos el cliente completo
                        // Usamos TryGetValue por si el cliente fue borrado pero sus reservas quedaron
                        var cliente = todosLosClientes.TryGetValue(r.IdCliente, out var cli)
                            ? cli
                            : new Cliente { Nombre = "(Cliente Eliminado)", Telefono = "N/A" };

                        // Mapeamos al DTO
                        return new RankingClienteDTO
                        {
                            Nombre = cliente.Nombre,
                            Telefono = cliente.Telefono,
                            CantidadReservas = r.CantidadReservas
                        };
                    })
                    .ToList();

                return ranking;
            }
        }
    }
}
