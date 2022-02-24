using AutoMapper;
using Moq;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Services;
using Music.Models;
using Music.UnitTesting.Moq.Repositories;
using System;
using Xunit;
using FluentValidation;
using FluentAssertions;
using Music.Domain.MappingProfiles;
using System.Reflection;
using Music.Views;
using System.Collections.Generic;
using Music.UnitTesting.Moq.Services;
using System.Linq;
using Music.UnitTesting.Domain.Services.UserTests.CreateUser;
using Music.Domain.ErrorHandling;
using System.Threading.Tasks;
using System.Threading;

namespace Music.UnitTesting.Domain.Services.UserTests.Tests.CreateUser
{
    public class CreateUserTests
    {
        private static IMapper _mapper;
        public CreateUserTests()
        {
            if (_mapper == null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.AddMaps(typeof(PlaylistProfile).GetTypeInfo().Assembly);
                });
                var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(PlaylistProfile).GetTypeInfo().Assembly));
                IMapper mapper = config.CreateMapper();
                _mapper = mapper;
            }
        }
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserTest), MemberType = typeof(CreateUserTestData))]
        public async void CreateUserWithWorkingData(UserCreationDTO userCreation,Task<User>existingUser,User inputUser, Task<User> outputUser, Task<UserDTO> userOutput)
        {
            // Arrange
            var mockUserRespository= new MockUserRepository().GetUserByName(userCreation.Name, existingUser).GetUser(outputUser).AddUser(inputUser, outputUser);
            CancellationToken cancellationToken = new CancellationToken();
            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = await userService.CreateUser(userCreation, cancellationToken);

            // Assert
            result.Value.Id.Should().Be(1);
            result.Value.Name.Should().Be(userCreation.Name);
        }
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserWithExistingUserReturnsError), MemberType = typeof(CreateUserTestData))]
        public async void CreateUserWithExistingUserReturnsError(UserCreationDTO userCreation, Task<User> existingUser, User inputUser, Task<User> outputUser, Task<UserDTO> userOutput)
        {
            // Arrange
            var mockUserRespository = new MockUserRepository().GetUserByName(userCreation.Name, existingUser).GetUser(outputUser).AddUser(inputUser, outputUser);
            CancellationToken cancellationToken = new CancellationToken();

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = await userService.CreateUser(userCreation, cancellationToken);

            // Assert
            result.Value.Should().BeNull();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Message.Should().Be("User with name 'Rick' already exists");
            result.Errors.First().Parameter.Should().Be("Name");
            result.Errors.First().Keyword.Should().Be("User");
            result.Errors.First().Code.Should().Be(Error.ErrorValues.AlreadyExists);
        }
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserWithNameShorterThanTwoCharactersReturnsError), MemberType = typeof(CreateUserTestData))]
        public async void CreateUserWithNameShorterThanTwoCharactersReturnsError(UserCreationDTO userCreation, Task<User> existingUser, Error resultError)
        {
            // Arrange
            var mockUserRespository = new MockUserRepository().GetUserByName(userCreation.Name, existingUser);
            CancellationToken cancellationToken = new CancellationToken();

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = await userService.CreateUser(userCreation, cancellationToken);

            // Assert
            result.Value.Should().BeNull();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().BeEquivalentTo(resultError);
        }
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserWithNameLongerThan30CharactersReturnsError), MemberType = typeof(CreateUserTestData))]
        public async void CreateUserWithNameLongerThan30CharactersReturnsError(UserCreationDTO userCreation, Task<User> existingUser, Error resultError)
        {
            // Arrange
            var mockUserRespository = new MockUserRepository().GetUserByName(userCreation.Name, existingUser);
            CancellationToken cancellationToken = new CancellationToken();

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = await userService.CreateUser(userCreation, cancellationToken);

            // Assert
            result.Value.Should().BeNull();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().BeEquivalentTo(resultError);
        }
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserWithEmptyNameReturnsError), MemberType = typeof(CreateUserTestData))]
        public async void CreateUserWithEmptyNameReturnsError(UserCreationDTO userCreation, Task<User> existingUser, Error resultError, Error resultError2)
        {
            // Arrange
            var mockUserRespository = new MockUserRepository().GetUserByName(userCreation.Name, existingUser);
            CancellationToken cancellationToken = new CancellationToken();

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = await userService.CreateUser(userCreation, cancellationToken);

            // Assert
            result.Value.Should().BeNull();
            result.Errors.Count.Should().Be(2);
            result.Errors.Should().ContainEquivalentOf(resultError);
            result.Errors.Should().ContainEquivalentOf(resultError2);
        }
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserWithNullNameReturnsError), MemberType = typeof(CreateUserTestData))]
        public async void CreateUserWithNullNameReturnsError(UserCreationDTO userCreation, Task<User> existingUser, Error resultError )
        {
            // Arrange
            var mockUserRespository = new MockUserRepository().GetUserByName(userCreation.Name, existingUser);
            CancellationToken cancellationToken = new CancellationToken();

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = await userService.CreateUser(userCreation, cancellationToken);

            // Assert
            result.Value.Should().BeNull();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainEquivalentOf(resultError);

        }
    }
}
