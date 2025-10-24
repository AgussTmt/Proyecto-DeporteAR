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
    internal class CanchaService : ICanchaService
    {
        public void Add(Cancha entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.CanchaRepository.Add(entity);

                    var (startHour, endHour) = ParseFranjaHoraria(entity.FranjaHoraria);
                    var businessDays = GetNextBusinessDays(DateTime.Today, 5);

                    
                    foreach (var day in businessDays)
                    {
                        
                        for (int hour = startHour; hour < endHour; hour++)
                        {
                            var slotTime = day.Date.AddHours(hour);

                            var newSlot = new CanchaHorario
                            {
                                IdCanchaHorario = Guid.NewGuid(),
                                
                                Cancha = entity,
                                FechaHorario = slotTime,
                                // Estado inicial 'Libre'
                                Estado = EstadoReserva.Libre,
                                Abonada = false,
                                FueCambiada = false,
                                ReservadaPor = null
                            };

                            
                            context.Repositories.CanchaHorarioRepository.Add(newSlot);
                        }

                        
                        
                    }
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
                   
                    context.Repositories.CanchaRepository.CambiarHabilitado(id);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public IEnumerable<Cancha> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaRepository.GetAll();
            }
        }

        public Cancha GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CanchaRepository.GetById(id);
            }
        }

        public void Update(Cancha entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.CanchaRepository.Update(entity);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private List<DateTime> GetNextBusinessDays(DateTime startDate, int count)
        {
            var days = new List<DateTime>();
            var currentDate = startDate.AddDays(1); // Empezamos desde mañana

            while (days.Count < count)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday &&
                    currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    days.Add(currentDate.Date); // Agregamos solo la fecha
                }
                currentDate = currentDate.AddDays(1);
            }
            return days;
        }

        private (int start, int end) ParseFranjaHoraria(string franjaHoraria)
        {
            try
            {
                var parts = franjaHoraria.Split('-');
                int start = int.Parse(parts[0].Split(':')[0]);
                int end = int.Parse(parts[1].Split(':')[0]);
                return (start, end);
            }
            catch (Exception ex)
            {
                // Lanza un error de negocio claro
                throw new InvalidOperationException($"La Franja Horaria '{franjaHoraria}' no tiene un formato válido (se esperaba 'HH:mm-HH:mm').", ex);
            }
        }
    }
}
