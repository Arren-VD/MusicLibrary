using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Views.ClientViews
{
    public class ExternalTrackDTO
    {
        public ExternalTrackDTO()
        {
            Artists = new List<ExternalArtistDTO>();
            Playlists = new List<ExternalPlaylistDTO>();
        }

        public List<ExternalArtistDTO> Artists { get; set; }
        public string ISRC_Id { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Preview_url { get; set; }
        public string ClientServiceName { get; set; }
        public List<ExternalPlaylistDTO> Playlists { get; set; }
    }
}
