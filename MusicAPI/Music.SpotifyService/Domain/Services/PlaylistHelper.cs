using AutoMapper;
using Microsoft.Extensions.Options;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Models.PlaylistModels;
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
        public List<SpotifyPlaylist> GetAllUserPlaylists(CancellationToken cancellationToken,string authToken, string userName, string userId)
        {
            var playlistInfoCollection = GetUserPlaylistInfoCollection(cancellationToken,authToken, userName, userId);
            var playlistCollection = GetUserPlaylistWithTracks(cancellationToken, authToken, playlistInfoCollection);
            return playlistCollection;
        }
        public List<ExternalTrackOutput> GetAllUserTracksFromPlaylists(CancellationToken cancellationToken,List<SpotifyPlaylist> playlistCollection)
        {
            var trackList = new List<ExternalTrackOutput>();
            playlistCollection.ForEach(playlist => playlist.items.ForEach(item =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                //Check if track is already in list, if it is add playlist to that item.
                var existingTrack = trackList.FirstOrDefault(x => x.ISRC_Id == item.track.external_ids.isrc);
                if (existingTrack == null)
                {
                    var track = _mapper.Map<ExternalTrackOutput>(item.track);
                    track.Playlists.Add(new NameDTO<string>(playlist.Id,playlist.Name));
                    track.ClientServiceName = _options.ServiceName;
                    trackList.Add(track);
                }
                else
                    existingTrack.Playlists.Add(new NameDTO<string>(playlist.Id, playlist.Name));
            }
            ));
            return trackList;
        }
        private List<SpotifyPlaylistInfo> GetUserPlaylistInfoCollection(CancellationToken cancellationToken,string authToken, string userName, string userId)
        {
            var playlistInfoCollection = new List<SpotifyPlaylistInfo>();
            var userPlaylists = _client.GetAllUserPlaylists(cancellationToken,authToken).Result;

            while (userPlaylists != null && !(playlistInfoCollection.Count >= _options.MaxPlaylists))
            {
                cancellationToken.ThrowIfCancellationRequested();
                playlistInfoCollection.AddRange(userPlaylists.items.Where(x => x.name != "All" && x.owner.id == userId));
                if (userPlaylists.next == null)
                    userPlaylists = null;
                if (userPlaylists != null)
                    userPlaylists = _client.GetAllUserPlaylists(cancellationToken,authToken, userPlaylists.next).Result;
            }
            return playlistInfoCollection;
        }

        private List<SpotifyPlaylist> GetUserPlaylistWithTracks(CancellationToken cancellationToken,string authToken,List<SpotifyPlaylistInfo> playlistInfos)
        {
            var playlistCollection = new List<SpotifyPlaylist>();
            playlistInfos.ForEach(x => {
                var playlist = GetUserPlaylistWithTracks(cancellationToken, authToken, x.id);
                playlist.Name = x.name;
                playlist.Id = x.id;
                playlistCollection.Add(playlist);
            });
            return playlistCollection;
        }
        private SpotifyPlaylist GetUserPlaylistWithTracks(CancellationToken cancellationToken,string authToken, string playlistId)
        {
            var playlistcollection = new SpotifyPlaylist();
            var userPlaylist = _client.GetUserPlaylistById(cancellationToken,authToken, playlistId).Result;

            while (userPlaylist != null && !(playlistcollection.items.Count >= _options.MaxTracks))
            {
                cancellationToken.ThrowIfCancellationRequested();
                playlistcollection.items.AddRange(userPlaylist.items);
                if (userPlaylist.Next == null )
                    userPlaylist = null;
                if(userPlaylist != null)
                    userPlaylist = _client.GetUserPlaylistById(cancellationToken,authToken, playlistId, userPlaylist.Next).Result;
            }
            return playlistcollection;

        }
    }
}
