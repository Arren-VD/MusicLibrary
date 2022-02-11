using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Models;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services.Helper
{
    public class ImportMusicHelper
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Artist> _artistRepository;
        private readonly IGenericRepository<TrackArtist> _trackArtistRepository;
        private readonly IGenericRepository<PlaylistTrack> _playlistTrackRepository;
        private readonly IGenericRepository<Playlist> _playListRepository;
        private readonly IGenericRepository<ClientPlayListTrack> _clientPlaylistTrackRepository;
        private readonly IGenericRepository<Track> _trackRepository;
        private readonly IGenericRepository<UserTrack> _userTrackRepository;
        private readonly IGenericRepository<ClientUserTrack> _clientUserTrackRepository;

        public ImportMusicHelper(IMapper mapper, IGenericRepository<Artist> artistRepository, IGenericRepository<TrackArtist> trackArtistRepository, IGenericRepository<Track> trackRepository, IGenericRepository<UserTrack> userTrackRepository,
        IGenericRepository<PlaylistTrack> playlistTrackRepository, IGenericRepository<Playlist> playListRepository, IGenericRepository<ClientPlayListTrack> clientPlaylistTrackRepository, IGenericRepository<ClientUserTrack> clientUserTrackRepository)
        {
            _userTrackRepository = userTrackRepository;
            _clientUserTrackRepository = clientUserTrackRepository;
            _trackRepository = trackRepository;
            _clientPlaylistTrackRepository = clientPlaylistTrackRepository;
            _mapper = mapper;
            _artistRepository = artistRepository;
            _trackArtistRepository = trackArtistRepository;
            _playlistTrackRepository = playlistTrackRepository;
            _playListRepository = playListRepository;
        }

        public void AddUserTrack(ExternalTrackDTO track, Track existingTrack, Track addedTrack, int userId)
        {
            var existingUserTrack = _userTrackRepository.FindByConditionAsync(x => x.TrackId == existingTrack.Id && x.UserId == userId);
            if (existingUserTrack == null)
            {
                _userTrackRepository.Insert(new UserTrack(addedTrack.Id, userId));
                _clientUserTrackRepository.Insert(new ClientUserTrack(existingTrack.Id, track.ClientServiceName, track.Id, track.Preview_url));
            }
            else
            {
                var existingClientUserTrack = _clientUserTrackRepository.FindByConditionAsync(x => x.UserTrackId == existingUserTrack.Id && x.ClientServiceName == track.ClientServiceName);
                if (existingClientUserTrack == null)
                {
                    _clientUserTrackRepository.Insert(new ClientUserTrack(existingTrack.Id, track.ClientServiceName, track.Id, track.Preview_url));
                }
            }
        }
        public void AddArtists(ExternalTrackDTO track, Track addedTrack)
        {
            track.Artists.ForEach(artist =>
            {
                var existingArtist = _artistRepository.FindByConditionAsync(x => x.ClientId == artist.Id);
                if (existingArtist == null)
                {
                    var addedArtist = _artistRepository.Insert(_mapper.Map<Artist>(artist));
                    _trackArtistRepository.Insert(new TrackArtist(addedTrack.Id, addedArtist.Id));
                }
                else
                {
                    _trackArtistRepository.Insert(new TrackArtist(addedTrack.Id, existingArtist.Id));
                }
            });
        }
        public void AddPlaylists(ExternalTrackDTO track, Track addedTrack, int userId)
        {
            track.Playlists.ForEach(playlist =>
            {
                var existingPlaylist = _playListRepository.FindByConditionAsync(x => x.Name == playlist.Name && x.UserId == userId);
                if (existingPlaylist == null)
                {
                    var result = _mapper.Map<Playlist>(playlist);
                    result.UserId = userId;
                    var addedPlaylist = _playListRepository.Insert(result);
                    var addedPlaylistTrack = _playlistTrackRepository.Insert(new PlaylistTrack(addedPlaylist.Id, addedTrack.Id, userId));
                    _clientPlaylistTrackRepository.Insert(new ClientPlayListTrack(playlist.Id,track.ClientServiceName, addedPlaylistTrack.Id));
                }
                else
                {
                    int playlistId;
                    var existingPlaylistTrack = _playlistTrackRepository.FindByConditionAsync(x => x.TrackId == addedTrack.Id && x.PlaylistId == existingPlaylist.Id && x.UserId == userId);
                    if (existingPlaylistTrack == null)
                    {
                        playlistId = _playlistTrackRepository.Insert(new PlaylistTrack(existingPlaylist.Id, addedTrack.Id, userId)).PlaylistId;
                    }
                    else
                    {
                        playlistId = existingPlaylistTrack.PlaylistId;
                    }
                    if (_clientPlaylistTrackRepository.FindByConditionAsync(x => x.PlaylistTrackId == existingPlaylist.Id) == null)
                    {
                        _clientPlaylistTrackRepository.Insert(new ClientPlayListTrack(playlist.Id, track.ClientServiceName, playlistId));
                    }
                }
            });
        }
    }
}
