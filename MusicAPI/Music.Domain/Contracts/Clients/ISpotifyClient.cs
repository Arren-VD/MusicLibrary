using Music.Models.SpotifyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Clients
{
    public interface ISpotifyClient
    {
        public  Task<SpotifyUser> GetCurrentSpotifyUser(string authToken);
    }
}
