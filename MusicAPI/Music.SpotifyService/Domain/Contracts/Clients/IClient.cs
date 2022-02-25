using Music.Spotify.Models;
using Music.Spotify.Models.PlaylistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.Spotify.Domain.Contracts
{
    public interface IClient
    {
        Task<SpotifyUser> GetCurrentClientUser(string authToken, CancellationToken cancellationToken);
        Task<string> GetCurrentClientUserId(string authToken, CancellationToken cancellationToken);
        Task<SpotifyPlaylistSummaryCollection> GetAllUserPlaylists(string authToken, CancellationToken cancellationToken, string nextPageURL = null);
        Task<SpotifyPlaylist> GetUserPlaylistById(string authToken, string playlistId, CancellationToken cancellationToken, string nextPageURL = null);
    }
}
