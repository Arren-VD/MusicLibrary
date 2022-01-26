using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.ErrorHandling;
using Music.Domain.ErrorHandling.Validations;
using Music.Domain.Exceptions;
using Music.Models;
using Music.Spotify.Domain.Contracts.Services;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Music.Domain.IServiceCollectionExtensions;

namespace Music.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEnumerable<IExternalService> _externalServices;
        private readonly IUserTokensRepository _userTokensRepository;
        private readonly UserCreationValidator _userValidation;

        public UserService(IMapper mapper, IUserRepository userRepository, IEnumerable<IExternalService> externalServices, IUserTokensRepository userTokensRepository, UserCreationValidator userValidation)
        {
            _userValidation = userValidation;
            _mapper = mapper;
            _userRepository = userRepository;
            _externalServices = externalServices;
            _userTokensRepository = userTokensRepository;  
        }
        public ValidationResult<UserDTO> CreateUser(UserCreationDTO user)
        {
            var result = new ValidationResult<UserDTO>();
            if (_userRepository.GetUserByName(user.Name) != null)
                result.AddError(Error.ErrorValues.AlreadyExists,"User","Name",user.Name);

            result.Errors.AddRange(_userValidation.Validate(user));
            if (result.Errors.Any())
                return result;

            var userToAdd = _mapper.Map<User>(user);

            var addedUser = _userRepository.AddUser(userToAdd);
            _userRepository.SaveChanges();

            var a = _userRepository.GetUser(addedUser);
            result.Value = _mapper.Map<UserDTO>(_userRepository.GetUser(addedUser));
            return result;
        }
        public UserDTO Login(LoginDTO user)
        {
            return _mapper.Map<UserDTO>(_userRepository.GetUserByName(user.Name));
        }

        public List<UserClientDTO> LinkUserToExternalAPIs(int userId,List<UserTokenDTO> userTokens)
        {
            List<UserClientDTO> userclients = new List<UserClientDTO>();
            foreach (var userToken in userTokens)
            {
                var svc = _externalServices.FirstOrDefault(ms => ms.GetName() == userToken.Name);
                var clientId = svc.ReturnClientUserId(userToken.Value);
                var a = _userTokensRepository.AddTokenById(new UserClient(clientId, userToken.Name, userId));
                userclients.Add(_mapper.Map<UserClientDTO>(_userTokensRepository.AddTokenById(new UserClient(clientId, userToken.Name, userId))));

                _userTokensRepository.SaveChanges();
            }

            return userclients;
        }
    }
}
