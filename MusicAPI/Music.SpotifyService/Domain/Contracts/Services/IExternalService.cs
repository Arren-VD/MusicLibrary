using Music.Spotify.Models;
using Music.Views;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Music.Spotify.Domain.Contracts.Services
{
    public  interface IExternalService
    {
        string GetName();
        ExternalUserDTO ReturnClientUser(string spotifyToken);

        string ReturnClientUserId(string spotifyToken);
        List<ClientTrackDTO> GetCurrentUserTracksWithPlaylistAndArtist(string authToken);
    }
}
