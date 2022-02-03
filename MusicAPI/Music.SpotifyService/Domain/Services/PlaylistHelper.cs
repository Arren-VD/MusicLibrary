using AutoMapper;
using Microsoft.Extensions.Options;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Models.PlaylistModels;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Domain.Services
{
    public class PlaylistHelper
    {
        private readonly IMapper _mapper;
        private readonly IClient _client;
        private readonly SpotifyOptions _options;
        public PlaylistHelper(IMapper mapper, IClient client, IOptions<SpotifyOptions> options)
        {
            _options = options.Value;
            _mapper = mapper;
            _client = client;
        }
        public List<SpotifyPlaylist> GetAllUserPlaylists(string authToken, string userName)
        {
            var playlistInfoCollection = GetUserPlaylistInfoCollection(authToken, userName);
            var playlistCollection = GetUserPlaylistWithTracks(authToken, playlistInfoCollection);
            return playlistCollection;
        }
        public List<ExternalTrackDTO> GetAllUserTracksFromPlaylists(List<SpotifyPlaylist> playlistCollection)
        {
            var trackList = new List<ExternalTrackDTO>();
            playlistCollection.ForEach(playlist => playlist.items.ForEach(item =>
            {
                //Check if track is already in list, if it is add playlist to that item.
                var existingTrack = trackList.FirstOrDefault(x => x.ISRC_Id == item.track.external_ids.isrc);
                if (existingTrack == null)
                {
                    var track = _mapper.Map<ExternalTrackDTO>(item.track);
                    track.Playlists.Add(new ExternalPlaylistDTO() { Id = playlist.Id, Name = playlist.Name });
                    track.ClientServiceName = _options.ServiceName;
                    trackList.Add(track);
                }
                else
                    existingTrack.Playlists.Add(new ExternalPlaylistDTO() { Id = playlist.Id, Name = playlist.Name });
            }
            ));
            return trackList;
        }
        private List<SpotifyPlaylistInfo> GetUserPlaylistInfoCollection(string authToken, string userName)
        {
            var playlistInfoCollection = new List<SpotifyPlaylistInfo>();
            var userPlaylists = _client.GetAllUserPlaylists(authToken).Result;

            while (userPlaylists != null && !(playlistInfoCollection.Count >= _options.MaxPlaylists))
            {
                playlistInfoCollection.AddRange(userPlaylists.items.Where(x => x.name != "All"));
                if (userPlaylists.next == null)
                    userPlaylists = null;
                if (userPlaylists != null)
                    userPlaylists = _client.GetAllUserPlaylists(authToken, userPlaylists.next).Result;
            }
            return playlistInfoCollection;
        }

        private List<SpotifyPlaylist> GetUserPlaylistWithTracks(string authToken,List<SpotifyPlaylistInfo> playlistInfos)
        {
            var playlistCollection = new List<SpotifyPlaylist>();
            playlistInfos.ForEach(x => {
                var playlist = GetUserPlaylistWithTracks(authToken, x.id);
                playlist.Name = x.name;
                playlist.Id = x.id;
                playlistCollection.Add(playlist);
            });
            return playlistCollection;
        }
        private SpotifyPlaylist GetUserPlaylistWithTracks(string authToken, string playlistId)
        {
            var playlistcollection = new SpotifyPlaylist();
            var userPlaylist = _client.GetUserPlaylistById(authToken, playlistId).Result;

            while (userPlaylist != null && !(playlistcollection.items.Count >= _options.MaxTracks))
            {
                playlistcollection.items.AddRange(userPlaylist.items);
                if (userPlaylist.Next == null )
                    userPlaylist = null;
                if(userPlaylist != null)
                    userPlaylist = _client.GetUserPlaylistById(authToken, playlistId, userPlaylist.Next).Result;
            }
            return playlistcollection;

        }
    }
}
