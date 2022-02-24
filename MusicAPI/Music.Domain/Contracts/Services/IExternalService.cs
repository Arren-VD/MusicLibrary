using Music.Views;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Music.Domain.Contracts.Services
{
    public  interface IExternalService
    {
        string GetName();
        Task<ExternalUserDTO> ReturnClientUser(string spotifyToken, CancellationToken cancellationToken);

        Task<string> ReturnClientUserId(string spotifyToken, CancellationToken cancellationToken);
        Task<List<ExternalTrackDTO>> GetCurrentUserTracksWithPlaylistAndArtist(string authToken, CancellationToken cancellationToken);
    }
}
