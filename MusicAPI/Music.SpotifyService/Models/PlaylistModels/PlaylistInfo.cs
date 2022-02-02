namespace Music.Spotify.Models.PlaylistModels
{
    public class PlaylistInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public PlaylistOwner owner { get; set; }
    }
}