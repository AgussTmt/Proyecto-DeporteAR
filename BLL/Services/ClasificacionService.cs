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
    /// <summary>
    /// Capa de Lógica de Negocio (BLL) para gestionar la
    /// tabla de posiciones (<see cref="Clasificacion"/>).
    /// </summary>
    /// <remarks>
    /// Este servicio usa el Unit of Work para asegurar que las
    /// operaciones (como crear o actualizar la tabla) se hagan
    /// de forma transaccional.
    /// </remarks>
    internal class ClasificacionService : IClasificacionService
    {
        /// <summary>
        /// Actualiza un registro existente en la tabla de posiciones
        /// (ej: después de que se carga el resultado de un partido).
        /// </summary>
        /// <param name="clasificacion">La entidad <see cref="Clasificacion"/> con los nuevos totales.</param>
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

        /// <summary>
        /// Crea un nuevo registro en la tabla de posiciones (ej: al inscribir un equipo).
        /// </summary>
        /// <param name="clasificacion">La entidad <see cref="Clasificacion"/> (normalmente con todo en 0).</param>
        /// <exception cref="InvalidOperationException">Tira una excepción si el equipo ya está en la tabla.</exception>
        /// <remarks>
        /// Esta es una validación de negocio clave. Antes de meter el equipo,
        /// se fija si ya existe uno con el mismo nombre en esa competición
        /// para evitar duplicados.
        /// </remarks>
        public void Crear(Clasificacion clasificacion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                try
                {
                    // Creamos "stubs" (objetos vacíos solo con el ID/Nombre)
                    // para no tener que pasar la entidad completa.
                    var compStub = new Competicion { IdCompeticion = clasificacion.IdCompeticion };
                    var equipoStub = new Equipo { Nombre = clasificacion.Equipo };

                    // Chequeo de negocio: ¿ya existe?
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

        /// <summary>
        /// Obtiene las estadísticas (la fila de clasificación) de un equipo
        /// específico en una competición específica.
        /// </summary>
        /// <param name="competicion">La <see cref="Competicion"/>.</param>
        /// <param name="equipo">El <see cref="Equipo"/>.</param>
        /// <returns>La <see cref="Clasificacion"/> (estadísticas) de ese equipo.</returns>
        public Clasificacion ObtenerClasificacion(Competicion competicion, Equipo equipo)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {

                return context.Repositories.ClasificacionRepository.GetByCompeticionEquipo(competicion, equipo);
            }
        }

        /// <summary>
        /// Obtiene la tabla de posiciones completa de una competición.
        /// </summary>
        /// <param name="idCompeticion">El ID de la competición.</param>
        /// <returns>Una lista de <see cref="Clasificacion"/> (la tabla de posiciones).</returns>
        /// <remarks>
        /// El repositorio ya devuelve esto ordenado por Puntos y Goles,
        /// así que la BLL no necesita hacer nada más, solo pasarla.
        /// </remarks>
        public List<Clasificacion> ListarPorCompeticion(Guid idCompeticion)
        {
            using (var context = FactoryDao.UnitOfWork.Create())
            {
                return context.Repositories.ClasificacionRepository.GetByCompeticion(idCompeticion);
            }
        }
    }
}