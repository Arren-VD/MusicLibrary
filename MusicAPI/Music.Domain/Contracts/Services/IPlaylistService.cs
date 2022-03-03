using Music.Models;
using Music.Views;
using Music.Views.ClientViews;
using Music.Views.Results;
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
        Task<Playlist> AddPlaylist(ExternalPlaylistDTO externalPlaylist, int userId, int trackId, string clientServiceName, CancellationToken cancellationToken);
        Task<List<Playlist>> AddPlaylistCollection( List<ExternalPlaylistDTO> playlistCollection, int userId, int trackId, string clientServiceName, CancellationToken cancellationToken);
        Task<List<PlaylistResult>> GetAllUserPlaylists( int userId, CancellationToken cancellationToken);
    }
}
