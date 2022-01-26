using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Models
{
    public class ClientTrackCollection
    {
        public string Next { get; set; }
        public ClientTrack[] Items { get; set; }

    }
}
