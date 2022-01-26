using AutoMapper;
using Moq;
using Music.Domain.Contracts.Repositories;
using Music.Domain.ErrorHandling;
using Music.Domain.ErrorHandling.Validations;
using Music.Domain.Services;
using Music.Spotify.Domain.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.Domain.Services.UserTests
{
    public static class UserServiceTestHelper
    {
        public static UserService CreateUserService(IMapper mapper, Mock<IUserRepository> moqIUserRepository, IEnumerable<Mock<IExternalService>> moqExternalServices, Mock<IUserTokensRepository> moqUserTokenRepository, UserCreationValidator userCreationValidator)
        {
            moqExternalServices ??= new List<Mock<IExternalService>>();
            var returnlist = (from item in moqExternalServices
                              select item.Object).ToList();
            moqIUserRepository ??= new Mock<IUserRepository>();
            moqUserTokenRepository ??= new Mock<IUserTokensRepository>();
            userCreationValidator ??= new UserCreationValidator();
            return new UserService(mapper, moqIUserRepository.Object, returnlist, moqUserTokenRepository.Object, userCreationValidator);
        }
    }
}
