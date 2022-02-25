using Music.Spotify.Models;
using Music.Spotify.Models.PlaylistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.Spotify.Domain.Contracts.Services
{
    public interface ISpotifyPlaylistService
    {
        List<SpotifyPlaylistSummary> GetPlaylistSummary(string authToken, SpotifyUser spotifyUser, CancellationToken cancellationToken);
        List<SpotifyPlaylist> GetUserPlaylistCollectionWithTracks(string authToken, List<SpotifyPlaylistSummary> playlistInfos, CancellationToken cancellationToken);
        SpotifyPlaylist GetUserPlaylistWithTracks(string authToken, string playlistId, CancellationToken cancellationToken);
    }
}
