using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Implementations.SqlServer.Helper;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class EquipoRepository : SqlTransactRepository, IEquipoRepository
    {
        public EquipoRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        public void Add(Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public void AddPlayers(List<Jugador> jugadores)
        {
            throw new NotImplementedException();
        }

        public Equipo GetByCompeticion(Competicion competicion)
        {
            throw new NotImplementedException();
        }

        public void Update(Equipo equipo)
        {
            throw new NotImplementedException();
        }
    }
}
