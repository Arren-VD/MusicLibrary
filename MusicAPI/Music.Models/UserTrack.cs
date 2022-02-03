using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
    public class UserTrack
    {
        public UserTrack(int trackId, int userId)
        {
            TrackId = trackId;
            UserId = userId;
        }

        public int Id { get; set; }
        public int TrackId { get; set; }
        public int UserId { get; set; }
        public Track Track { get; set; }
        public List<ClientUserTrack> ClientTracks { get; set; }

    }
}
