using Music.Models.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        public User AddUser(User user);

        public User GetUserByName(string name);

        void SaveChanges();

        User GetUser(User user);

        User UpdateUser(User user);
    }
}
