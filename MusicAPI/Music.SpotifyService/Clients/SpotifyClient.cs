using Microsoft.Extensions.Options;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Clients;
using Music.Spotify.Models;
using Music.Spotify.Models.PlaylistModels;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Music.Spotify.Clients.ClientErrors;
using Music.Domain.Exceptions;
using Music.Spotify.Clients.Helpers;

namespace Music.Spotify.Clients
{
    public class SpotifyClient : IClient
    {
        private readonly SpotifyOptions _options;
        private readonly HttpRequestHelper _httpHelper;
        public SpotifyClient(IOptions<SpotifyOptions> options, HttpRequestHelper httpHelper)
        {
            _httpHelper = httpHelper;
            _options = options.Value;
        }
        //TODO : Refactor this to be cleaner/ more reusable
        public async Task<ClientUser> GetCurrentClientUser(string authToken)
        {
            return _httpHelper.Get<ClientUser>(authToken, "/me/").Result;
        }

        public async Task<Playlist> GetUserPlaylistById(string authToken, string playlistId, string nextPageURL = null)
        {
            return _httpHelper.Get<Playlist>(authToken, nextPageURL ?? $"/playlists/{playlistId}/tracks?limit={Math.Min(_options.MaxTracks ?? 50, 50)}").Result;
        }

        public async Task<SpotifyPlaylistCollection> GetAllUserPlaylists(string authToken, string nextPageURL = null)
        {
            return _httpHelper.Get<SpotifyPlaylistCollection>(authToken, nextPageURL ?? $"/me/playlists?limit={Math.Min(_options.MaxPlaylists ?? 50, 50)}").Result;
        }
        public string GetCurrentClientUserId(string authToken) => GetCurrentClientUser(authToken).Result.Id;
    }
}
