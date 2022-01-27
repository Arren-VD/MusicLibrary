using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Models
{
    public class TrackGenre
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public int GenreId { get; set; }
    }
}
