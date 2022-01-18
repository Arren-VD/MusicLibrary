using AutoMapper;
using Moq;
using Music.Domain.Contracts.Clients;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.API.Services
{
    public static class UserServiceTestHelper
    {
        public static UserService CreateUserService(IMapper mapper, Mock<IUserRepository> moqIUserRepository)
        {
            Mock<ISpotifyClient> moqSpotifyClient = new Mock<ISpotifyClient>();
            return new UserService(moqSpotifyClient.Object, mapper, moqIUserRepository.Object);
        }
        public static UserService CreateUserService(IMapper mapper,Mock<ISpotifyClient> moqSpotifyClient)
        {
            Mock<IUserRepository> moqIUserRepository = new Mock<IUserRepository>();
            return new UserService(moqSpotifyClient.Object, mapper, moqIUserRepository.Object);
        }
        public static UserService CreateUserService(IMapper mapper, Mock<IUserRepository> moqIUserRepository, Mock<ISpotifyClient> moqSpotifyClient)
        {
            return new UserService(moqSpotifyClient.Object, mapper, moqIUserRepository.Object);
        }
    }
}
