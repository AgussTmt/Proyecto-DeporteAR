using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Interfaces
{
    internal interface IPatenteRepository
    {
        Patente GetById(Guid id);
    }
}
