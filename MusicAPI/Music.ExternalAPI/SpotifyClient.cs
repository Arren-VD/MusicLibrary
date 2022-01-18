using Music.Domain.Contracts.Clients;
using Music.Models.SpotifyModels;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Music.ExternalAPI
{
    public class SpotifyClient : ISpotifyClient
    {
        private readonly  HttpClient _client = new HttpClient();
        public SpotifyClient(HttpClient client)
        {
            _client = client;
        }
        //TODO : Refactor this to be cleaner/ more reusable
        public async Task<SpotifyUser> GetCurrentSpotifyUser(string authToken)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/"))
            {
                requestMessage.Headers.Add("Authorization", "Bearer " + authToken);

                var result = await _client.SendAsync(requestMessage);

                var json = result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<SpotifyUser>(json);
            }
}
    }
}
