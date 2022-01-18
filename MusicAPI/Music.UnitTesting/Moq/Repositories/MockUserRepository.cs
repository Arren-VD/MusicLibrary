using Moq;
using Music.Domain.Contracts.Repositories;
using Music.Models.Local;
using Music.Models.SpotifyModels;
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
        public MockUserRepository UpdateUser(User user, User Output)
        {
            Setup(repo => repo.UpdateUser(user)).Returns(Output);
            return this;
        }
    }
}
