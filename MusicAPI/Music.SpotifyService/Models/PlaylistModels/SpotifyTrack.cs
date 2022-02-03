using System.Collections.Generic;

namespace Music.Spotify.Models.PlaylistModels
{
    public class SpotifyTrack
    {
        public List<SpotifyArtist> artists { get; set; }
        public SpotifyExternalIds external_ids { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string preview_url { get; set; }
    }
}