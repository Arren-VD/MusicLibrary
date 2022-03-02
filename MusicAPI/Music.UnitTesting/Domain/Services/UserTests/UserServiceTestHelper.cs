using AutoMapper;
using Moq;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.ErrorHandling;
using Music.Domain.ErrorHandling.Validations;
using Music.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.Domain.Services.UserTests
{
    public static class UserServiceTestHelper
    {
        public static UserService CreateUserService(Mock<IMapper> mapper = null, Mock<IGenericRepository> moqRepo = null, IEnumerable<Mock<IExternalService>> moqExternalServices = null, UserCreationValidator userCreationValidator = null)
        {
            mapper ??= new Mock<IMapper>();
            moqExternalServices ??= new List<Mock<IExternalService>>();
            var returnlist = (from item in moqExternalServices
                              select item.Object).ToList();
            userCreationValidator ??= new UserCreationValidator();
            return new UserService(mapper.Object, moqRepo.Object, returnlist, userCreationValidator);
        }
    }
}
