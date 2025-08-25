using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;
using DomainModel;

namespace DAL.Interfaces
{
    //Con IGenericRepository es suficiente
    public interface ICanchaRepository : IGenericRepository <Cancha>
    {
    }
}
