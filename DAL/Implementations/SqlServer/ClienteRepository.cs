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
    /// <summary>
    /// Repositorio SQL para gestionar las entidades <see cref="Cliente"/>.
    /// Provee métodos de ABM (CRUD) para los clientes.
    /// </summary>
    /// <remarks>
    /// Opera dentro de una transacción y conexión SQL existente (Unit of Work).
    /// </remarks>
    internal class ClienteRepository : SqlTransactRepository, IClienteRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de conexión y transacción de una
        /// Unidad de Trabajo (Unit of Work) existente.
        /// </summary>
        /// <param name="context">La <see cref="SqlConnection"/> activa.</param>
        /// <param name="_transaction">La <see cref="SqlTransaction"/> activa.</param>
        public ClienteRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {

        }
        private const string _sqlSelect = @"SELECT 
                IdCliente, Nombre, Telefono, Email
            FROM DbCliente";

        /// <summary>
        /// Agrega un nuevo <see cref="Cliente"/> a la base de datos.
        /// </summary>
        /// <param name="cliente">La entidad <see cref="Cliente"/> a insertar.</param>
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

        /// <summary>
        /// Obtiene un <see cref="Cliente"/> específico buscando por su número de teléfono.
        /// </summary>
        /// <param name="Numero">El número de teléfono a buscar.</param>
        /// <returns>El <see cref="Cliente"/> encontrado, o <c>null</c>.</returns>
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

        /// <summary>
        /// Obtiene una lista de todos los clientes registrados.
        /// </summary>
        /// <returns>Una colección de <see cref="Cliente"/>.</returns>
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

        /// <summary>
        /// Obtiene un <see cref="Cliente"/> específico por su ID (PK).
        /// </summary>
        /// <param name="id">El ID (PK) del cliente.</param>
        /// <returns>El <see cref="Cliente"/> encontrado, o <c>null</c>.</returns>
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

        /// <summary>
        /// Actualiza un registro de <see cref="Cliente"/> existente en la base de datos.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Cliente"/> con los datos modificados.</param>
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

        /// <summary>
        /// Elimina un <see cref="Cliente"/> de la base de datos.
        /// </summary>
        /// <param name="id">El ID (PK) del cliente a eliminar.</param>
        public void Delete(Guid id)
        {
            string sql = "DELETE FROM DbCliente WHERE IdCliente = @IdCliente";
            base.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@IdCliente", id));
        }
    }
}