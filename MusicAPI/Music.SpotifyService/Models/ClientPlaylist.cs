using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Models
{
    public class ClientPlaylist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Next { get; set; }
        public ClientItem[] Items { get; set; }
    }
}
