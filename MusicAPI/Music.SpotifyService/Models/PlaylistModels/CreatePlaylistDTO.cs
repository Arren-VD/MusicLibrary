using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Models.PlaylistModels
{
    public class CreatePlaylistDTO
    {
        public CreatePlaylistDTO(string name, string description, bool @public)
        {
            Name = name;
            Description = description;
            Public = @public;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool @Public { get; set; }
    }
}
