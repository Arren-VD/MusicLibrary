using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Views
{
    public class ClientTrackDTO
    {
        public List<ClientArtistDTO> Artists { get; set; }
        public int Duration_ms { get; set; }
        public ExternalUrlsDTO External_urls { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public string Preview_url { get; set; }
    }
}
