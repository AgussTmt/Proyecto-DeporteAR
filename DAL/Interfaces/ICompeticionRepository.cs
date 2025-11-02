using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;
using DomainModel;

namespace DAL.Interfaces
{
    public interface ICompeticionRepository : IGenericRepository <Competicion>
    {
        List<Competicion> GetByTimeAndCancha(Competicion competicion);

        void AddEquipo(Guid idCompeticion, Guid idEquipo);

        List<Competicion> GetWithVacancies();

        List<Competicion> GetByClient(Cliente cliente);

        void RemoveEquipo(Guid idCompeticion, Guid idEquipo);
        List<Competicion> GetByEquipo(Guid idEquipo);
        void RemoveAllEquipos(Guid idCompeticion);

    }
}
