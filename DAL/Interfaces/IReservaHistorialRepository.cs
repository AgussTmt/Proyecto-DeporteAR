using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DAL.Interfaces
{
    public interface IReservaHistorialRepository
    {
        void Add(ReservaHistorial entity);
    }
}
