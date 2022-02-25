using Music.Spotify.Models.PlaylistModels;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.Spotify.Domain.Contracts.Services
{
    public interface ISpotifyTrackService
    {
        List<ExternalTrackDTO> GetAllUserTracksFromPlaylists(List<SpotifyPlaylist> playlistCollection, CancellationToken cancellationToken);
    }
}
