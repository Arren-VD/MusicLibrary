using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Music.DataAccess.Database;
using Music.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Music.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class

    {
        private readonly MusicContext _context;
        private readonly DbSet<T> table;

        public GenericRepository(MusicContext context)
        {
            _context = context ?? throw new ArgumentNullException("MusicDB is null", (Exception)null); ;
            table = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }
        public async Task<T> Insert(T obj)
        {
            var result =  await table.AddAsync(obj);
            await Save();
            return  result.Entity;

        }
        public async Task Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await Save();

        }
        public async Task Delete(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
            await Save();

        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            var result =  await table.FirstOrDefaultAsync(predicate);
            await Save();
            return result;

        }
    }
}
