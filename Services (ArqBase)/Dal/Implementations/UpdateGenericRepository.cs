using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dal.Tools;
using Services.Dal.Interfaces;

namespace Services__ArqBase_.Dal.Implementations
{
    public class UpdateGenericRepository
    {
        public void UpdateHabilitadoJoin(object Ent1, object Ent2)
        {
            Type typeEnt1 = Ent1.GetType();
            Type typeEnt2 = Ent2.GetType();

            object id1 = typeEnt1.Name == "Usuario" ?
            typeEnt1.GetProperty("IdUsuario").GetValue(Ent1) :
            typeEnt1.GetProperty("Id").GetValue(Ent1);

            object id2 = typeEnt2.GetProperty("Id").GetValue(Ent2);


            string Statement = $"UPDATE {typeEnt1.Name}{typeEnt2.Name} SET Habilitado = 1 - Habilitado WHERE Id{typeEnt1.Name} = @Id1 AND Id{typeEnt2.Name} = @Id2";


            SqlHelper.ExecuteNonQuery(Statement, CommandType.Text, new SqlParameter("@Id1", id1),
            new SqlParameter("@Id2", id2));

        }
    }
}
