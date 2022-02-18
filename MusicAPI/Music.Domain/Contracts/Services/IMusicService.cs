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
    public interface IMusicService
    {
        Task<List<TrackDTO>> ImportClientMusicToDB(CancellationToken cancellationToken,int userId, List<UserTokenDTO> userTokens);
        Task<List<TrackDTO>> GetAllTracksWithPlaylistAndArtist(CancellationToken cancellationToken, int userId, List<UserTokenDTO> userTokens);
        
    }
}
