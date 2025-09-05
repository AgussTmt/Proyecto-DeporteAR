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
    internal class UsuarioPatenteRepository : IJoinRepository<Usuario, Patente>
    {
        public void Add(Usuario obj, Patente obj2)
        {
            string commandText = "INSERT INTO UsuarioPatente (IdUsuario, IdPatente) VALUES (@IdUsuario, @IdPatente)";
            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario),
                new SqlParameter("@IdPatente", obj2.Id));
        }

        public List<Patente> GetByObject(Usuario obj)
        {
            List<Patente> patentes = new List<Patente>();

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader("SELECT IdPatente, Habilitado FROM UsuarioPatente WHERE IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario)))
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
