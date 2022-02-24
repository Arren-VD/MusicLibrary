using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.ErrorHandling;
using Music.Domain.ErrorHandling.Validations;
using Music.Domain.Exceptions;
using Music.Models;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<ValidationResult<UserDTO>> CreateUser(UserCreationDTO user, CancellationToken cancellationToken)
        {
            var result = new ValidationResult<UserDTO>();
            result.Errors.AddRange(_userValidation.Validate(user));
            if (result.Errors.Any())
                return result;

            if (await _userRepository.GetUserByName(user.Name) != null)
                result.AddError(Error.ErrorValues.AlreadyExists,"User","Name",user.Name);

            var userToAdd = _mapper.Map<User>(user);

            var addedUser = await _userRepository.AddUser(userToAdd);
            _userRepository.SaveChanges();
            result.Value = _mapper.Map<UserDTO>(await _userRepository.GetUser(addedUser));
            return result;
        }
        public async Task<UserDTO> Login(LoginDTO user, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDTO>(await _userRepository.GetUserByName(user.Name));
        }

        public async Task<List<UserClientDTO>> LinkUserToExternalAPIs(int userId,List<UserTokenDTO> userTokens, CancellationToken cancellationToken)
        {
            List<UserClientDTO> userclients = new List<UserClientDTO>();
            foreach (var userToken in userTokens)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var svc =  _externalServices.FirstOrDefault(ms => ms.GetName() == userToken.Name);
                var clientId = await svc.ReturnClientUserId(userToken.Value,cancellationToken);
                var a = await _userTokensRepository.AddTokenById(new UserClient(clientId, userToken.Name, userId));
                userclients.Add(_mapper.Map<UserClientDTO>(await _userTokensRepository.AddTokenById(new UserClient(clientId, userToken.Name, userId))));

                _userTokensRepository.SaveChanges();
            }

            return userclients;
        }
        public async Task<UserDTO> GetUserById(int userId, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDTO>(await _userRepository.GetUserById(userId));
        }
    }
}
