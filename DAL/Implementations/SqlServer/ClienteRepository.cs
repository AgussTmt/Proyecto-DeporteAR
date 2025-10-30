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
                IdCliente, Nombre, Telefono, Email
            FROM DbCliente";
        public void Add(Cliente cliente)
        {
            string sql = @"INSERT INTO DbCliente 
                           (IdCliente, Nombre, Telefono, Email)
                           VALUES
                           (@IdCliente, @Nombre, @Telefono, @Email)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdCliente", cliente.IdCliente),
                new SqlParameter("@Nombre", (object)cliente.Nombre ?? DBNull.Value),
                new SqlParameter("@Telefono", (object)cliente.Telefono ?? DBNull.Value),
                new SqlParameter("@Email", (object)cliente.Email ?? DBNull.Value)
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

        public IEnumerable<Cliente> GetAll()
        {
            var clientes = new List<Cliente>();

            // Usamos el _sqlSelect y el ExecuteReader de la clase base
            using (var reader = base.ExecuteReader(_sqlSelect, CommandType.Text))
            {
                while (reader.Read())
                {
                    // Usamos el patrón de GetValues() que establecimos
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    // Mapeamos con el adapter
                    clientes.Add(ClienteAdapter.Current.Get(values));
                }
            }
            return clientes;
        }

        public Cliente GetById(Guid id)
        {
            Cliente cliente = null;
            string sql = $"{_sqlSelect} WHERE IdCliente = @IdCliente";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdCliente", id)))
            {
                if (reader.Read()) // Usamos 'if' porque esperamos un solo resultado
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    cliente = ClienteAdapter.Current.Get(values);
                }
            }
            return cliente; // Devuelve null si no se encontró
        }

        public void Update(Cliente entity)
        {
            string sql = @"UPDATE DbCliente SET
                            Nombre = @Nombre,
                            Telefono = @Telefono,
                            Email = @Email
                           WHERE IdCliente = @IdCliente";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                new SqlParameter("@Telefono", (object)entity.Telefono ?? DBNull.Value),
                new SqlParameter("@Email", (object)entity.Email ?? DBNull.Value),
                new SqlParameter("@IdCliente", entity.IdCliente)
            );
        }
    }
}
