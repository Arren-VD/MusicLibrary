namespace Music.Spotify.Models.PlaylistModels
{
    public class SpotifyPlaylistSummary
    {
        public string id { get; set; }
        public string name { get; set; }
        public SpotifyPlaylistOwner owner { get; set; }
    }
}