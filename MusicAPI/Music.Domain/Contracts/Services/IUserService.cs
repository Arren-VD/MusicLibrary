using Music.Domain.ErrorHandling;
using Music.Models;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Services
{
    public interface IUserService
    {
        Task<ValidationResult<UserDTO>> CreateUser(UserCreationDTO user, CancellationToken cancellationToken);

        Task<UserDTO> Login(LoginDTO user, CancellationToken cancellationToken);

        Task<List<UserClientDTO>> LinkUserToExternalAPIs(int userId, List<UserTokenDTO> spotifyTokens, CancellationToken cancellationToken);

        Task<UserDTO> GetUserById(int userId, CancellationToken cancellationToken);

    }
}
