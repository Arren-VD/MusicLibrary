using Microsoft.EntityFrameworkCore;
using Music.DataAccess.Database;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Exceptions;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly IDbContextFactory<MusicContext> _context;
        public UserRepository(IDbContextFactory<MusicContext> context)
        {
            _context = context;
        }
        public async Task<User> AddUser(User user)
        {
            using (var context = _context.CreateDbContext())
            {
                return (await context.Users.AddAsync(user)).Entity;
            }
        }
        public async Task<User> GetUserByName(string name)
        {
            using (var context = _context.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x.Name == name);
            }
        }
        public async Task<User> GetUser(User user)
        {
            using (var context = _context.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x == user);
            }
        }
        public async Task<User> GetUserById(int id)
        {
            using (var context = _context.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            }
        }
        public async Task<User> UpdateUser(User user)
        {
            using (var context = _context.CreateDbContext())
            {
                var userToUpdate = await context.Users.FirstOrDefaultAsync(x => x == user);
                userToUpdate = user;
                return userToUpdate;
            }
        }

        public async Task SaveChanges()
        {
            using (var context = _context.CreateDbContext())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
