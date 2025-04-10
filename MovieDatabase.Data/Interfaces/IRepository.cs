using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
      IQueryable<T> GetAll();
        void Create(T entity);
        void Delete(T enity);
    }
}
