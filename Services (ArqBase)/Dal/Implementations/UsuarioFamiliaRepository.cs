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
    internal class UsuarioFamiliaRepository : IJoinRepository<Usuario, Familia>
    {
        /// <summary>
        /// Asigna una familia (grupo de roles) específica a un usuario en la base de datos (tabla UsuarioFamilia).
        /// </summary>
        /// <param name="obj">El usuario al cual se le asignará la familia.</param>
        /// <param name="obj2">La familia que será asignada.</param>
        public void Add(Usuario obj, Familia obj2)
        {
            {
                string commandText = "INSERT INTO UsuarioFamilia (IdUsuario, IdFamilia) VALUES (@IdUsuario, @IdFamilia)";
                SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                    new SqlParameter("@IdUsuario", obj.IdUsuario),
                    new SqlParameter("@IdFamilia", obj2.Id));
            }
        }

        /// <summary>
        /// Obtiene la lista de todas las familias (grupos de roles) asociadas a un usuario específico.
        /// </summary>
        /// <param name="obj">El usuario cuyas familias se desean recuperar.</param>
        /// <returns>Una lista de <see cref="Familia"/> asignadas al usuario, incluyendo su estado de habilitación.</returns>
        public List<Familia> GetByObject(Usuario obj)
        {
            List<Familia> familias = new List<Familia>();

            using(SqlDataReader dataReader = SqlHelper.ExecuteReader("SELECT IdFamilia, Habilitado FROM UsuarioFamilia WHERE IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario)))
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
