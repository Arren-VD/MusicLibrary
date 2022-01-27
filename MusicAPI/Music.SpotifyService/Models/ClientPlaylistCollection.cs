using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Models
{
    public class ClientPlaylistCollection
    {
        public List<ClientPlaylist> Items { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public int Total { get; set; }

    }
}
