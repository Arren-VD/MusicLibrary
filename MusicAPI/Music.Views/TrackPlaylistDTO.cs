using System.Collections.Generic;

namespace Music.Views
{
    public class TrackPlaylistDTO
    {
        public int Id { get; set; }
        public List<ClientPlaylistDTO> ClientPlayList{get;set;}
        public string Name { get; set; }
    }
}