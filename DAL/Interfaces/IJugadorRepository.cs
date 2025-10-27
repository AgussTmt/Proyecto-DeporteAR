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
        IEnumerable<Jugador> GetByEquipo(Guid idEquipo);

        List<Jugador> GetSinEquipo();
    }
}
