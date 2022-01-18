using AutoMapper;
using Music.Domain.Contracts.Clients;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.Exceptions;
using Music.Domain.Validators;
using Music.Models.Local;
using Music.Models.SpotifyModels;
using Music.Views;
using Music.Views.LocalDTOs;
using Music.Views.SpotifyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public class UserService : IUserService
    {
        ISpotifyClient _spotifyClient;
        IMapper _mapper;
        IUserRepository _userRepository;
        public UserService(ISpotifyClient spotifyClient, IMapper mapper, IUserRepository userRepository)
        {
            _spotifyClient = spotifyClient;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public UserDTO LinkUserToSpotify(string spotifyToken, int userid)
        {
            var spotifyUser = _mapper.Map<SpotifyUser>(_spotifyClient.GetCurrentSpotifyUser(spotifyToken).Result);
            var user = _mapper.Map<User>(_userRepository.GetUserById(userid));

            user.SpotifyId = spotifyUser.Id;

            var updatedUser = _mapper.Map<UserDTO>(_userRepository.UpdateUser(user));

            _userRepository.SaveChanges();
            return updatedUser;
        }
        public SpotifyUserDTO GetSpotifyUser(string spotifyToken)
        {
            SpotifyUser spotifyUser = _spotifyClient.GetCurrentSpotifyUser(spotifyToken).Result;

            SpotifyUserDTO spotifyUserDTO = _mapper.Map<SpotifyUserDTO>(spotifyUser);

            return spotifyUserDTO;
        }
        public UserDTO CreateUser(UserCreationDTO user)
        {
            if (_userRepository.GetUserByName(user.Name) != null)
                 throw new EntityAlreadyExistsException(nameof(user), typeof(User), user.Name);

             var userToAdd = _mapper.Map<User>(user);
             var addedUser = _userRepository.AddUser(userToAdd);

             _userRepository.SaveChanges();

             return _mapper.Map<UserDTO>(_userRepository.GetUser(addedUser));
            return _mapper.Map<UserDTO>(_userRepository.GetUser(_mapper.Map<User>(user))); 
        }
        public UserDTO Login(LoginDTO user)
        {
            return _mapper.Map<UserDTO>(_userRepository.GetUserByName(user.Name));
        }
    }
}
