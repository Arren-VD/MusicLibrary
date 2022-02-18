using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class GenericRepository : IGenericRepository

    {
        private readonly IDbContextFactory<MusicContext> _context;

        public GenericRepository(IDbContextFactory<MusicContext> context)
        {
            _context = context ?? throw new ArgumentNullException("MusicDB is null", (Exception)null); ;
        }
        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            using (var context = _context.CreateDbContext())
            {
                var table = context.Set<T>();
                return await table.ToListAsync();
            }
        }
        public async Task<T> GetById<T>(object id) where T : class
        {
            using (var context = _context.CreateDbContext()) 
            {
                var table = context.Set<T>();
                return await table.FindAsync(id);
            }
        }
        public async Task<T> Insert<T>(T obj) where T : class
        {
            using (var context = _context.CreateDbContext())
            {
                var table = context.Set<T>();
                var result = await table.AddAsync(obj);
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }
        public async Task Update<T>(T obj) where T : class
        {
            using (var context = _context.CreateDbContext())
            {
                var table = context.Set<T>();
                table.Attach(obj);
                context.Entry(obj).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete<T>(object id) where T : class
        {
            using (var context = _context.CreateDbContext())
            {
                var table = context.Set<T>();
                T existing = await table.FindAsync(id);
                table.Remove(existing);
                await context.SaveChangesAsync();
            }
        }
        public async Task<T> FindByConditionAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = _context.CreateDbContext())
            {
                var table = context.Set<T>();
                var result = await table.FirstOrDefaultAsync(predicate);
                await context.SaveChangesAsync();
                return result;
            }
        }
        public async Task<List<T>> FindAllByConditionAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = _context.CreateDbContext())
            {
                var table = context.Set<T>();
                var result = await table.Where(predicate).ToListAsync();
                await context.SaveChangesAsync();
                return result;
            }
        }
    }
}
