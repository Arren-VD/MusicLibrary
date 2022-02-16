using Microsoft.EntityFrameworkCore.Storage;
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
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);
        Task<T> Insert(T obj);
        Task Update(T obj);
        Task Delete(object id);
        Task Save();
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}
