using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
    public class ClientTrackLink
    {
        public int Id { get; set; }
        public int TrackId { get; set; }
        public int ClientTrackMeta { get; set; }
    }
}
