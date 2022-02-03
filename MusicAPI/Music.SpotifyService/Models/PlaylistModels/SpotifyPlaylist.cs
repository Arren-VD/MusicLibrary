using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Models.PlaylistModels
{
    public class SpotifyPlaylist
    {
        public SpotifyPlaylist()
        {
            items = new List<SpotifyTrackInfo>();
        }
        public List<SpotifyTrackInfo> items { get; set; }
        public string Next { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
