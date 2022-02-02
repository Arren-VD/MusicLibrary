using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Views.ClientViews
{
    public class ClientTrackDTO
    {
        public ClientTrackDTO()
        {
            Artists = new List<ClientArtistDTO>();
            Playlists = new List<ClientPlaylistDTO>();
        }
        public List<ClientArtistDTO> Artists { get; set; }
        public string ISRC_Id { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Preview_url { get; set; }
        public List<ClientPlaylistDTO> Playlists { get; set; }
    }
}
