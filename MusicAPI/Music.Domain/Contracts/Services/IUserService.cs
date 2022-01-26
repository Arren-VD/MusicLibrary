using Music.Domain.ErrorHandling;
using Music.Models;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Services
{
    public interface IUserService
    {
        //UserDTO LinkUserToSpotify(string spotifyToken, int userid);

        //SpotifyUserDTO GetSpotifyUser(string spotifyToken);

        ValidationResult<UserDTO> CreateUser(UserCreationDTO user);

        UserDTO Login(LoginDTO user);

        List<UserClientDTO> LinkUserToExternalAPIs(int userId, List<UserTokenDTO> spotifyTokens);

    }
}
