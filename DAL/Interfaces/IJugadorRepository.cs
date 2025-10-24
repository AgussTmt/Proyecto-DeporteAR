using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;
using DomainModel;

namespace DAL.Interfaces
{
    public interface IJugadorRepository : IGenericRepository<Jugador>
    {
        void Delete(Guid id);
        IEnumerable<Jugador> GetByEquipo(Guid idEquipo);
    }
}
