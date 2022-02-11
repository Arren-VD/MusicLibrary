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
        public void CreateUserWithWorkingData(UserCreationDTO userCreation,User existingUser,User inputUser, User outputUser, UserDTO userOutput)
        {
            // Arrange
            var mockUserRespository= new MockUserRepository().GetUserByName(userCreation.Name, existingUser).GetUser(outputUser).AddUser(inputUser, outputUser);

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = userService.CreateUser(userCreation);

            // Assert
            result.Value.Id.Should().Be(1);
            result.Value.Name.Should().Be(userCreation.Name);
        }
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserWithExistingUserReturnsError), MemberType = typeof(CreateUserTestData))]
        public void CreateUserWithExistingUserReturnsError(UserCreationDTO userCreation, User existingUser, User inputUser, User outputUser, UserDTO userOutput)
        {
            // Arrange
            var mockUserRespository = new MockUserRepository().GetUserByName(userCreation.Name, existingUser).GetUser(outputUser).AddUser(inputUser, outputUser);

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = userService.CreateUser(userCreation);

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
        public void CreateUserWithNameShorterThanTwoCharactersReturnsError(UserCreationDTO userCreation, User existingUser, Error resultError)
        {
            // Arrange
            var mockUserRespository = new MockUserRepository().GetUserByName(userCreation.Name, existingUser);

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = userService.CreateUser(userCreation);

            // Assert
            result.Value.Should().BeNull();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().BeEquivalentTo(resultError);
        }
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserWithNameLongerThan30CharactersReturnsError), MemberType = typeof(CreateUserTestData))]
        public void CreateUserWithNameLongerThan30CharactersReturnsError(UserCreationDTO userCreation, User existingUser, Error resultError)
        {
            // Arrange
            var mockUserRespository = new MockUserRepository().GetUserByName(userCreation.Name, existingUser);

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = userService.CreateUser(userCreation);

            // Assert
            result.Value.Should().BeNull();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Should().BeEquivalentTo(resultError);
        }
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserWithEmptyNameReturnsError), MemberType = typeof(CreateUserTestData))]
        public void CreateUserWithEmptyNameReturnsError(UserCreationDTO userCreation, User existingUser, Error resultError, Error resultError2)
        {
            // Arrange
            var mockUserRespository = new MockUserRepository().GetUserByName(userCreation.Name, existingUser);

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = userService.CreateUser(userCreation);

            // Assert
            result.Value.Should().BeNull();
            result.Errors.Count.Should().Be(2);
            result.Errors.Should().ContainEquivalentOf(resultError);
            result.Errors.Should().ContainEquivalentOf(resultError2);
        }
        [Theory]
        [MemberData(nameof(CreateUserTestData.CreateUserWithNullNameReturnsError), MemberType = typeof(CreateUserTestData))]
        public void CreateUserWithNullNameReturnsError(UserCreationDTO userCreation, User existingUser, Error resultError )
        {
            // Arrange
            var mockUserRespository = new MockUserRepository().GetUserByName(userCreation.Name, existingUser);

            var userService = UserServiceTestHelper.CreateUserService(_mapper, mockUserRespository, null, null, null);

            // Act
            var result = userService.CreateUser(userCreation);

            // Assert
            result.Value.Should().BeNull();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainEquivalentOf(resultError);

        }
    }
}
