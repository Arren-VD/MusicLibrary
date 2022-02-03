using AutoMapper;
using Microsoft.Extensions.Options;
using Music.Domain.Contracts.Services;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Clients;
using Music.Spotify.Domain.Contracts.Services;
using Music.Spotify.Models;
using Music.Views;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Music.Spotify.Domain.Services
{
    public class SpotifyService : IExternalService
    {
        private readonly IMapper _mapper;
        private readonly IClient _client;
        private readonly PlaylistHelper _playlistHelper;
        private readonly SpotifyOptions _options;
        public SpotifyService(IMapper mapper, IClient client, PlaylistHelper playlistHelper, IOptions<SpotifyOptions> options)
        {
            _options = options.Value;
            _playlistHelper = playlistHelper;
            _mapper = mapper;
            _client = client;
        }
        public ExternalUserDTO ReturnClientUser(string spotifyToken)
        {
            return _mapper.Map<ExternalUserDTO>(_client.GetCurrentClientUser(spotifyToken).Result);
        }
        public string ReturnClientUserId(string spotifyToken)
        {
            return _client.GetCurrentClientUserId(spotifyToken);
        }
        public List<ExternalTrackDTO> GetCurrentUserTracksWithPlaylistAndArtist(string authToken)
        {
            var userName = _client.GetCurrentClientUser(authToken).Result.Display_Name;
            var userPlaylists = _playlistHelper.GetAllUserPlaylists(authToken, userName);
            var trackList = _playlistHelper.GetAllUserTracksFromPlaylists(userPlaylists);

            return trackList;
        }
        public string GetName()
        {
            return _options.ServiceName;
        }
    }
}
