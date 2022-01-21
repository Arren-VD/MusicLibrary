using AutoMapper;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Clients;
using Music.Spotify.Domain.Contracts.Services;
using Music.Spotify.Models;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Spotify.Domain.Services
{
    public class SpotifyTestService : IExternalService
    {
        private readonly IMapper _mapper;
        private readonly IClient _client;
        public SpotifyTestService(IMapper mapper, IClient client)
        {
            _mapper = mapper;
            _client = client;
        }
        public ExternalUserDTO ReturnClientUser(string spotifyToken)
        {
            return _mapper.Map<ClientUser>(_client.GetCurrentSpotifyUser(spotifyToken).Result);
        }
        public string GetName()
        {
            return "SpotifyTest";
        }
    }
}
