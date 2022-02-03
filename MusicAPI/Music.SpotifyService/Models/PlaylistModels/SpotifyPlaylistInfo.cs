namespace Music.Spotify.Models.PlaylistModels
{
    public class SpotifyPlaylistInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public SpotifyPlaylistOwner owner { get; set; }
    }
}