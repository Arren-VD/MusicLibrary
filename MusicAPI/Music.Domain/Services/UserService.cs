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

        public UserService(IMapper mapper, IUserRepository userRepository, IEnumerable<IExternalService> externalServices, IUserTokensRepository userTokensRepository)
        {
           

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
            var addedUser = _userRepository.AddUser(userToAdd);

            _userRepository.SaveChanges();

            return _mapper.Map<UserDTO>(_userRepository.GetUser(_mapper.Map<User>(user)));
        }
        public UserDTO Login(LoginDTO user)
        {
            return _mapper.Map<UserDTO>(_userRepository.GetUserByName(user.Name));
        }

        public List<UserClientDTO> LinkUserToExternalAPIs(int userId,List<TokenDTO> spotifyTokens)
        {
            foreach (var item in spotifyTokens)
            {
                var svc = _externalServices.FirstOrDefault(ms => ms.GetName() == item.Name);
                var clientId = svc.ReturnClientUser(item.Value).Id;
                var userClient = _mapper.Map<UserClientDTO>(_userTokensRepository.AddTokenById(new UserClient(clientId, userToken.Name, userId)));
            }


            var userClient = _mapper.Map<UserClientDTO>(_tokensRepository.AddTokenById(new UserClient(clientId, userToken.Name, userId)));

            _tokensRepository.SaveChanges();
            return userClient;
            //validate if user doesnt have requested link
            List<UserClientDTO> userClientDTOs = new List<UserClientDTO>();
            spotifyTokens.ForEach(st => userClientDTOs.Add(_externalServices.FirstOrDefault(ms => ms.GetName() == st.Name).LinkUsers(st, userId)));

            return userClientDTOs;
        }
    }
}
