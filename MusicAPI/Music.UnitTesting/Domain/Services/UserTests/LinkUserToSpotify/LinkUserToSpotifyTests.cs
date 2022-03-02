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
using System.Threading.Tasks;
using System.Threading;
using Music.UnitTesting.Moq.Automapper;

namespace Music.UnitTesting.Domain.Services.UserTests.LinkUserToSpotify
{
    public class LinkUserToSpotifyTests
    {
        private static IMapper _mapper;
        public LinkUserToSpotifyTests()
        { 

        }
        [Theory]
        [MemberData(nameof(LinkUserToSpotifyTestData.LinkUserToExternalAPISpotifyTest), MemberType = typeof(LinkUserToSpotifyTestData))]
        public async void LinkUserToSpotifyWorkingData(CancellationToken cancellationToken, int userId , List<UserTokenDTO> tokens,Task<string> spotifyId, UserClient userClient, Task<UserClient> outputUserClient, UserClientDTO outputDTO, List<UserClientDTO> outputDTOs)
        {
            // Arrange
            var svcs = new List<MockExternalService>();
            svcs.Add(new MockExternalService().GetName("Spotify").ReturnClientUserId(cancellationToken,tokens.FirstOrDefault().Value,spotifyId));
            var mockRepo = new MockGenericRepository().InsertAny(outputUserClient);
            var mockMapper = new MockMapper().Map<UserClient, UserClientDTO>(outputUserClient.Result, outputDTO);
            //var userService = UserServiceTestHelper.CreateUserService(mockMapper, mockRepo, svcs.ToList() );
            var userService = ServiceTestHelper.CreateGenericService<UserService>(new object[] { mockMapper,mockRepo , svcs.ToList() });

            // Act
            var result = await userService.LinkUserToExternalAPIs(userId, tokens, cancellationToken);

            // Assert
            result.First().Should().NotBeNull();
            result.First().ClientId.Should().Be(spotifyId.Result);
            result.First().ClientName.Should().Be(tokens.First().Name);
            result.First().UserId.Should().Be(outputDTOs.First().UserId);
        }
    }
}
