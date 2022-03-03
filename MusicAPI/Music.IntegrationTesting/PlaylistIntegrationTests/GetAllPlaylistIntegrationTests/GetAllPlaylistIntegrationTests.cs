using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Music.DataAccess.Database;
using Music.Views;
using Music.Models;
using MusicAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Music.Views.Results;

namespace Music.IntegrationTesting.PlaylistIntegrationTests.GetAllPlaylistIntegrationTests
{
    public class GetAllPlaylistIntegrationTests : IClassFixture<MusicWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly MusicWebApplicationFactory<Startup> _factory;
        private readonly MusicContext _context;

        public GetAllPlaylistIntegrationTests(MusicWebApplicationFactory<Startup> factory)
        {
            _context = DatabaseFixture.getDB(factory);
            _client = factory.CreateClient();
            _factory = factory;
        }

        [InlineData(1)]
        [Theory]
        public async Task GetAllEmptyDB(int userId)
        {
            // Act
            var response = await _client.GetAsync($"api/Playlist/user/{userId}/getallplaylists");
            var r = response.RequestMessage.RequestUri.ToString();

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<TrackPlaylistDTO>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            result.Should().BeEmpty();;
        }

        [MemberData(nameof(GetAllPlaylistIntegrationTestsData.GetAllSeededDBWithOnlyUserData), MemberType = typeof(GetAllPlaylistIntegrationTestsData))]
        [Theory]
        public async Task GetAllSeededDBWithOnlyUserData(int userId,List<Playlist> playlists, List<PlaylistResult> dtos)
        {
            //arrange
            _context.Playlist.AddRange(playlists);
            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync($"api/Playlist/user/{userId}/getallplaylists");
            var r = response.RequestMessage.RequestUri.ToString();

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<PlaylistResult>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(dtos);
        }

        [MemberData(nameof(GetAllPlaylistIntegrationTestsData.GetAllSeededDBWithDifferentUserData), MemberType = typeof(GetAllPlaylistIntegrationTestsData))]
        [Theory]
        public async Task GetAllSeededDBWithDifferentUserData(int userId, List<Playlist> userPlaylists, List<Playlist> otherUserPlaylists, List<PlaylistResult> dtos)
        {
            //arrange
            _context.Playlist.AddRange(userPlaylists);
            _context.Playlist.AddRange(otherUserPlaylists);
            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync($"api/Playlist/user/{userId}/getallplaylists");
            var r = response.RequestMessage.RequestUri.ToString();

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<TrackPlaylistDTO>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(dtos);
        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
