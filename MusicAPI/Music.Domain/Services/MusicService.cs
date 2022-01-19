using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.Exceptions;
using Music.Domain.Validators;
using Music.Models;
using Music.Spotify.Domain.Contracts.Services;
using Music.Spotify.Domain.Services;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public class MusicService : IMusicService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IService _service;
        private readonly IUserTokensRepository _tokensRepository;
        public MusicService(IMapper mapper, IUserRepository userRepository, IService service,IUserTokensRepository tokensRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _service = service;
            _tokensRepository = tokensRepository;
        }
        public string GetName()
        {
            return _service.GetName();
        }
        public UserClientDTO LinkUsers(TokenDTO userToken, int userId)
        {
            var clientId = _service.ReturnClientUser(userToken.Value).Id;

            var userClient = _mapper.Map<UserClientDTO>(_tokensRepository.AddTokenById(new UserClient(clientId, userToken.Name,userId)));

            _tokensRepository.SaveChanges();
            return userClient;
        }
        /*
        public SpotifyUserDTO GetSpotifyUser(string spotifyToken)
        {
            SpotifyUser spotifyUser = _spotifyClient.GetCurrentSpotifyUser(spotifyToken).Result;

            SpotifyUserDTO spotifyUserDTO = _mapper.Map<SpotifyUserDTO>(spotifyUser);

            return spotifyUserDTO;
        }*/
    }
}
