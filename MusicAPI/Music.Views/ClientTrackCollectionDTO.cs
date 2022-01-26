using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Views
{
    public class ClientTrackCollectionDTO
    {
        public string Next { get; set; }
        public ClientTrackDTO[] Items { get; set; }
    }
}
