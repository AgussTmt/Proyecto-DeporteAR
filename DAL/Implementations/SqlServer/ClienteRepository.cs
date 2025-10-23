using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Implementations.SqlServer.Adapters;
using DAL.Implementations.SqlServer.Helper;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class ClienteRepository : SqlTransactRepository, IClienteRepository
    {
        public ClienteRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {

        }
        private const string _sqlSelect = @"SELECT 
                IdCliente, Nombre, Telefono 
            FROM DbCliente";
        public void Add(Cliente cliente)
        {
            string sql = @"INSERT INTO DbCliente 
                           (IdCliente, Nombre, Telefono)
                           VALUES
                           (@IdCliente, @Nombre, @Telefono)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdCliente", cliente.IdCliente),
                new SqlParameter("@Nombre", (object)cliente.Nombre ?? DBNull.Value),
                new SqlParameter("@Telefono", (object)cliente.Telefono ?? DBNull.Value)
            );
        }

        public Cliente GetByNumero(string Numero)
        {
            string sql = $"{_sqlSelect} WHERE Telefono = @Telefono";
            Cliente cliente = null;

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Telefono", Numero)))
            {
                if (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    // Usamos el Adapter para mapear
                    cliente = ClienteAdapter.Current.Get(values);
                }
            }
            return cliente;
        }
    }
}
