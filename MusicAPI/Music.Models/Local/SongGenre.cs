using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Models.Local
{
    public class SongGenre
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public int GenreId { get; set; }
    }
}
