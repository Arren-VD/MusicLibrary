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

        public ImportMusicHelper(IMapper mapper, IGenericRepository<Artist> artistRepository, IGenericRepository<TrackArtist> trackArtistRepository,
            IGenericRepository<PlaylistTrack> playlistTrackRepository, IGenericRepository<Playlist> playListRepository, IGenericRepository<ClientPlayListTrack> clientPlaylistTrackRepository)
        {
            _clientPlaylistTrackRepository = clientPlaylistTrackRepository;
            _mapper = mapper;
            _artistRepository = artistRepository;
            _trackArtistRepository = trackArtistRepository;
            _playlistTrackRepository = playlistTrackRepository;
            _playListRepository = playListRepository;
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
