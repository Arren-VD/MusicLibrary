﻿using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Clients;
using Music.Spotify.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Music.Spotify.Clients
{
    public class SpotifyClient : IClient
    {
        private readonly  HttpClient _client = new HttpClient();
        public SpotifyClient(HttpClient client)
        {
            _client = client;
        }
        //TODO : Refactor this to be cleaner/ more reusable
        public async Task<ClientUser> GetCurrentSpotifyUser(string authToken)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/"))
            {
                requestMessage.Headers.Add("Authorization", "Bearer " + authToken);

                var result = await _client.SendAsync(requestMessage);

                var json = result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<ClientUser>(json);
            }
        }
    }
}