using Microsoft.EntityFrameworkCore;
using Music.DataAccess.Database;
using Music.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public T Insert(T obj)
        {
            var result =  table.Add(obj).Entity;
            Save();
            return result;

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
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
