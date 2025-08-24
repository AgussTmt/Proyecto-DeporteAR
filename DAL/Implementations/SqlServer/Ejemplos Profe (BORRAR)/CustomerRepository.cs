using Dal.Interfaces;
using Dal.Tools;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dal.Implementations.SqlServer.Adapters;
using Services.Facade.Extensions;

namespace Dal.Implementations.SqlServer
{
    internal class CustomerRepository : ICustomerRepository
    {

        #region Statements
        private string InsertStatement
        {
            get => "DECLARE @NewID TABLE (IdCustomer uniqueidentifier); " +
                   "INSERT INTO [dbo].[Customer] (Name) " +
                   "OUTPUT INSERTED.IdCustomer INTO @NewID " +
                   "VALUES (@Name);" +
                   "SELECT IdCustomer FROM @NewID;";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Customer] SET [Name] = @Name WHERE IdCustomer = @IdCustomer";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Customer] WHERE IdCustomer = @IdCustomer";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdCustomer, Name FROM [dbo].[Customer] WHERE IdCustomer = @IdCustomer";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdCustomer, Name FROM [dbo].[Customer]";
        }
        #endregion


        public void Add(Customer entity)
        {
            try
            {
                //Para Stored procedures se puede utilizar SELECT SCOPE_IDENTITY()
                object returnValue = SqlHelper.ExecuteScalar(InsertStatement, CommandType.Text,
                  new SqlParameter[] { new SqlParameter("@Name", entity.Name) });

                entity.IdCustomer = Guid.Parse(returnValue.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Delete(Guid id)
        {
            //No se recomienda ejecutar delete directamente, aplicar update 
            //sobre algún campo "Habilitado", "Activo" = false
            //Persistir de alguna manera, fecha de baja, motivo, etc.
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> listCustomers = new List<Customer>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectAllStatement,
                                                    CommandType.Text,
                                                    new SqlParameter[] { }))
            {

                //Mientras tenga un registro para la lectura...avanzo
                while (reader.Read())
                {
                    //Leemos cada tupla de la tabla
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    Customer customer = CustomerAdapter.Current.Get(data);
                    listCustomers.Add(customer);
                }
            }

            return listCustomers;
        }

        public IEnumerable<Customer> GetByCreationDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Customer GetById(Guid id)
        {
            Customer customer = default;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectOneStatement,
                                                 CommandType.Text,
                                                 new SqlParameter[] {
                                                     new SqlParameter("@IdCustomer", id) }))
            {

                //Hacemos la lectura de un solo registro
                if (reader.Read())
                {
                    //Leemos cada tupla de la tabla
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    customer = CustomerAdapter.Current.Get(data);
                }
            }

            return customer;
        }

        public void Update(Customer entity)
        {
            try
            {
                int filasAfectadas = SqlHelper.ExecuteNonQuery(UpdateStatement, CommandType.Text,
                                     new SqlParameter[] { 
                                         new SqlParameter("@Name", entity.Name),
                                         new SqlParameter("@IdCustomer", entity.IdCustomer) });

                if (filasAfectadas == 0) new Exception("Algún problemita");

                Console.WriteLine($"Filas afectadas: {filasAfectadas.ToString()}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
