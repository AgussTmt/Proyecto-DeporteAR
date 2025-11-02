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
    internal class ClasificacionService : IClasificacionService
    {

        public void Actualizar(Clasificacion clasificacion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    context.Repositories.ClasificacionRepository.Update(clasificacion);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                   
                    throw;
                }
            }
        }

        public void Crear(Clasificacion clasificacion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    var compStub = new Competicion { IdCompeticion = clasificacion.IdCompeticion };
                    var equipoStub = new Equipo { Nombre = clasificacion.Equipo };

                    var existente = context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(compStub, equipoStub);

                    if (existente != null)
                    {
                        
                        throw new InvalidOperationException($"El equipo '{clasificacion.Equipo}' ya está en la tabla de clasificación de esta competición.");
                    }

                    
                    context.Repositories.ClasificacionRepository.Add(clasificacion);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public Clasificacion ObtenerClasificacion(Competicion competicion, Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                
                return context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(competicion, equipo);
            }
        }

        public List<Clasificacion> ListarPorCompeticion(Guid idCompeticion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.ClasificacionRepository.GetByCompeticion(idCompeticion);
            }
        }
    }
}
