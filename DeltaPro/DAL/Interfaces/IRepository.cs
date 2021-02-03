using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
 public   interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        T Get(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Remove(int id);
        Task Save();



    }
}
