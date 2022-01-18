using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models.Local
{
    public class User
    {
        public User()
        {

        }
        public User(int id, string name, string spotifyId)
        {
            Id = id;
            Name = name;
            SpotifyId = spotifyId;
        }
        public int Id { get; set; }
        public string SpotifyId { get; set; }

        public string Name { get; set; }
    }
}
