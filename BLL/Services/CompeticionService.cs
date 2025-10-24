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
                        throw new KeyNotFoundException("La competición no existe.");

                    

                    
                    if (comp.ListaEquipos.Count < comp.CuposMinimos)
                        throw new InvalidOperationException($"No se puede crear el fixture. Se requieren {comp.CuposMinimos} equipos y solo hay {comp.ListaEquipos.Count} inscriptos.");

                    
                    if (comp.Estado != EstadoCompeticion.SinFixture)
                        throw new InvalidOperationException("El fixture para esta competición ya fue creado anteriormente.");

                    
                    var partidosGenerados = GenerarPartidosRoundRobin(comp);

                    
                    foreach (var partido in partidosGenerados)
                    {
                        context.Repositories.FixtureRepository.Add(partido);
                    }

                    
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

        private List<Fixture> GenerarPartidosRoundRobin(Competicion comp)
        {
            var partidos = new List<Fixture>();
            var equipos = new List<Equipo>(comp.ListaEquipos);

            // Si el número de equipos es impar, se agrega un "equipo fantasma"
            // El que juega contra él, "descansa".
            if (equipos.Count % 2 != 0)
            {
                equipos.Add(new Equipo { IdEquipo = Guid.Empty, Nombre = "DESCANSA" });
            }

            int numEquipos = equipos.Count;
            int numRondas = numEquipos - 1;
            int partidosPorRonda = numEquipos / 2;

            // Define cuándo empiezan los partidos y con qué frecuencia
            DateTime fechaPartido = comp.FechaInicio;
            TimeSpan horaPartido = TimeSpan.Parse(comp.FranjaHoraria.Split('-')[0]); 

            for (int r = 0; r < numRondas; r++)
            {
                for (int i = 0; i < partidosPorRonda; i++)
                {
                    Equipo local = equipos[i];
                    Equipo visitante = equipos[numEquipos - 1 - i];

                    
                    if (local.IdEquipo != Guid.Empty && visitante.IdEquipo != Guid.Empty)
                    {
                        var partido = new Fixture
                        {
                            IdFixture = Guid.NewGuid(),
                            IdCompeticion = comp.IdCompeticion,
                            Estado = EstadoFixture.Pendiente,
                            Resultado = "0-0",
                            Horario = fechaPartido.Date.Add(horaPartido),
                            Equipos = new List<Equipo> { local, visitante }
                        };
                        partidos.Add(partido);

                        
                        horaPartido = horaPartido.Add(TimeSpan.FromMinutes(comp.canchaAsignada.DuracionXPartidoMin));
                    }
                }

                
                var equipoRotativo = equipos[1];
                equipos.RemoveAt(1);
                equipos.Add(equipoRotativo);

                
                fechaPartido = fechaPartido.AddDays(comp.Frecuencia);
            }

            return partidos;
        }
    }
}

