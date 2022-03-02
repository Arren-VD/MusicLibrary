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
using Music.UnitTesting.Moq.Automapper;
using System.Linq.Expressions;

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
        public async void CreateUserWithWorkingData(UserCreationDTO input,Task<User>existingUser,Task<User> userToAdd, Task<User> outputUser,Task<User> userInDb, Task<UserDTO> resultUser)
        {
            var mockRepo = new MockGenericRepository().FindByConditionAsync(existingUser).GetById(outputUser.Result, userInDb).Insert(userToAdd.Result, outputUser);
            var mockMapper = new MockMapper().Map<UserCreationDTO, User>(input, userToAdd.Result).Map<User,UserDTO>(userInDb.Result, resultUser.Result);
            CancellationToken cancellationToken = new CancellationToken();
            var userService = UserServiceTestHelper.CreateUserService(mockMapper, mockRepo, null, null);

            // Act
            var result = await userService.CreateUser(input, cancellationToken);

            // Assert
            result.Value.Id.Should().Be(1);
            result.Value.Name.Should().Be(input.Name);
        }
        
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserWithExistingUserReturnsError), MemberType = typeof(CreateUserTestData))]
        public async void CreateUserWithExistingUserReturnsError(UserCreationDTO input, Task<User> existingUser)
        {
            // Arrange
            //var a = It.Is<Expression<Func<User, bool>>>(y => y.Compile()(null);
            //var mockRepo = new MockGenericRepository().FindByConditionAsync<User>(It.Is<Expression<Func<User,bool>>>(y => y.Compile() (existingUser.Result)), existingUser);
            var mockRepo = new MockGenericRepository().FindByConditionAsync(existingUser);
            CancellationToken cancellationToken = new CancellationToken();
            var userService = UserServiceTestHelper.CreateUserService(null,mockRepo);

            // Act
            var result = await userService.CreateUser(input, cancellationToken);

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
            var mockRepo = new MockGenericRepository().FindByConditionAsync(existingUser);
            CancellationToken cancellationToken = new CancellationToken();

            var userService = UserServiceTestHelper.CreateUserService(null, mockRepo);

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
            var mockRepo = new MockGenericRepository().FindByConditionAsync(existingUser);
            CancellationToken cancellationToken = new CancellationToken();

            var userService = UserServiceTestHelper.CreateUserService(null, mockRepo);

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
            var mockRepo = new MockGenericRepository().FindByConditionAsync(existingUser);
            CancellationToken cancellationToken = new CancellationToken();

            var userService = UserServiceTestHelper.CreateUserService(null, mockRepo);

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
            var mockRepo = new MockGenericRepository().FindByConditionAsync(existingUser);
            CancellationToken cancellationToken = new CancellationToken();

            var userService = UserServiceTestHelper.CreateUserService(null, mockRepo);

            // Act
            var result = await userService.CreateUser(userCreation, cancellationToken);

            // Assert
            result.Value.Should().BeNull();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainEquivalentOf(resultError);

        }
    }
}
