using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Models.PlaylistModels
{
    public class Playlist
    {
        public Playlist()
        {
            items = new List<TrackInfo>();
        }
        public List<TrackInfo> items { get; set; }
        public string Next { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
