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
using System.Threading;
using System.Threading.Tasks;

namespace Music.Spotify.Domain.Services
{
    public class SpotifyService : IExternalService
    {
        private readonly IMapper _mapper;
        private readonly IClient _client;
        private readonly ISpotifyPlaylistService _spotifyPlaylistSvc;
        private readonly ISpotifyTrackService _spotifyTrackSvc;
        private readonly SpotifyOptions _options;
        public SpotifyService(IMapper mapper, IClient client, IOptions<SpotifyOptions> options, ISpotifyPlaylistService spotifyPlaylistSvc, ISpotifyTrackService spotifyTrackSvc)
        {
            _options = options.Value;
            _mapper = mapper;
            _client = client;
            _spotifyPlaylistSvc = spotifyPlaylistSvc;
            _spotifyTrackSvc = spotifyTrackSvc;
    }
        public async Task<ExternalUserDTO> ReturnClientUser(string spotifyToken, CancellationToken cancellationToken)
        {
            return  _mapper.Map<ExternalUserDTO>(await _client.GetCurrentClientUser(spotifyToken, cancellationToken));
        }
        public async Task<string> ReturnClientUserId(string spotifyToken, CancellationToken cancellationToken)
        {
            return await _client.GetCurrentClientUserId(spotifyToken, cancellationToken);
        }
        public async Task<List<ExternalTrackDTO>> GetCurrentUserTracksWithPlaylistAndArtist(string authToken, CancellationToken cancellationToken)
        {
            var spotifyUser = (await _client.GetCurrentClientUser(authToken, cancellationToken));
            var playlistSummary = _spotifyPlaylistSvc.GetPlaylistSummary(authToken,spotifyUser, cancellationToken);
            var userPlaylists = _spotifyPlaylistSvc.GetUserPlaylistCollectionWithTracks(authToken, playlistSummary, cancellationToken);
            var trackList = _spotifyTrackSvc.GetAllUserTracksFromPlaylists(userPlaylists, cancellationToken);

            return trackList;
        }
        public string GetName()
        {
            return  _options.ServiceName;
        }
    }
}
