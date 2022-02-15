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
        readonly MusicContext _context;
        public UserRepository(MusicContext context)
        {
            _context = context;
        }
        public async Task<User> AddUser(User user)
        {
            return  (await _context.Users.AddAsync(user)).Entity;
        }
        public async Task<User> GetUserByName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<User> GetUser(User user)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x == user);
        }
        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User> UpdateUser(User user)
        {
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(x => x == user);
            userToUpdate = user;
            return userToUpdate;
        }

        public async void SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
