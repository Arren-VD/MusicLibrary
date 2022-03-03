using MusicAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Music.IntegrationTesting
{
    public class RegisterControllerIntegrationTests : IClassFixture<MusicWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public RegisterControllerIntegrationTests(MusicWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        [InlineData(1)]
        [Theory]
        public async Task Read_GET_Action(int userId)
        {
            // Act
            var response = await _client.GetAsync($"/user/{userId}/getallplaylists");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("<h1 class=\"bg-info text-white\">Records</h1>", responseString);
        }
    }
}
