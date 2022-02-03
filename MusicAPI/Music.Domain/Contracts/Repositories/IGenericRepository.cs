using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        T Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        T FindByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}
