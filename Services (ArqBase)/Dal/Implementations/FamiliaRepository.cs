using Dal.Tools;
using Services.Dal.Implementations.Adapters;
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
    internal class FamiliaRepository : IFamiliaRepository
    {
        #region Statements
        private string SelectAllStatement
        {
            get => "SELECT IdFamilia, Nombre, Habilitado, VerificadorHash FROM [dbo].[Familia]";
        }

        private string SelectByIdStatement
        {
            get => "SELECT IdFamilia, Nombre, Habilitado, VerificadorHash FROM [dbo].[Familia] WHERE IdFamilia = @IdFamilia";
        }

        private string AddStatement
        {
            get => "INSERT INTO Familia (IdFamilia, Nombre, Habilitado, VerificadorHash) VALUES (@IdFamilia, @Nombre, @Habilitado, @VerificadorHash)";
        }




        #endregion


        public Familia Add(Familia familia)
        {
            SqlHelper.ExecuteNonQuery(AddStatement, CommandType.Text, new SqlParameter("@IdFamilia", familia.Id),
                new SqlParameter("@Nombre", familia.Nombre),
                new SqlParameter("@Habilitado", familia.Habilitado),
                new SqlParameter("@VerificadorHash", familia.VerificadorHash)
                );

            return familia;

        }


        public List<Familia> GetAll()
        {
            List<Familia> ListFamilias = new List<Familia>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectAllStatement,
                                                                    CommandType.Text,
                                                                    new SqlParameter[] { }))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    Familia patente = FamiliaAdapter.Current.Get(data);
                    ListFamilias.Add(patente);
                }
            }

            return ListFamilias;
        }

        public Familia GetById(Guid id)
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectByIdStatement,
                                                     CommandType.Text,
                                                     new SqlParameter[] { new SqlParameter("@IdFamilia", id) }))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return FamiliaAdapter.Current.Get(data);
                }
                else
                {
                    return null; // or throw an exception if not found
                }
            }
        }
    }
}
