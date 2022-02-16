using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        public Task<User> AddUser(User user);

        public Task<User> GetUserByName(string name);

        Task SaveChanges();

        Task<User> GetUser(User user);

        Task<User> UpdateUser(User user);
    }
    public interface ITempRepo
    {
        Task<Track> FindByConditionAsync(int id);
    }
}
