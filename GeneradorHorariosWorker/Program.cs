using BLL.Facade; // Para BLLFacade
using DAL.Factory;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration; // Para leer config
using System.Linq;

namespace GeneradorHorariosWorker
{
    class Program
    {
        
        private const int DIAS_HORIZONTE = 14;

        static void Main(string[] args)
        {
            Console.WriteLine($"[{DateTime.Now:G}] Iniciando generación de horarios...");
            int canchasProcesadas = 0;
            int horariosGenerados = 0;

            try
            {
                // 1. Obtener todas las canchas ACTIVAS (usa GetAll que ya filtra)
                var canchasActivas = BLLFacade.Current.CanchaService.GetAll();

                foreach (var cancha in canchasActivas)
                {
                    Console.WriteLine($"Procesando cancha: {cancha.Nombre} ({cancha.IdCancha})");
                    try
                    {
                        // 2. Obtener la plantilla de disponibilidad semanal
                        var disponibilidadSemanal = BLLFacade.Current.CanchaService.GetDisponibilidadSemanal(cancha.IdCancha);

                        if (disponibilidadSemanal == null || !disponibilidadSemanal.Any())
                        {
                            Console.WriteLine("  -> Advertencia: Cancha sin disponibilidad semanal definida. Saltando.");
                            continue; // Pasar a la siguiente cancha
                        }

                        // 3. Determinar desde qué fecha generar
                        DateTime fechaMaximaExistente = DateTime.MinValue;
                        // Necesitamos un método en BLL/DAL para obtener la fecha máxima
                        fechaMaximaExistente = BLLFacade.Current.CanchaHorarioService.GetMaximaFechaHorario(cancha.IdCancha); 

                        // Si nunca se generaron, empezamos desde mañana
                        DateTime fechaInicioGeneracion = (fechaMaximaExistente == DateTime.MinValue)
                            ? DateTime.Today.AddDays(1)
                            : fechaMaximaExistente.Date.AddDays(1); 

                        // 4. Determinar hasta qué fecha generar
                        DateTime fechaFinGeneracion = DateTime.Today.AddDays(DIAS_HORIZONTE);

                        Console.WriteLine($"  -> Generando desde {fechaInicioGeneracion:d} hasta {fechaFinGeneracion:d}");

                        // 5. Bucle de generación
                        int horariosParaEstaCancha = 0;
                        for (DateTime diaActual = fechaInicioGeneracion; diaActual <= fechaFinGeneracion; diaActual = diaActual.AddDays(1))
                        {
                            // Si este día tiene disponibilidad definida en la plantilla
                            if (disponibilidadSemanal.TryGetValue(diaActual.DayOfWeek, out var franja))
                            {
                                int startHour = franja.start.Hours;
                                int endHour = franja.end.Hours;
                                for (int hour = startHour; hour < endHour; hour++)
                                {
                                    var slotTime = diaActual.Date.AddHours(hour);
                                    bool yaExiste = BLLFacade.Current.CanchaHorarioService.ExisteHorario(cancha.IdCancha, slotTime); // <-- NECESITAS CREAR ESTO

                                    if (!yaExiste)
                                    {
                                        var newSlot = new CanchaHorario
                                        {
                                            IdCanchaHorario = Guid.NewGuid(),
                                            Cancha = cancha, // ¡OJO! Pasar solo el ID podría ser mejor
                                            // Cancha = new Cancha { IdCancha = cancha.IdCancha }, // Más seguro
                                            FechaHorario = slotTime,
                                            Estado = EstadoReserva.Libre,
                                            Abonada = false,
                                            FueCambiada = false,
                                            ReservadaPor = null
                                        };

                                        BLLFacade.Current.CanchaHorarioService.Crear(newSlot);
                                    }
                                } // Fin bucle horas
                            } // Fin if TryGetValue
                        } // Fin bucle días
                        Console.WriteLine($"  -> Generados {horariosParaEstaCancha} nuevos horarios.");
                        canchasProcesadas++;
                    }
                    catch (Exception exCancha)
                    {
                        Console.WriteLine($"  -> ERROR procesando cancha {cancha.Nombre}: {exCancha.Message}");
                        // Continuar con la siguiente cancha
                    }
                } // Fin foreach canchas
            }
            catch (Exception exGeneral)
            {
                Console.WriteLine($"ERROR FATAL en la generación: {exGeneral.ToString()}");
                // Podrías enviar un email o loggear a un archivo aquí
            }
            finally
            {
                Console.WriteLine($"[{DateTime.Now:G}] Proceso finalizado. Canchas procesadas: {canchasProcesadas}. Horarios generados: {horariosGenerados}.");
                // Opcional: Console.ReadKey(); para que no se cierre si lo ejecutas manualmente
            }
        }
    }
}
