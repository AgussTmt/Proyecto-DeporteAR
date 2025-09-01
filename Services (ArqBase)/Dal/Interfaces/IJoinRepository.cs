using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Interfaces
{
    internal interface IJoinRepository<T, Y>
    {
        List<Y> GetByObject(T obj);
        
        void Add(T obj, Y obj2);
    }
}
