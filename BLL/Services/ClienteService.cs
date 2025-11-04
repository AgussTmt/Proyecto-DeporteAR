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
    /// <summary>
    /// Capa de Lógica de Negocio (BLL) para el ABM (CRUD) de Clientes.
    /// </summary>
    /// <remarks>
    /// Acá está la lógica pesada. Se asegura de que no puedas borrar a un cliente
    /// si es capitán o si tiene reservas pendientes. También tiene la lógica
    /// para armar el ranking de los que más reservan.
    /// </remarks>
    internal class ClienteService : IClienteService
    {
        /// <summary>
        /// Agrega un nuevo Cliente a la base de datos.
        /// </summary>
        /// <param name="entity">El <see cref="Cliente"/> a crear.</param>
        /// <exception cref="InvalidOperationException">Si ya existe un cliente con ese teléfono.</exception>
        /// <remarks>
        /// Antes de mandarlo a guardar, este método hace dos cosas:
        /// 1. Se fija si ya existe otro cliente con ese mismo teléfono.
        /// 2. Le asigna un Guid nuevo si no viene uno. Bien ahí, la BLL
        /// se encarga de crear el ID.
        /// </remarks>
        public void Add(Cliente entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // Validación de BLL: No duplicar teléfono
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

        /// <summary>
        /// Intenta borrar un Cliente de la base de datos, con validaciones de negocio.
        /// </summary>
        /// <param name="id">El ID del cliente a borrar.</param>
        /// <exception cref="InvalidOperationException">Si el cliente es capitán de un equipo o tiene reservas activas.</exception>
        /// <remarks>
        /// ¡Esta es la lógica de "borrado seguro"! Antes de apretar el gatillo, se fija en dos cosas:
        /// 1. Que el cliente no sea capitán de un equipo.
        /// 2. Que el cliente no tenga reservas activas a futuro.
        /// Si alguna pasa, te frena el carro con una excepción. Impecable.
        /// </remarks>
        public void Delete(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // Check 1: ¿es capitan de algun equipo?
                    var equiposDelCliente = context.Repositories.EquipoRepository.GetByCapitan(id);

                    if (equiposDelCliente.Any())
                    {
                        string nombresEquipos = string.Join(", ", equiposDelCliente.Select(e => e.Nombre));
                        throw new InvalidOperationException($"No se puede borrar este cliente porque es capitán de lo(s) siguiente(s) equipo(s): {nombresEquipos}. Primero debe cambiar el capitán de esos equipos.");
                    }

                    // Check 2: ¿tiene reservas activas?
                    int reservasActivas = context.Repositories.CanchaHorarioRepository.CountReservasActivasByCliente(id);
                    if (reservasActivas > 0)
                    {
                        throw new InvalidOperationException($"No se puede borrar. Este cliente tiene {reservasActivas} reserva(s) a futuro.");
                    }

                    // Si pasó los checks, borra
                    context.Repositories.ClienteRepository.Delete(id);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de todos los clientes.
        /// </summary>
        /// <returns>Colección de <see cref="Cliente"/>.</returns>
        /// <remarks>
        /// Este es un simple "pass-through". Llama al repositorio
        /// y devuelve lo que le da, sin lógica extra.
        /// </remarks>
        public IEnumerable<Cliente> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.ClienteRepository.GetAll();
            }
        }

        /// <summary>
        /// Obtiene un cliente específico por su ID.
        /// </summary>
        /// <param name="id">El ID del cliente.</param>
        /// <returns>El <see cref="Cliente"/>.</returns>
        public Cliente GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.ClienteRepository.GetById(id);
            }
        }

        /// <summary>
        /// Actualiza un Cliente existente.
        /// </summary>
        /// <param name="entity">El <see cref="Cliente"/> con los datos modificados.</param>
        /// <exception cref="InvalidOperationException">Si el teléfono ya está en uso por OTRO cliente.</exception>
        /// <remarks>
        /// Lógica de negocio clave: al igual que el 'Add', chequea que el teléfono
        /// no esté duplicado, PERO se aviva de que no sea el teléfono del
        /// *propio cliente* que estás editando. Justo lo que tiene que hacer.
        /// </remarks>
        public void Update(Cliente entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // Validación de BLL: Chequeo de teléfono duplicado
                    var existente = context.Repositories.ClienteRepository.GetByNumero(entity.Telefono);
                    if (existente != null && existente.IdCliente != entity.IdCliente) // <- La lógica clave
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

        /// <summary>
        /// Obtiene un Top N de clientes, ordenados por cantidad de reservas.
        /// </summary>
        /// <param name="topN">El número de clientes a devolver (ej: 5 para un Top 5).</param>
        /// <returns>Una lista del DTO <see cref="RankingClienteDTO"/>.</returns>
        /// <remarks>
        /// Este es un método de reporte y está bien optimizado.
        /// En lugar de hacer un N+1, se trae todos los clientes (1 consulta) y
        /// todas las reservas (1 consulta), y después arma el ranking en
        /// memoria con LINQ. Cero latencia. Muy bien.
        /// </remarks>
        public List<RankingClienteDTO> GetRankingClientes(int topN)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                // 1. Traemos todos los clientes a un diccionario para lookup rápido. (1 consulta)
                var todosLosClientes = context.Repositories.ClienteRepository.GetAll()
                    .ToDictionary(c => c.IdCliente, c => c);

                // 2. Traemos todos los horarios. (1 consulta)
                var todosLosHorarios = context.Repositories.CanchaHorarioRepository.GetAll();

                // 3. Magia de LINQ (esto corre en memoria, es rápido)
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