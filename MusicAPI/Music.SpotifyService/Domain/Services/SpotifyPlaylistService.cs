using AutoMapper;
using Microsoft.Extensions.Options;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Services;
using Music.Spotify.Models;
using Music.Spotify.Models.PlaylistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.Spotify.Domain.Services
{
    internal class SpotifyPlaylistService : ISpotifyPlaylistService
    {
        private readonly IClient _client;
        private readonly SpotifyOptions _options;
        public SpotifyPlaylistService(IClient client, IOptions<SpotifyOptions> options)
        {
            _options = options.Value;
            _client = client;
        }
        public List<SpotifyPlaylistSummary> GetPlaylistSummary(string authToken, SpotifyUser spotifyUser, CancellationToken cancellationToken)
        {
            var playlistInfoCollection = new List<SpotifyPlaylistSummary>();
            var userPlaylists = _client.GetAllUserPlaylists(authToken, cancellationToken).Result;

            while (userPlaylists != null && !(playlistInfoCollection.Count >= _options.MaxPlaylists))
            {
                cancellationToken.ThrowIfCancellationRequested();
                playlistInfoCollection.AddRange(userPlaylists.items.Where(x => x.name != "All" && x.owner.id == spotifyUser.Id));
                if (userPlaylists.next == null)
                    userPlaylists = null;
                if (userPlaylists != null)
                    userPlaylists = _client.GetAllUserPlaylists(authToken, cancellationToken, userPlaylists.next).Result;
            }
            return playlistInfoCollection;
        }
        public List<SpotifyPlaylist> GetUserPlaylistCollectionWithTracks(string authToken, List<SpotifyPlaylistSummary> playlistInfos, CancellationToken cancellationToken)
        {
            var playlistCollection = new List<SpotifyPlaylist>();
            playlistInfos.ForEach(x => {
                var playlist = GetUserPlaylistWithTracks(authToken, x.id, cancellationToken);
                playlist.Name = x.name;
                playlist.Id = x.id;
                playlistCollection.Add(playlist);
            });
            return playlistCollection;
        }
        public SpotifyPlaylist GetUserPlaylistWithTracks(string authToken, string playlistId, CancellationToken cancellationToken)
        {
            var playlistcollection = new SpotifyPlaylist();
            var userPlaylist = _client.GetUserPlaylistById(authToken, playlistId, cancellationToken).Result;

            while (userPlaylist != null && !(playlistcollection.items.Count >= _options.MaxTracks))
            {
                cancellationToken.ThrowIfCancellationRequested();
                playlistcollection.items.AddRange(userPlaylist.items);
                if (userPlaylist.Next == null)
                    userPlaylist = null;
                if (userPlaylist != null)
                    userPlaylist = _client.GetUserPlaylistById(authToken, playlistId, cancellationToken, userPlaylist.Next).Result;
            }
            return playlistcollection;

        }
    }
}
