using AutoMapper;
using Microsoft.Extensions.Options;
using Music.Domain.Contracts.Services;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Clients;
using Music.Spotify.Domain.Contracts.Services;
using Music.Spotify.Models;
using Music.Spotify.Models.PlaylistModels;
using Music.Views;
using Music.Views.ClientViews;
using Music.Views.GlobalViews;
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
        private readonly PlaylistHelper _playlistHelper;
        private readonly SpotifyOptions _options;
        public SpotifyService(IMapper mapper, IClient client, PlaylistHelper playlistHelper, IOptions<SpotifyOptions> options)
        {
            _options = options.Value;
            _playlistHelper = playlistHelper;
            _mapper = mapper;
            _client = client;
        }
        public async Task<ExternalUserDTO> ReturnClientUser(CancellationToken cancellationToken,string spotifyToken)
        {
            return  _mapper.Map<ExternalUserDTO>(await _client.GetCurrentClientUser(cancellationToken,spotifyToken));
        }
        public async Task<string> ReturnClientUserId(CancellationToken cancellationToken,string spotifyToken)
        {
            return await _client.GetCurrentClientUserId(cancellationToken,spotifyToken);
        }
        public async Task<List<ExternalTrackOutput>> GetCurrentUserTracksWithPlaylistAndArtist(CancellationToken cancellationToken,string authToken)
        {
            var user = (await _client.GetCurrentClientUser(cancellationToken,authToken));
            var userPlaylists = _playlistHelper.GetAllUserPlaylists(cancellationToken,authToken, user.Display_Name,user.Id);
            var trackList = _playlistHelper.GetAllUserTracksFromPlaylists(cancellationToken,userPlaylists);

            return trackList;
        }
        public async Task<NameDTO<string>> UpsertPlaylist(string authToken,int userId, NameDTO<string> clientPlaylist, CancellationToken cancellationToken)
        {
            var user = (await _client.GetCurrentClientUser(cancellationToken, authToken));
            var userPlaylists = _playlistHelper.GetAllUserPlaylists(cancellationToken, authToken, user.Display_Name, user.Id);
            var existingPlaylist = userPlaylists.FirstOrDefault(x => x.Name == clientPlaylist.Name || clientPlaylist.Id == x.Id) ;

            if (existingPlaylist == null)
            {
                var result = await _client.AddPlaylist(user.Id, authToken, new CreatePlaylistDTO(clientPlaylist.Name, null, false), cancellationToken);
                return new NameDTO<string> { Name = result.Name, Id = result.Id };
            }
            return new NameDTO<string> { Name = existingPlaylist.Name, Id = existingPlaylist.Id };
        }
        public string GetName()
        {
            return  _options.ServiceName;
        }
    }
}
