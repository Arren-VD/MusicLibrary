using Music.Models;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Services
{
    public interface IPlaylistService
    {
        Playlist AddPlaylist(ExternalPlaylistDTO externalPlaylist, int userId, int trackId, string clientServiceName);
    }
}
