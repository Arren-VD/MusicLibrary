using Moq;
using Music.Domain.Contracts.Repositories;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.Moq.Repositories
{ 
    public class MockUserRepository : Mock<IUserRepository>
    {
        public MockUserRepository() : base(MockBehavior.Strict)
        {
            
        }
        public MockUserRepository GetUserById(int id, Task<User> Output)
        {
            Setup(repo => repo.GetUserById(id)).Returns(Output);
            return this;
        }
        public MockUserRepository GetUserByName(string name, Task<User> Output)
        {
            Setup(repo => repo.GetUserByName(name)).Returns(Output);
            return this;
        }
        public MockUserRepository UpdateUser(User user, Task<User> Output)
        {
            Setup(repo => repo.UpdateUser(user)).Returns(Output);
            return this;
        }
        public MockUserRepository GetUser(Task<User> user)
        {
            Setup(repo => repo.GetUser(It.Is<User>(u => u.Name == user.Result.Name && u.Id == user.Result.Id))).Returns(user);
            return this;
        }
        public MockUserRepository AddUser(User inputUser, Task<User> outputUser)
        {
            Setup(repo => repo.AddUser(It.Is<User>(u => u.Name == inputUser.Name && u.Id == inputUser.Id))).Returns(outputUser);
            return this;
        }
    }
}
