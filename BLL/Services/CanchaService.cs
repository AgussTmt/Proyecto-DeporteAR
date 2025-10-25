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
        public void Add(Cancha entity, Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)> disponibilidad)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    context.Repositories.CanchaRepository.Add(entity);

                    
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

                    
                    DateTime proximaSemana = DateTime.Today.AddDays(7);
                    for (DateTime diaActual = DateTime.Today.AddDays(1); diaActual < proximaSemana; diaActual = diaActual.AddDays(1))
                    {
                        if (disponibilidad.TryGetValue(diaActual.DayOfWeek, out var franja))
                        {
                            int startHour = franja.start.Hours;
                            int endHour = franja.end.Hours;
                            for (int hour = startHour; hour < endHour; hour++)
                            {
                               
                                var slotTime = diaActual.Date.AddHours(hour);
                                var newSlot = new CanchaHorario
                                {
                                    IdCanchaHorario = Guid.NewGuid(), 
                                    Cancha = entity,                
                                    FechaHorario = slotTime,          
                                    Estado = EstadoReserva.Libre,   
                                    Abonada = false,                
                                    FueCambiada = false,              
                                    ReservadaPor = null              
                                };
                                context.Repositories.CanchaHorarioRepository.Add(newSlot);
                            }
                        }
                    }

                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }


        public void CambiarHabilitado(Guid id)
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

        public void Update(Cancha entity, Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)> disponibilidad)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    context.Repositories.CanchaRepository.Update(entity);

                    context.Repositories.CanchaDisponibilidadRepository.DeleteByCancha(entity.IdCancha);

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

        public IEnumerable<Cancha> GetAllIncludingDisabled()
        {
            using (var context = FactoryDao.UnitOfWork.Create()) 
            { 
                return context.Repositories.CanchaRepository.GetAllIncludingDisabled(); 
            }
        }

        public bool EsHorarioValido(Guid idCancha, DateTime fechaHora)
        {
            try
            {
                
                var disponibilidad = GetDisponibilidadSemanal(idCancha);

                // Verificamos si hay configuración para ese día de la semana
                if (disponibilidad.TryGetValue(fechaHora.DayOfWeek, out var franja))
                {
                    // Verificamos si la hora del día está dentro del rango [Inicio, Fin)
                    TimeSpan horaDelDia = fechaHora.TimeOfDay;
                    return horaDelDia >= franja.start && horaDelDia < franja.end;
                }
                else
                {
                    // No hay disponibilidad definida para este día de la semana
                    return false;
                }
            }
            catch
            {
                // Si hay error al cargar disponibilidad, asumimos que no es válido
                return false;
            }
        }
    }
}
