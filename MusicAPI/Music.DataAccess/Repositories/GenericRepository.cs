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
        private MusicContext _context;
        private DbSet<T> table;

        public GenericRepository(MusicContext context)
        {
            _context = context;
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
            Save();
            return  result.Entity;

        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            Save();

        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            Save();

        }
        public async void Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await table.FirstOrDefaultAsync(predicate);
        }
    }
}
