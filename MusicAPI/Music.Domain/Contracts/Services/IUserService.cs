using Music.Models.Local;
using Music.Views.LocalDTOs;
using Music.Views.SpotifyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Services
{
    public interface IUserService
    {
        UserDTO LinkUserToSpotify(string spotifyToken, int userid);

        SpotifyUserDTO GetSpotifyUser(string spotifyToken);

        UserDTO CreateUser(UserCreationDTO user);

        UserDTO Login(LoginDTO user);

    }
}
