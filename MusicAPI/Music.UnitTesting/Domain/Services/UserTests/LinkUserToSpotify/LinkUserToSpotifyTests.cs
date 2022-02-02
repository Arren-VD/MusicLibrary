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

namespace Music.UnitTesting.Domain.Services.UserTests.LinkUserToSpotify
{
    public class LinkUserToSpotifyTests
    {
        private static IMapper _mapper;
        public LinkUserToSpotifyTests()
        {
            if (_mapper == null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.AddMaps(typeof(ClientMappingProfile).GetTypeInfo().Assembly);
                });
                var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(ClientMappingProfile).GetTypeInfo().Assembly));
                IMapper mapper = config.CreateMapper();
                _mapper = mapper;
            }
        }
        [Theory]
        [MemberData(nameof(LinkUserToSpotifyTestData.LinkUserToExternalAPISpotifyTest), MemberType = typeof(LinkUserToSpotifyTestData))]
        public void LinkUserToSpotifyWorkingData(int userId , List<UserTokenDTO> tokens,string spotifyId, List<UserClient> userClients, UserClient outputUserClient)
        {
            // Arrange
            var svcs = new List<MockExternalService>();
            svcs.Add(new MockExternalService().GetName("Spotify").ReturnClientUserId(tokens.FirstOrDefault().Value,spotifyId));
            var mockUserTokenRepository = new MockUserTokenRepository().AddTokenById(userClients.FirstOrDefault(), outputUserClient);

            var userService = UserServiceTestHelper.CreateUserService(_mapper,null, svcs.ToList(), mockUserTokenRepository,null);
       
            // Act
            var result = userService.LinkUserToExternalAPIs(userId, tokens);

            // Assert
            result.First().Should().NotBeNull();
            result.First().ClientId.Should().Be(spotifyId);
            result.First().ClientName.Should().Be(tokens.First().Name);
            result.First().UserId.Should().Be(userClients.First().UserId);
        }
    }
}
