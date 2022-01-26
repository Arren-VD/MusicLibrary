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
        public MockUserRepository GetUserById(int id, User Output)
        {
            Setup(repo => repo.GetUserById(id)).Returns(Output);
            return this;
        }
        public MockUserRepository GetUserByName(string name, User Output)
        {
            Setup(repo => repo.GetUserByName(name)).Returns(Output);
            return this;
        }
        public MockUserRepository UpdateUser(User user, User Output)
        {
            Setup(repo => repo.UpdateUser(user)).Returns(Output);
            return this;
        }
        public MockUserRepository GetUser(User user)
        {
            Setup(repo => repo.GetUser(It.Is<User>(u => u.Name == user.Name && u.Id == user.Id))).Returns(user);
            return this;
        }
        public MockUserRepository AddUser(User inputUser, User outputUser)
        {
            Setup(repo => repo.AddUser(It.Is<User>(u => u.Name == inputUser.Name && u.Id == inputUser.Id))).Returns(outputUser);
            return this;
        }
    }
}
