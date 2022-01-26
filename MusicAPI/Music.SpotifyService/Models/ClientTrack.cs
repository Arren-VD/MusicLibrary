using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Models
{

    public class ClientTrack
    {
        public List<ClientArtist> Artists { get; set; }
        public int Duration_ms { get; set; }
        public ExternalUrls External_urls { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public string Preview_url { get; set; }

    }
}
