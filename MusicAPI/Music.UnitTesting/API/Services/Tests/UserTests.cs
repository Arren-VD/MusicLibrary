using AutoMapper;
using Moq;
using Music.Domain.Contracts.Clients;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Services;
using Music.Models.Local;
using Music.Models.SpotifyModels;
using Music.UnitTesting.Moq.Clients;
using Music.UnitTesting.Moq.Repositories;
using Music.UnitTesting.TestData.Models;
using System;
using Xunit;
using FluentValidation;
using FluentAssertions;
using Music.Domain.MappingProfiles;
using System.Reflection;

namespace Music.UnitTesting.API.Services.Tests
{
    public class UserTests
    {
        private static IMapper _mapper;
        public UserTests()
        {
            if (_mapper == null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.AddMaps(typeof(SpotifyMappingProfile).GetTypeInfo().Assembly);
                });
                var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(SpotifyMappingProfile).GetTypeInfo().Assembly));
                IMapper mapper = config.CreateMapper();
                _mapper = mapper;
            }
        }
        [Theory]
        [MemberData(nameof(UserServiceTestData.LinkUserToSpotifyWorkingData), MemberType = typeof(UserServiceTestData))]
        public void LinkUserToSpotifyWorkingData(string authToken, int userId ,SpotifyUser spotifyUser,User userById, User returnUser )
        {                   
            // Arrange
            var SpotifyClientMoq = new MockSpotifyClient().GetCurrentSpotifyUser(authToken, spotifyUser);
            var mockUserRepository = new MockUserRepository().GetUserById(userId, userById).UpdateUser(userById, returnUser);
           
            var userService = UserServiceTestHelper.CreateUserService(_mapper,mockUserRepository, SpotifyClientMoq);
       
            // Act
            var result = userService.LinkUserToSpotify(authToken, userId);

            // Assert
            result.SpotifyId.Should().Be(spotifyUser.Id);
            result.Name.Should().Be(userById.Name);
            result.Id.Should().Be(userById.Id);
        }
    }
}
