using AutoMapper;
using Microsoft.Extensions.Options;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Services;
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
    public class SpotifyTrackService : ISpotifyTrackService
    {
        private readonly IMapper _mapper;
        private readonly IClient _client;
        private readonly SpotifyOptions _options;
        public SpotifyTrackService(IMapper mapper, IClient client, IOptions<SpotifyOptions> options)
        {
            _options = options.Value;
            _mapper = mapper;
            _client = client;
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
    }
}
