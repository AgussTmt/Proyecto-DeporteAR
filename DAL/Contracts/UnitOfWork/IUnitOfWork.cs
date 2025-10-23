using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrones_3parcial.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUnitOfWorkAdapter Create();
    }
}
