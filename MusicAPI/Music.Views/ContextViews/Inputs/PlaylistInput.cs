using Music.Views.GlobalViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Views.Inputs
{
    public  class PlaylistInput
    {
        public int? Id { get; set; }
        public List<ClientDTO> ClientPlaylists { get; set; }
        public string Name { get; set; }
    }
}
