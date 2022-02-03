namespace Music.Spotify
{
    public class SpotifyOptions
    {
        public string URL { get; set; }
        public int? MaxPlaylists { get; set; }
        public int? MaxTracks { get; set; }
        public  string ServiceName { get { return "Spotify"; } }
    }
}