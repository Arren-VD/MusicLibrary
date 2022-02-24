using AutoMapper;
using Microsoft.Extensions.Options;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Models.PlaylistModels;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public List<SpotifyPlaylist> GetAllUserPlaylists(string authToken, string userName, string userId, CancellationToken cancellationToken)
        {
            var playlistInfoCollection = GetUserPlaylistInfoCollection(authToken, userName, userId, cancellationToken);
            var playlistCollection = GetUserPlaylistWithTracks( authToken, playlistInfoCollection, cancellationToken);
            return playlistCollection;
        }
        public List<ExternalTrackDTO> GetAllUserTracksFromPlaylists(List<SpotifyPlaylist> playlistCollection, CancellationToken cancellationToken)
        {
            var trackList = new List<ExternalTrackDTO>();
            playlistCollection.ForEach(playlist => playlist.items.ForEach(item =>
            {
                cancellationToken.ThrowIfCancellationRequested();
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
        private List<SpotifyPlaylistInfo> GetUserPlaylistInfoCollection(string authToken, string userName, string userId, CancellationToken cancellationToken)
        {
            var playlistInfoCollection = new List<SpotifyPlaylistInfo>();
            var userPlaylists = _client.GetAllUserPlaylists(authToken, cancellationToken).Result;

            while (userPlaylists != null && !(playlistInfoCollection.Count >= _options.MaxPlaylists))
            {
                cancellationToken.ThrowIfCancellationRequested();
                playlistInfoCollection.AddRange(userPlaylists.items.Where(x => x.name != "All" && x.owner.id == userId));
                if (userPlaylists.next == null)
                    userPlaylists = null;
                if (userPlaylists != null)
                    userPlaylists = _client.GetAllUserPlaylists(authToken, cancellationToken, userPlaylists.next).Result;
            }
            return playlistInfoCollection;
        }

        private List<SpotifyPlaylist> GetUserPlaylistWithTracks(string authToken,List<SpotifyPlaylistInfo> playlistInfos, CancellationToken cancellationToken)
        {
            var playlistCollection = new List<SpotifyPlaylist>();
            playlistInfos.ForEach(x => {
                var playlist = GetUserPlaylistWithTracks( authToken, x.id, cancellationToken);
                playlist.Name = x.name;
                playlist.Id = x.id;
                playlistCollection.Add(playlist);
            });
            return playlistCollection;
        }
        private SpotifyPlaylist GetUserPlaylistWithTracks(string authToken, string playlistId, CancellationToken cancellationToken)
        {
            var playlistcollection = new SpotifyPlaylist();
            var userPlaylist = _client.GetUserPlaylistById(authToken, playlistId, cancellationToken).Result;

            while (userPlaylist != null && !(playlistcollection.items.Count >= _options.MaxTracks))
            {
                cancellationToken.ThrowIfCancellationRequested();
                playlistcollection.items.AddRange(userPlaylist.items);
                if (userPlaylist.Next == null )
                    userPlaylist = null;
                if(userPlaylist != null)
                    userPlaylist = _client.GetUserPlaylistById(authToken, playlistId, cancellationToken, userPlaylist.Next).Result;
            }
            return playlistcollection;

        }
    }
}
