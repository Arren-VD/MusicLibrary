using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Models
{
    public class TrackArtist
    {
        public TrackArtist(int trackId, int artistId)
        {
            TrackId = trackId;
            ArtistId = artistId;
        }
        public TrackArtist()
        {

        }
        public int Id { get; set; }
        public int TrackId { get; set; }
        public int ArtistId { get; set; }

        public Artist Artist { get; set; }
    }
}
