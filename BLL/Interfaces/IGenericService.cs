using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(Guid id);

        IEnumerable<T> GetAll();

        T GetById(Guid id);
    }
}
