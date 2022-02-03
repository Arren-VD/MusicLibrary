using Music.Spotify.Models;
using Music.Spotify.Models.PlaylistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Domain.Contracts
{
    public interface IClient
    {
        Task<SpotifyUser> GetCurrentClientUser(string authToken);
        string GetCurrentClientUserId(string authToken);
        Task<SpotifyPlaylistCollection> GetAllUserPlaylists(string authToken, string nextPageURL = null);
        Task<SpotifyPlaylist> GetUserPlaylistById(string authToken, string playlistId, string nextPageURL = null);
    }
}
