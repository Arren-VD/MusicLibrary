namespace Music.Models
{
    public class ClientPlayListTrack
    {
        public ClientPlayListTrack(string clientId, string clientServiceName, int playlistTrackId)
        {
            ClientId = clientId;
            ClientServiceName = clientServiceName;
            PlaylistTrackId = playlistTrackId;
        }

        public string ClientId { get; set; }
        public string ClientServiceName { get; set; }
        public int Id { get; set; }
        public int PlaylistTrackId { get; set; }
    }
}