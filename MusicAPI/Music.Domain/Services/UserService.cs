using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.Exceptions;
using Music.Domain.Validators;
using Music.Models;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEnumerable<IMusicService> _musicServices;
        public UserService(IMapper mapper, IUserRepository userRepository, IEnumerable<IMusicService> musicServices)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _musicServices = musicServices;
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
            //validate if user doesnt have requested link
            List<UserClientDTO> userClientDTOs = new List<UserClientDTO>();
            spotifyTokens.ForEach(st => userClientDTOs.Add(_musicServices.FirstOrDefault(ms => ms.GetName() == st.Name).LinkUsers(st, userId)));

            return userClientDTOs;
        }
    }
}
