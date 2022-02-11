using Music.Domain.Contracts.Repositories;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public class UserTrackService : IUserTrackService
    {
        private readonly IGenericRepository<UserTrack> _userTrackRepository;

        public UserTrackService(IGenericRepository<UserTrack> userTrackRepository)
        {
            _userTrackRepository = userTrackRepository;
        }
        public UserTrack AddUserTrack(UserTrack userTrack)
        {
            if(_userTrackRepository.FindByConditionAsync(x => x.TrackId == userTrack.Id && x.UserId == userTrack.UserId) == null)
                return _userTrackRepository.Insert(userTrack);
            return null;
        }
        public UserTrack GetUserTrackByIDs(int userId, int trackId)
        {
            return _userTrackRepository.FindByConditionAsync(x => x.UserId == userId && x.TrackId == trackId);
        }
    }
}
