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


            
            string Statement = $"UPDATE {typeEnt1.Name}{typeEnt2.Name} SET Habilitado = 1 - Habilitado";

            SqlHelper.ExecuteNonQuery(Statement, CommandType.Text, new SqlParameter[0]);

        }
    }
}
