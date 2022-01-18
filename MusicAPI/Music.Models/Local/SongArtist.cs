using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Models.Local
{
    public class SongArtist
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public int ArtistId { get; set; }
    }
}
