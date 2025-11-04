using Dal.Tools;
using Services.Dal.Implementations.Adapters;
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
    internal class PatenteRepository : IPatenteRepository
    {


        #region Statements
        private string SelectAllStatement
        {
            get => "SELECT IdPatente, DataKey, TipoAcceso, Habilitado FROM [dbo].[Patente]";
        }

        private string SelectByIdStatement
        {
            get => "SELECT IdPatente, DataKey, TipoAcceso, Habilitado FROM [dbo].[Patente] WHERE IdPatente = @IdPatente";
        }
        #endregion

        /// <summary>
        /// Obtiene una lista con todas las patentes (permisos) disponibles en el sistema.
        /// </summary>
        /// <returns>Una <see cref="List{Patente}"/> que contiene todas las patentes.</returns>
        public List<Patente> GetAll()
        {
            List<Patente> ListPatentes = new List<Patente>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectAllStatement,
                                                                    CommandType.Text,
                                                                    new SqlParameter[] { }))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    Patente patente = PatenteAdapter.Current.Get(data);
                    ListPatentes.Add(patente);
                }
            }

            return ListPatentes;
        }

        /// <summary>
        /// Obtiene una patente específica mediante su identificador único.
        /// </summary>
        /// <param name="id">El <see cref="Guid"/> de la patente a buscar.</param>
        /// <returns>El objeto <see cref="Patente"/> si se encuentra; de lo contrario, <c>null</c>.</returns>
        public Patente GetById(Guid id)
        {


            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectByIdStatement,
                                                     CommandType.Text,
                                                     new SqlParameter[] { new SqlParameter("@IdPatente", id) }))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return PatenteAdapter.Current.Get(data);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
