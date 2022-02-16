using Music.Models;
using Music.Views.ClientViews;
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
        Task<Playlist> AddPlaylist(CancellationToken cancellationToken,ExternalPlaylistDTO externalPlaylist, int userId, int trackId, string clientServiceName);
        Task<List<Playlist>> AddPlaylistCollection(CancellationToken cancellationToken, List<ExternalPlaylistDTO> playlistCollection, int userId, int trackId, string clientServiceName);
    }
}
