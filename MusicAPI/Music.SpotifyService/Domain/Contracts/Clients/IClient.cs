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
        Task<SpotifyUser> GetCurrentClientUser(CancellationToken cancellationToken,string authToken);
        Task<string> GetCurrentClientUserId(CancellationToken cancellationToken,string authToken);
        Task<SpotifyPlaylistCollection> GetAllUserPlaylists(CancellationToken cancellationToken,string authToken, string nextPageURL = null);
        Task<SpotifyPlaylist> GetUserPlaylistById(CancellationToken cancellationToken,string authToken, string playlistId, string nextPageURL = null);
    }
}
