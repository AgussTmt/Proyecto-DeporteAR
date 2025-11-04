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
    /// Repositorio para manejar la relación jerárquica (Padre-Hijo) entre Familias.
    /// Implementa el patrón Composite a nivel de persistencia.
    /// </summary>
    internal class FamiliaFamiliaRepository : IJoinRepository<Familia, Familia>
    {
        /// <summary>
        /// Crea una relación jerárquica, asignando una familia (Hijo) a otra familia (Padre).
        /// </summary>
        /// <param name="obj">La familia 'Padre' a la que se le asignará el hijo.</param>
        /// <param name="obj2">La familia 'Hijo' que será asignada como descendiente.</param>
        public void Add(Familia obj, Familia obj2)
        {
            {
                string commandText = "INSERT INTO FamiliaFamilia (IdFamiliaPadre, IdFamiliaHijo) VALUES (@IdFamiliaPadre, @IdFamiliaHijo)";
                SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                    new SqlParameter("@IdFamiliaPadre", obj.Id),
                    new SqlParameter("@IdFamiliaHijo", obj2.Id));
            }
        }

        /// <summary>
        /// Obtiene la lista de todas las familias 'Hijo' directas asociadas a una familia 'Padre'.
        /// </summary>
        /// <param name="obj">La familia 'Padre' cuyas familias 'Hijo' se desean recuperar.</param>
        /// <returns>Una lista de <see cref="Familia"/> (hijos) asignadas a la familia padre.</returns>
        public List<Familia> GetByObject(Familia obj)
        {
            List<Familia> familias = new List<Familia>();

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader("SELECT IdFamiliaHijo, Habilitado FROM FamiliaFamilia WHERE IdFamiliaPadre = @IdFamiliaPadre",
                CommandType.Text,
                new SqlParameter("@IdFamiliaPadre", obj.Id)))
            {
                while (dataReader.Read())
                {
                    Guid idFamilia = dataReader.GetGuid(0);
                    bool habilitado = dataReader.GetBoolean(1);

                    Familia familia = new FamiliaRepository().GetById(idFamilia);
                    familia.Habilitado = habilitado;
                    familias.Add(familia);
                }
            }

            return familias;
        }
    }
}
