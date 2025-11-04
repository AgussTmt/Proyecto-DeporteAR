using Dal.Tools;
using Services.Dal.Interfaces;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Repositorio para manejar la relación (tabla de unión) entre Familias (grupos) y Patentes (permisos).
    /// </summary>
    internal class FamiliaPatenteRepository : IJoinRepository<Familia, Patente>
    {

        /// <summary>
        /// Asigna una patente específica a una familia (grupo de roles) en la base de datos (tabla FamiliaPatente).
        /// </summary>
        /// <param name="obj">La familia a la cual se le asignará la patente.</param>
        /// <param name="obj2">La patente que será asignada a la familia.</param>
        public void Add(Familia obj, Patente obj2)
        {
            {
                string commandText = "INSERT INTO FamiliaPatente (IdPatente, IdFamilia) VALUES (@IdPatente, @IdFamilia)";
                SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                    new SqlParameter("@IdPatente", obj2.Id),
                    new SqlParameter("@IdFamilia", obj.Id));
            }
        }

        /// <summary>
        /// Obtiene la lista de todas las patentes (permisos) que componen una familia (grupo) específica.
        /// </summary>
        /// <param name="obj">La familia cuyas patentes se desean recuperar.</param>
        /// <returns>Una lista de <see cref="Patente"/> asignadas a la familia, incluyendo su estado de habilitación.</returns>
        public List<Patente> GetByObject(Familia obj)
        {
            List<Patente> patentes = new List<Patente>();

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader("SELECT IdPatente, Habilitado FROM FamiliaPatente WHERE IdFamilia = @IdFamilia",
                CommandType.Text,
                new SqlParameter("@IdFamilia", obj.Id)))
            {
                while (dataReader.Read())
                {
                    Guid idPatente = dataReader.GetGuid(0);
                    bool habilitado = dataReader.GetBoolean(1);

                    Patente patente = new PatenteRepository().GetById(idPatente);
                    patente.Habilitado = habilitado;

                    patentes.Add(patente);
                }
            }

            return patentes;
        }
    }
}
