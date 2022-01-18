using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models.SpotifyModels
{

    public class SpotifyUser
    {
        public SpotifyUser() 
        { 

        }
        public SpotifyUser(string id, string country, string email, string displayName) 
        {
            Id = id;
            Country = country;
            Email = email;
            Display_Name = displayName;
        }
        public string Id { get; set; }
        public string Country { get; set; }

        public string Email { get; set; }
        public string Display_Name { get; set; }
    }
}
