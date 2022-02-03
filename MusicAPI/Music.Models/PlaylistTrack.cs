using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Models
{
    public class PlaylistTrack
    {
        public PlaylistTrack(int playlistId, int trackId, int userId)
        {
            PlaylistId = playlistId;
            TrackId = trackId;
            UserId = userId;
        }

        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }
        public int UserId { get; set; }
        public Playlist Playlist { get; set; }
        public List<ClientPlayListTrack> ClientPlaylists { get; set; }

    }
}
