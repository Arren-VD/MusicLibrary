using Music.Views;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Services
{
    public interface IMusicService
    {
        List<TrackDTO> ImportClientMusicToDB(int userId, List<UserTokenDTO> userTokens);
    }
}
