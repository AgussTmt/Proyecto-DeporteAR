using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbModel;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class ClienteRepository : IClienteRepository
    {
        public void Add(Cliente cliente)
        {
            using (var db = new DeporteARContext())
            {
                var clienteDb = new DbCliente { Nombre = cliente.Nombre, Telefono = cliente.Telefono };

                db.DbCliente.Add(clienteDb);
                db.SaveChanges();
            }
        }

        public Cliente GetByNumero(string Numero)
        {
            throw new NotImplementedException();
        }
    }
}
