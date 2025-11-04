using Dal.Tools;
using Services.Dal.Implementations.Adapters;
using Services.DomainModel;
using Services.Facade;
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

        /// <summary>
        /// Agrega una nueva familia (grupo de roles) a la base de datos.
        /// </summary>
        /// <param name="familia">El objeto <see cref="Familia"/> a insertar.</param>
        /// <returns>El mismo objeto <see cref="Familia"/> que se pasó como parámetro.</returns>
        public Familia Add(Familia familia)
        {
            SqlHelper.ExecuteNonQuery(AddStatement, CommandType.Text, new SqlParameter("@IdFamilia", familia.Id),
                new SqlParameter("@Nombre", familia.Nombre),
                new SqlParameter("@Habilitado", familia.Habilitado),
                new SqlParameter("@VerificadorHash", familia.VerificadorHash)
                );

            return familia;

        }

        /// <summary>
        /// Obtiene una lista con todas las familias (grupos de roles) disponibles en el sistema.
        /// </summary>
        /// <returns>Una <see cref="List{Familia}"/> que contiene todas las familias.</returns>
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

        /// <summary>
        /// Obtiene una familia (grupo de roles) específica mediante su identificador único.
        /// </summary>
        /// <param name="id">El <see cref="Guid"/> de la familia a buscar.</param>
        /// <returns>El objeto <see cref="Familia"/> si se encuentra; de lo contrario, <c>null</c>.</returns>
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
                    return null;
                }
            }
        }

        /// <summary>
        /// Verifica la integridad de todos los registros en la tabla [Familia] comparando
        /// un hash calculado con el hash almacenado (VerificadorHash).
        /// </summary>
        /// <returns>Una lista de strings que describe cada fila donde se detectó una manipulación o corrupción de datos.</returns>
        public List<string> VerificarIntegridadHash()
        {
            var errores = new List<string>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectAllStatement,
                                                                    CommandType.Text,
                                                                    new SqlParameter[] { }))
            {
                while (reader.Read())
                {
                    string id = reader["IdFamilia"].ToString();
                    string nombre = reader["Nombre"].ToString();
                    bool habilitado = Convert.ToBoolean(reader["Habilitado"]);
                    string hashGuardado = reader["VerificadorHash"] == DBNull.Value ? null : reader["VerificadorHash"].ToString();
                    string datosConcatenados = $"{id}-{nombre}-{habilitado}";
                    string hashCalculado = CryptographyService.HashMd5(datosConcatenados);

                    if (hashGuardado != hashCalculado)
                    {
                        errores.Add($"Error en Familia: Fila ID {id} (Nombre: '{nombre}') ha sido manipulada.");
                    }
                }
            }
            return errores;
        }
    }
}

