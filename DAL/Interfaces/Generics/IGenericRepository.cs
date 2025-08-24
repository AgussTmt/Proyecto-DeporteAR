using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(Guid id);

        IEnumerable<T> GetAll();

        T GetById(Guid id);
    }
}
