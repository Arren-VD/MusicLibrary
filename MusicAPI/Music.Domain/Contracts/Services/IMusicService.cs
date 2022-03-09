using Music.Domain.Services.Helpers;
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
        Task<List<TrackDTO>> ImportClientMusicToDB(int userId, List<UserTokenDTO> userTokens, CancellationToken cancellationToken);
        Task<PagingWrapper<TrackDTO>> GetAllTracksWithPlaylistAndArtist( int userId, List<int> playlistIds, int page, int pageSize, CancellationToken cancellationToken);
        
    }
}
