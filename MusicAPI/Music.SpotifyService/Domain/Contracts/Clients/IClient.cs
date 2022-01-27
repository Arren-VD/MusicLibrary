using Music.Spotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Domain.Contracts
{
    public interface IClient
    {
        Task<ClientUser> GetCurrentUser(string authToken);
        string GetCurrentUserId(string authToken);

        Task<ClientPlaylist> GetUserPlaylistById(string authToken, string playlistId, string nextPageURL = null);

        Task<ClientPlaylistCollection> GetAllUserPlaylists(string authToken, string nextPageURL = null);
    }
}
