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
        Task<ValidationResult<UserDTO>> CreateUser(CancellationToken cancellationToken,UserCreationDTO user);

        Task<UserDTO> Login(CancellationToken cancellationToken,LoginDTO user);

        Task<List<UserClientDTO>> LinkUserToExternalAPIs(CancellationToken cancellationToken,int userId, List<UserTokenDTO> spotifyTokens);

        Task<UserDTO> GetUserById(CancellationToken cancellationToken,int userId);

    }
}
