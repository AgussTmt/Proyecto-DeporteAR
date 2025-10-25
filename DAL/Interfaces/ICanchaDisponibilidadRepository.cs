using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DAL.Interfaces
{
    public interface ICanchaDisponibilidadRepository
    {
        List<CanchaDisponibilidad> GetByCancha(Guid idCancha);

        void Add(CanchaDisponibilidad entity);

        void DeleteByCancha(Guid idCancha);
    }
}
