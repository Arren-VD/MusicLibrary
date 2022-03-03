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

namespace Music.IntegrationTesting.MusicIntegrationTests
{
    public class GetCurrentUserTracksWithPlaylistAndArtistIntegrationTests : IClassFixture<MusicWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly MusicWebApplicationFactory<Startup> _factory;
        private readonly MusicContext _context;

        public GetCurrentUserTracksWithPlaylistAndArtistIntegrationTests(MusicWebApplicationFactory<Startup> factory)
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
            var response = await _client.PostAsync($"api/Music/user/{userId}/getalltracks", Converters.GetStringContent(new { InvalidParameter = ""}));
            var r = response.RequestMessage.RequestUri.ToString();

            // Assert
            var result = await response.Content.ReadFromJsonAsync<string>();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            result.Should().BeEmpty(); ;
        }



        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
