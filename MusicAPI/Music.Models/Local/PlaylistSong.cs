using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Models.Local
{
    public class PlaylistSong
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
    }
}
