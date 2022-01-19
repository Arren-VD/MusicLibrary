using Music.Spotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Domain.Contracts.Services
{
    public  interface IService
    {
        string GetName();
        ClientUser ReturnClientUser(string spotifyToken);
    }
}
