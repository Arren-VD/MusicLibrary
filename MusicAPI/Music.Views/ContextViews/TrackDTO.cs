using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Views
{
    public class TrackDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISRC_Id { get; set; }
    //    public List<ClientTrackDTO> ClientTrackInfo { get; set; }
        public List<PlaylistDTO> Playlists { get; set; }
        public List<ArtistDTO> Artists { get; set; }
    }
}
