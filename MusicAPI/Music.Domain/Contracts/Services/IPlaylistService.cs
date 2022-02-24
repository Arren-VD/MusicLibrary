using Music.Models;
using Music.Views;
using Music.Views.ClientViews;
using Music.Views.GlobalViews;
using Music.Views.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Services
{
    public interface IPlaylistService
    {
        Task<Playlist> AddPlaylist(CancellationToken cancellationToken, NameDTO<string> externalPlaylist, int userId, int trackId, string clientServiceName);
        Task<List<Playlist>> AddPlaylistCollection(CancellationToken cancellationToken, List<NameDTO<string>> playlistCollection, int userId, int trackId, string clientServiceName);
        Task<List<PlaylistDTO>> GetAllUserPlaylists(CancellationToken cancellationToken, int userId);
        Task<Playlist> AddPlaylistToUserTrack(int userId, int trackId, PlaylistInput playlistInput, CancellationToken cancellationToken);
    }
}
