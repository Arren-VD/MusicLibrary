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
        public User AddUser(User user)
        {
            return  _context.Users.Add(user).Entity;
        }
        public User GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(x => x.Name == name);
        }
        public User GetUser(User user)
        {
            return _context.Users.FirstOrDefault(x => x == user);
        }
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }
        public User UpdateUser(User user)
        {
            var userToUpdate = _context.Users.FirstOrDefault(x => x == user);
            userToUpdate = user;
            return userToUpdate;

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
