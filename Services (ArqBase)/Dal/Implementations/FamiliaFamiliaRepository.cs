﻿using Dal.Tools;
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
    internal class FamiliaFamiliaRepository : IJoinRepository<Familia, Familia>
    {
        public void Add(Familia obj, Familia obj2)
        {
            {
                string commandText = "INSERT INTO FamiliaFamilia (IdFamiliaPadre, IdFamiliaHijo) VALUES (@IdFamiliaPadre, @IdFamiliaHijo)";
                SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                    new SqlParameter("@IdFamiliaPadre", obj.Id),
                    new SqlParameter("@IdFamiliaHijo", obj2.Id));
            }
        }

        public List<Familia> GetByObject(Familia obj)
        {
            List<Familia> familias = new List<Familia>();

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader("SELECT IdFamiliaHijo FROM FamiliaFamilia WHERE IdFamiliaPadre = @IdFamiliaPadre",
                CommandType.Text,
                new SqlParameter("@IdFamiliaPadre", obj.Id)))
            {
                while (dataReader.Read())
                {
                    Guid idFamilia = dataReader.GetGuid(0);

                    familias.Add(new FamiliaRepository().GetById(idFamilia));
                }
            }

            return familias;
        }
    }
}
