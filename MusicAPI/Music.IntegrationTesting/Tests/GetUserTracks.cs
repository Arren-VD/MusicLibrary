using FluentAssertions;
using Music.DataAccess.Database;
using Music.Domain.Services.Helpers;
using Music.IntegrationTesting.Helpers;
using Music.IntegrationTesting.PlaylistIntegrationTests;
using Music.Views;
using MusicAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Music.IntegrationTesting.Tests.Music
{
    public class GetUserTracks : IClassFixture<MusicWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly MusicContext _context;

        public GetUserTracks(MusicWebApplicationFactory<Startup> factory)
        {
            _context = DatabaseFixture.getDB(factory);
            _client = factory.CreateClient();
        }

        [InlineData(1)]
        [Theory]
        public async Task GetAllEmptyDB(int userId)
        {
            // Act
            var response = await _client.GetAsync($"api/Music/user/{userId}/getalltracks");

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PagingWrapper<TrackDTO>>();

            response?.StatusCode.Should().Be(HttpStatusCode.OK);
            response?.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
            result?.Collection.Should().BeEmpty();
            result?.TotalPages.Should().Be(0);
        }

        [InlineData(1)]
        [Theory]
        public async Task GetAllWithFullListInDBReturnsTracksWithCorrectUserId(int userId)
        {
            // Act
            var response = await _client.GetAsync($"api/Music/user/{userId}/getalltracks");

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PagingWrapper<TrackDTO>>();

            response?.StatusCode.Should().Be(HttpStatusCode.OK);
            response?.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
            result?.Collection.Should().BeEmpty();
            result?.TotalPages.Should().Be(0);
        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
