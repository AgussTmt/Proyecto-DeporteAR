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
    internal class FixtureService : IFixtureService
    {
        public void Add(Fixture entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.FixtureRepository.Add(entity);
                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }

        public void CargarResul(Fixture fixture)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    var fixtureDb = context.Repositories.FixtureRepository.GetById(fixture.IdFixture);
                    if (fixtureDb == null)
                        throw new KeyNotFoundException("El partido no existe.");
                    if (fixtureDb.Estado == EstadoFixture.Finalizado)
                        throw new InvalidOperationException("Este partido ya fue finalizado.");

                    
                    var (golesLocal, golesVisitante) = ParseResultado(fixture.Resultado);

                    //Obtener los equipos
                    var equipoLocal = fixtureDb.Equipos.ElementAt(0);
                    var equipoVisitante = fixtureDb.Equipos.ElementAt(1);

                    //Obtener las filas de clasificación de ambos equipos
                    var compStub = new Competicion { IdCompeticion = fixtureDb.IdCompeticion };
                    var clasifLocal = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(compStub, equipoLocal);
                    var clasifVisitante = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(compStub, equipoVisitante);

                    if (clasifLocal == null || clasifVisitante == null)
                        throw new InvalidOperationException("No se encontraron las filas de clasificación para los equipos de este partido.");

                    //Actualizar estadísticas comunes
                    clasifLocal.PartidosJugados += 1;
                    clasifVisitante.PartidosJugados += 1;
                    clasifLocal.GolesAFavor += golesLocal;
                    clasifVisitante.GolesAFavor += golesVisitante;

                    //Asignar puntos y V/E/D
                    if (golesLocal > golesVisitante) // Gana Local
                    {
                        clasifLocal.Victorias += 1;
                        clasifLocal.Puntos += 3;
                        clasifVisitante.Derrotas += 1;
                    }
                    else if (golesVisitante > golesLocal) // Gana Visitante
                    {
                        clasifVisitante.Victorias += 1;
                        clasifVisitante.Puntos += 3;
                        clasifLocal.Derrotas += 1;
                    }
                    else // Empate
                    {
                        clasifLocal.Empates += 1;
                        clasifLocal.Puntos += 1;
                        clasifVisitante.Empates += 1;
                        clasifVisitante.Puntos += 1;
                    }

                    //Actualizar el estado del partido
                    fixtureDb.Resultado = fixture.Resultado;
                    fixtureDb.Estado = EstadoFixture.Finalizado;

                    
                    //Guardar las 3 entidades modificadas (2 clasif + 1 fixture)
                    context.Repositories.ClasificacionRepository.Update(clasifLocal);
                    context.Repositories.ClasificacionRepository.Update(clasifVisitante);
                    context.Repositories.FixtureRepository.Update(fixtureDb);

                    
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw; // Rollback automático
                }
            }
        }

        public void Delete(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.FixtureRepository.Delete(id);
                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }

        public IEnumerable<Fixture> GetAll()
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.FixtureRepository.GetAll();
            }
        }

        public Fixture GetById(Guid id)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.FixtureRepository.GetById(id);
            }
        }

        public List<Fixture> ListarPorRangoTiempo(DateTime dateTime)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.FixtureRepository.GetByTimeRange(dateTime);
            }
        }

        public void postergar(Fixture fixture)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    
                    //Obtener la entidad
                    var fixtureDb = context.Repositories.FixtureRepository.GetById(fixture.IdFixture);
                    if (fixtureDb == null)
                        throw new KeyNotFoundException("El partido no existe.");

                    //Validar que no esté finalizado
                    if (fixtureDb.Estado == EstadoFixture.Finalizado)
                        throw new InvalidOperationException("No se puede postergar un partido que ya finalizó.");

                    //Aplicar cambios
                    fixtureDb.Horario = fixture.Horario; // Asigna la nueva fecha/hora
                    fixtureDb.Estado = EstadoFixture.Postergado;

                    //Guardar
                    context.Repositories.FixtureRepository.Update(fixtureDb);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw; 
                }
            }
        }

        public void Update(Fixture entity)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.FixtureRepository.Update(entity);
                    context.SaveChanges();
                }
                catch (Exception) { throw; }
            }
        }

        private (int golesLocal, int golesVisitante) ParseResultado(string resultado)
        {
            try
            {
                var parts = resultado.Split('-');
                if (parts.Length != 2)
                    throw new FormatException("El resultado debe tener el formato 'GolesLocal-GolesVisitante'.");

                return (int.Parse(parts[0]), int.Parse(parts[1]));
            }
            catch (Exception ex)
            {
                throw new FormatException($"Error al parsear el resultado '{resultado}'. Asegúrese de que tenga el formato 'X-Y'.", ex);
            }
        }
    }
}
