using Music.Views;
using Music.Views.ClientViews;
using Music.Views.GlobalViews;
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
        Task<ExternalUserDTO> ReturnClientUser(CancellationToken cancellationToken,string spotifyToken);

        Task<string> ReturnClientUserId(CancellationToken cancellationToken,string spotifyToken);
        Task<List<ExternalTrackOutput>> GetCurrentUserTracksWithPlaylistAndArtist(CancellationToken cancellationToken,string authToken);
        Task<NameDTO<string>> UpsertPlaylist(string authToken, int userId, NameDTO<string> clientPlaylist, CancellationToken cancellationToken);
    }
}
