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
    internal class FamiliaPatenteRepository : IJoinRepository<Familia, Patente>
    {
        public void Add(Familia obj, Patente obj2)
        {
            {
                string commandText = "INSERT INTO FamiliaPatente (IdPatente, IdFamilia) VALUES (@IdPatente, @IdFamilia)";
                SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                    new SqlParameter("@IdPatente", obj2.Id),
                    new SqlParameter("@IdFamilia", obj.Id));
            }
        }

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
