namespace Music.Views.ClientViews
{
    public class ClientTrackDTO
    {
        public int Id { get; set; }
        public int UserTrackId { get; set; }
        public string ClientServiceName { get; set; }
        public string ClientTrackId { get; set; }
        public string Preview_url { get; set; }
    }
}