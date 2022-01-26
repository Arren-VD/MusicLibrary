using AutoMapper;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Clients;
using Music.Spotify.Domain.Contracts.Services;
using Music.Spotify.Models;
using Music.Spotify.Views;
using Music.Views;
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
        public SpotifyService(IMapper mapper, IClient client)
        {
            _mapper = mapper;
            _client = client;
        }
        public ExternalUserDTO ReturnClientUser(string spotifyToken)
        {
            return _mapper.Map<ExternalUserDTO>(_client.GetCurrentUser(spotifyToken).Result);
        }
        public string ReturnClientUserId(string spotifyToken)
        {
            return _client.GetCurrentUserId(spotifyToken);
        }
        public List<ClientTrackDTO> GetUserPlaylistCalledAll(string authToken)
        {
            var playlistCollection = new ClientPlaylistCollection();

            var playlist = new ClientPlaylist();
            var itemList = new List<ClientItem>();

            string playlistId = "";

            do
            {
                playlistCollection = null;
                playlistCollection = (_client.GetUserPlaylist(authToken, playlistCollection?.Next).Result);
                playlistId = playlistCollection.Items.FirstOrDefault(x => x.Name == "All").Id;
            } while (!String.IsNullOrEmpty(playlistCollection.Next) && String.IsNullOrEmpty(playlistId));
            
            do
            {
                playlist = _client.GetUserPlaylistCalledAll(authToken, playlistId, playlist?.Next).Result;
                itemList.AddRange(playlist.Items);
            } while (!String.IsNullOrEmpty(playlist.Next));

            List<ClientTrack> trackList = new List<ClientTrack>();
            itemList.ForEach(x => trackList.Add(x.Track));
            return _mapper.Map< List<ClientTrackDTO> >(trackList);
        }
        public string GetName()
        {
            return "Spotify";
        }
    }
}
