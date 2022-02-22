using System.Collections.Generic;

namespace Music.Views
{
    public class TrackCollectionDTO
    {
        public List<TrackDTO> Tracks{get;set;}
        public int TotalPages { get; set; }
    }
}