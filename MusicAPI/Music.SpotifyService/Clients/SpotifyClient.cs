using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Clients;
using Music.Spotify.Models;
using Music.Spotify.Models.PlaylistModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Clients
{
    public class SpotifyClient : IClient
    {
        private readonly HttpClient _client = new HttpClient();
        public SpotifyClient(HttpClient client)
        {
            _client = client;
        }
        //TODO : Refactor this to be cleaner/ more reusable
        public async Task<ClientUser> GetCurrentClientUser(string authToken)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/"))
            {
                requestMessage.Headers.Add("Authorization", "Bearer " + authToken);

                var result = await _client.SendAsync(requestMessage);

                var json = result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<ClientUser>(json);
            }
        }

        public async Task<Playlist> GetUserPlaylistById(string authToken, string playlistId, string nextPageURL = null)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, nextPageURL ?? $"https://api.spotify.com/v1/playlists/{playlistId}/tracks?limit=50"))
            {
                requestMessage.Headers.Add("Authorization", "Bearer " + authToken);

                var result = await _client.SendAsync(requestMessage);

                var json = result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<Playlist>(json);
            }
        }

        public async Task<PlaylistCollection> GetAllUserPlaylists(string authToken, string nextPageURL = null)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, nextPageURL ?? "https://api.spotify.com/v1/me/playlists?limit=50"))
            {
                requestMessage.Headers.Add("Authorization", "Bearer " + authToken);
                var result = await _client.SendAsync(requestMessage);
                var json = result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<PlaylistCollection>(json);
            }
        }
        public string GetCurrentClientUserId(string authToken) => GetCurrentClientUser(authToken).Result.Id;
    }
}
