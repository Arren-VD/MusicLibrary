using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.Exceptions;
using Music.Domain.Validators;
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
        private readonly UserValidators _userValidator;

        public UserService(IMapper mapper, IUserRepository userRepository, IEnumerable<IExternalService> externalServices, IUserTokensRepository userTokensRepository, UserValidators userValidator)
        {

            _userValidator = userValidator;
            _mapper = mapper;
            _userRepository = userRepository;
            _externalServices = externalServices;
            _userTokensRepository = userTokensRepository;  
        }
        public UserDTO CreateUser(UserCreationDTO user)
        {
            if (_userRepository.GetUserByName(user.Name) != null)
                throw new EntityAlreadyExistsException(nameof(user), typeof(User), user.Name);
            var userToAdd = _mapper.Map<User>(user);

            _userRepository.AddUser(userToAdd);


            _userRepository.SaveChanges();

            return _mapper.Map<UserDTO>(_userRepository.GetUser(_mapper.Map<User>(user)));
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
                var clientId = svc.ReturnClientUser(userToken.Value).Id;
                userclients.Add(_mapper.Map<UserClientDTO>(_userTokensRepository.AddTokenById(new UserClient(clientId, userToken.Name, userId))));

                _userTokensRepository.SaveChanges();
            }

            return userclients;
        }
    }
}
