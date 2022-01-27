using AutoMapper;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Clients;
using Music.Spotify.Domain.Contracts.Services;
using Music.Spotify.Models;
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
                playlistCollection = (_client.GetAllUserPlaylists(authToken, playlistCollection?.Next).Result);
                playlistId = playlistCollection.Items.FirstOrDefault(x => x.Name == "All").Id;
            } while (!String.IsNullOrEmpty(playlistCollection.Next) && String.IsNullOrEmpty(playlistId));

            do
            {
                playlist = _client.GetUserPlaylistById(authToken, playlistId, playlist?.Next).Result;
                itemList.AddRange(playlist.Items);
            } while (!String.IsNullOrEmpty(playlist.Next));

            List<ClientTrack> trackList = new List<ClientTrack>();
            itemList.ForEach(x => trackList.Add(x.Track));
            return _mapper.Map<List<ClientTrackDTO>>(trackList);
        }
        public List<ClientTrackDTO> GetAllUserTrackswithCategory(string authToken)
        {
            //Get list of playlists without tracks from user
            var playlistCollection = new ClientPlaylistCollection();
            var playlistIDandNamess = new List<ClientIdAndName>();
            do
            {
                var newPlayListCollection = (_client.GetAllUserPlaylists(authToken, playlistCollection?.Next).Result);
                if(newPlayListCollection == playlistCollection)
                    playlistCollection = null;
                playlistCollection = newPlayListCollection;
                playlistCollection.Items.ForEach(x => {
                    if (x.Name != "All")
                        playlistIDandNamess.Add(new ClientIdAndName { Id =x.Id, Name = x.Name });
                });
            } while (!String.IsNullOrEmpty(playlistCollection.Next));
            //get each playlist with tracks
            var clientTrackDTOList = new List<ClientTrackDTO>();
            foreach (var playlistIdAndName in playlistIDandNamess)
            {
                var playlist = new ClientPlaylist();
                playlist = _client.GetUserPlaylistById(authToken, playlistIdAndName.Id, playlist?.Next).Result;

                //Foreach track within item of playlist, map to clienttrackdto and add to list
                playlist.Items.ForEach(x => {
                    var clientTrackDTO = _mapper.Map<ClientTrackDTO>(x.Track);
                var playlistIdentifier = new PlayListIdentifierDTO()
                {
                    Name = playlistIdAndName.Name,
                    Id = playlistIdAndName.Id
                };
                var existingClientTrack = clientTrackDTOList.FirstOrDefault(x => x.Id == clientTrackDTO.Id);

                if (existingClientTrack == null)
                    {
                        clientTrackDTO.PlaylistIdentifier.Add(playlistIdentifier);
                        clientTrackDTOList.Add(clientTrackDTO);
                    }
                else
                    {
                        existingClientTrack.PlaylistIdentifier.Add(playlistIdentifier);
                    }
                });
            }

            return clientTrackDTOList;
        }
        public string GetName()
        {
            return "Spotify";
        }
    }
}
