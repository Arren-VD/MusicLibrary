using System.Collections.Generic;

namespace Music.Spotify.Models.PlaylistModels
{
    public class ClientTrack
    {
        public List<ClientArtist> artists { get; set; }
        public ExternalIds external_ids { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string preview_url { get; set; }
    }
}