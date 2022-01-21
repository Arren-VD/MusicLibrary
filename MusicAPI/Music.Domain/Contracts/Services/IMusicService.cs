using Music.Models;
using Music.Spotify.Domain.Contracts.Services;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Services
{
    public interface IMusicService
    {
        public UserClientDTO LinkUsers(TokenDTO userToken, int userId);
        string GetName();
    }
}
