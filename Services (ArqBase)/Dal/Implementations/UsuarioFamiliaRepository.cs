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
        public void Add(Usuario obj, Familia obj2)
        {
            {
                string commandText = "INSERT INTO UsuarioFamilia (IdUsuario, IdFamilia) VALUES (@IdUsuario, @IdFamilia)";
                SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                    new SqlParameter("@IdUsuario", obj.IdUsuario),
                    new SqlParameter("@IdFamilia", obj2.Id));
            }
        }

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
