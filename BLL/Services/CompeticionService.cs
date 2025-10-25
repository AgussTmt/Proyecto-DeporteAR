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
    internal class CompeticionService : ICompeticionService
    {
        public void Add(Competicion entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    entity.Estado = EstadoCompeticion.SinFixture;
                    context.Repositories.CompeticionRepository.Add(entity);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void AñadirEquipo(Competicion competicion, Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {


                    var comp = context.Repositories.CompeticionRepository.GetById(competicion.IdCompeticion);
                    if (comp == null)
                        throw new KeyNotFoundException("La competición no existe.");

                    //No se puede inscribir si el fixture ya esta creado
                    if (comp.Estado != EstadoCompeticion.SinFixture)
                        throw new InvalidOperationException("Las inscripciones están cerradas, el fixture ya fue creado.");

                    //No se puede inscribir si está lleno
                    if (comp.ListaEquipos.Count >= comp.Cupos)
                        throw new InvalidOperationException("La competición ha alcanzado su cupo máximo de equipos.");

                    var equipoCompleto = context.Repositories.EquipoRepository.GetById(equipo.IdEquipo);
                    if (equipoCompleto == null || !equipoCompleto.Habilitado)
                    {
                        throw new InvalidOperationException($"El equipo '{equipo.Nombre}' no existe o está deshabilitado y no puede ser inscripto.");
                    }

                    //El equipo ya está inscripto
                    if (comp.ListaEquipos.Any(e => e.IdEquipo == equipo.IdEquipo))
                        throw new InvalidOperationException("El equipo ya está inscripto en esta competición.");


                    context.Repositories.CompeticionRepository.AddEquipo(comp.IdCompeticion, equipo.IdEquipo);


                    var clasificacion = new Clasificacion
                    {
                        IdClasificacion = Guid.NewGuid(),
                        IdCompeticion = comp.IdCompeticion,
                        Equipo = equipo.Nombre,
                        Derrotas = 0,
                        Empates = 0,
                        Victorias = 0,
                        GolesAFavor = 0,
                        PartidosJugados = 0,
                        Puntos = 0
                    };
                    context.Repositories.ClasificacionRepository.Add(clasificacion);


                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void CrearFixture(Competicion competicion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    var comp = context.Repositories.CompeticionRepository.GetById(competicion.IdCompeticion);
                    if (comp == null)
                    {
                        throw new KeyNotFoundException("La competición no existe.");
                    }

                    if (comp.Estado != EstadoCompeticion.SinFixture)
                    {
                        throw new InvalidOperationException("El fixture para esta competición ya fue creado anteriormente.");
                    }

                    if (comp.ListaEquipos.Count < comp.CuposMinimos)
                    {
                        throw new InvalidOperationException($"No se puede crear el fixture. Se requieren {comp.CuposMinimos} equipos habilitados y solo hay {comp.ListaEquipos.Count} inscritos y habilitados.");
                    }

                    if (comp.ListaEquipos.Count < 2)
                    {
                        throw new InvalidOperationException("Se necesitan al menos 2 equipos habilitados para generar un fixture.");
                    }

                    if (comp.canchaAsignada == null || comp.canchaAsignada.IdCancha == Guid.Empty)
                    {
                        throw new InvalidOperationException("La competición no tiene una cancha asignada válida.");
                    }

                    if (comp.canchaAsignada.DuracionXPartidoMin == 0)
                    {
                        comp.canchaAsignada = context.Repositories.CanchaRepository.GetById(comp.canchaAsignada.IdCancha);
                    }

                    if (comp.canchaAsignada == null)
                    {
                        throw new InvalidOperationException("La cancha asignada a la competición no fue encontrada.");
                    }


                    //Logica Principal
                    var partidosGenerados = GenerarPartidosRoundRobin(comp, comp.ListaEquipos); 

                    //Bloquear Horarios y Guardar Fixtures
                    foreach (var partido in partidosGenerados)
                    {
                        var horarioParaBloquear = context.Repositories.CanchaHorarioRepository.GetByCanchaYHora(
                            comp.canchaAsignada.IdCancha,
                            partido.Horario
                        );

                        
                        if (horarioParaBloquear == null)
                        {
                            throw new InvalidOperationException($"Error al generar fixture: No se encontró un slot de horario disponible para el partido del {partido.Horario:g} en la cancha '{comp.canchaAsignada.Nombre}'. Verifique la disponibilidad de horarios.");
                        }

                      
                        if (horarioParaBloquear.Estado != EstadoReserva.Libre)
                        {
                            throw new InvalidOperationException($"Conflicto al generar fixture: El horario {partido.Horario:g} para la cancha '{comp.canchaAsignada.Nombre}' ya se encuentra '{horarioParaBloquear.Estado}'.");
                        }

                        // Si todo está OK para este horario, lo bloqueamos y guardamos el fixture
                        horarioParaBloquear.Estado = EstadoReserva.OcupadoPorTorneo; 
                        context.Repositories.CanchaHorarioRepository.Update(horarioParaBloquear);
                        context.Repositories.FixtureRepository.Add(partido);
                    }

                    //Actualizar el estado de la competición
                    comp.Estado = EstadoCompeticion.ConFixture;
                    context.Repositories.CompeticionRepository.Update(comp);

                    
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
            throw new NotImplementedException();
        }

        public IEnumerable<Competicion> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetAll();
            }
        }

        public Competicion GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetById(id);
            }
        }

        public List<Competicion> ListarConVacantes(Competicion competicion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetWithVacancies();
            }
        }

        public List<Competicion> ListarPorCliente(Cliente cliente)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.CompeticionRepository.GetByClient(cliente);
            }
        }

        public void QuitarEquipo(Competicion competicion, Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {

                    var comp = context.Repositories.CompeticionRepository.GetById(competicion.IdCompeticion);
                    if (comp == null)
                        throw new KeyNotFoundException("La competición no existe.");


                    if (comp.Estado != EstadoCompeticion.SinFixture)
                        throw new InvalidOperationException("No se puede quitar un equipo una vez que el fixture está creado.");


                    context.Repositories.CompeticionRepository.RemoveEquipo(comp.IdCompeticion, equipo.IdEquipo);


                    var clasificacion = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(comp, equipo);
                    if (clasificacion != null)
                    {

                        context.Repositories.ClasificacionRepository.Delete(clasificacion.IdClasificacion);
                    }


                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Update(Competicion entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.CompeticionRepository.Update(entity);
                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }

        private List<Fixture> GenerarPartidosRoundRobin(Competicion comp, List<Equipo> equiposParaFixture)
        {
            var partidos = new List<Fixture>();
            var equipos = new List<Equipo>(equiposParaFixture);

            // Si el número de equipos es impar, se agrega un "equipo fantasma"
            if (equipos.Count % 2 != 0)
            {
                equipos.Add(new Equipo { IdEquipo = Guid.Empty, Nombre = "DESCANSA" });
            }

            int numEquipos = equipos.Count;
            int numRondas = numEquipos - 1;
            int partidosPorRonda = numEquipos / 2;

            DateTime fechaPartido = comp.FechaInicio;
            TimeSpan horaInicioRonda = TimeSpan.Parse(comp.FranjaHoraria.Split('-')[0]); // Hora de inicio de la jornada
                                                                                         // Necesitamos la duración que ya cargamos antes en CrearFixture
            int duracionMinutos = comp.canchaAsignada.DuracionXPartidoMin;

            for (int r = 0; r < numRondas; r++)
            {
                TimeSpan horaPartidoActual = horaInicioRonda; // Resetea la hora para cada nueva fecha

                for (int i = 0; i < partidosPorRonda; i++)
                {
                    Equipo local = equipos[i];
                    Equipo visitante = equipos[numEquipos - 1 - i];

                    // Si ninguno es el equipo "DESCANSA", se crea el partido
                    if (local.IdEquipo != Guid.Empty && visitante.IdEquipo != Guid.Empty)
                    {
                        var partido = new Fixture
                        {
                            IdFixture = Guid.NewGuid(),
                            IdCompeticion = comp.IdCompeticion,
                            Estado = EstadoFixture.Pendiente,
                            Resultado = null,
                            Horario = fechaPartido.Date.Add(horaPartidoActual),
                            Equipos = new List<Equipo> { local, visitante }
                        };
                        partidos.Add(partido);

                        // Avanza la hora para el siguiente partido de esta ronda
                        horaPartidoActual = horaPartidoActual.Add(TimeSpan.FromMinutes(duracionMinutos));
                    }
                }

                // Rotar los equipos (dejando el primero fijo)
                // Guardamos el segundo equipo
                var equipoRotativo = equipos[1];
                // Lo removemos de la segunda posición
                equipos.RemoveAt(1);
                // Lo agregamos al final
                equipos.Add(equipoRotativo);

                // Avanzar la fecha para la siguiente ronda
                fechaPartido = fechaPartido.AddDays(comp.Frecuencia);
            }

            return partidos;
        }
    }
}

