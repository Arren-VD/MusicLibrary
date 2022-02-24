namespace Music.Models
{
    public class ClientPlayListTrack
    {
        public ClientPlayListTrack(string clientPlaylistId, string clientServiceName, int playlistTrackId)
        {
            ClientPlaylistId = clientPlaylistId;
            ClientServiceName = clientServiceName;
            PlaylistTrackId = playlistTrackId;
        }

        public string ClientPlaylistId { get; set; }
        public string ClientServiceName { get; set; }
        public int Id { get; set; }
        public int PlaylistTrackId { get; set; }
    }
}