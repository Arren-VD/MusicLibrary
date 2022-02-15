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
    public class MockUserTokenRepository : Mock<IUserTokensRepository>
    {
        public MockUserTokenRepository AddTokenById(UserClient userClient, Task<UserClient> Output)
        {
            Setup(repo => repo.AddTokenById(It.Is<UserClient>(uc =>uc.ClientId == userClient.ClientId && uc.UserId == userClient.UserId && uc.ClientName == userClient.ClientName
                ))).Returns(Output);
            return this;
        }
    }
}
