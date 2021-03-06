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
using System.Threading;

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
        public async Task<SpotifyUser> GetCurrentClientUser(string authToken, CancellationToken cancellationToken)
        {
            return await _httpHelper.Get<SpotifyUser>(authToken, "/me/");
        }

        public async Task<SpotifyPlaylist> GetUserPlaylistById(string authToken, string playlistId, CancellationToken cancellationToken, string nextPageURL = null)
        {
            return await _httpHelper.Get<SpotifyPlaylist>(authToken, nextPageURL ?? $"/playlists/{playlistId}/tracks?limit={Math.Min(_options.MaxTracks ?? 50, 50)}");
        }

        public async Task<SpotifyPlaylistSummaryCollection> GetAllUserPlaylists(string authToken, CancellationToken cancellationToken, string nextPageURL = null)
        {
            return await _httpHelper.Get<SpotifyPlaylistSummaryCollection>(authToken, nextPageURL ?? $"/me/playlists?limit={Math.Min(_options.MaxPlaylists ?? 50, 50)}");
        }
        public async Task<string> GetCurrentClientUserId(string authToken, CancellationToken cancellationToken)
        {
            return (await GetCurrentClientUser(authToken, cancellationToken)).Id;
    
        }
    }
}
