using System;
using System.Collections.Generic;

namespace Music.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISRC_Id { get; set; }

        public List<PlaylistTrack> PlaylistTracks { get; set; }
        public List<TrackArtist> TrackArtists { get; set; }
        public List<UserTrack> UserTracks { get; set; }
    }
}
