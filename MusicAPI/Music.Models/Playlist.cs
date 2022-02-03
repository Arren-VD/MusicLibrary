using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
