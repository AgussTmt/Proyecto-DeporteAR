using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations.SqlServer.Adapters
{
    internal class ClienteAdapter
    {
        private readonly static ClienteAdapter _instance = new ClienteAdapter();

        public static ClienteAdapter Current
        {
            get { return _instance; }
        }

        private ClienteAdapter() { }

        public Cliente Get(object[] values)
        {
            if (values == null || values.Length < 3)
            {
                throw new ArgumentException("Se requieren 3 valores para mapear un Cliente.");
            }

            return new Cliente
            {
                IdCliente = (Guid)values[0],
                Nombre = values[1] == DBNull.Value ? null : values[1].ToString(),
                Telefono = values[2] == DBNull.Value ? null : values[2].ToString()
            };
        }
    }
}
