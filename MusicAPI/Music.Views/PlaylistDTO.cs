namespace Music.Views
{
    public class PlaylistDTO
    {
        public int Id { get; set; }
        public ClientPlaylistDTO ClientPlayList{get;set;}
        public string Name { get; set; }
    }
}