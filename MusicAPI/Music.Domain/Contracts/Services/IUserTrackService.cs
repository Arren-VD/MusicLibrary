using Music.Domain.Contracts.Repositories;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public interface IUserTrackService
    {
        public UserTrack AddUserTrack(UserTrack userTrack);
        UserTrack GetUserTrackByIDs(int userId, int trackId);
    }
}
