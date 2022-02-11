using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Models;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IGenericRepository<PlaylistTrack> _playlistTrackRepository;
        private readonly IGenericRepository<Playlist> _playListRepository;
        private readonly IGenericRepository<ClientPlayListTrack> _clientPlaylistTrackRepository;
        private readonly IMapper _mapper;

        public PlaylistService(IGenericRepository<PlaylistTrack> playlistTrackRepository, IGenericRepository<Playlist> playListRepository, IGenericRepository<ClientPlayListTrack> clientPlaylistTrackRepository, IMapper mapper)
        {
            _playlistTrackRepository = playlistTrackRepository;
            _playListRepository = playListRepository;
            _clientPlaylistTrackRepository = clientPlaylistTrackRepository;
            _mapper = mapper;
        }

        public Playlist AddPlaylist(ExternalPlaylistDTO externalPlaylist, int userId, int trackId,string clientServiceName)
        {
            var mappedPlaylist = _mapper.Map<Playlist>(externalPlaylist);
            mappedPlaylist.UserId = userId;
            var playlist = _playListRepository.FindByConditionAsync(x => x.Name == externalPlaylist.Name && x.UserId == userId) ?? _playListRepository.Insert(mappedPlaylist);

            var playlistTrack = _playlistTrackRepository.FindByConditionAsync(x => x.PlaylistId == playlist.Id && x.UserId == userId && x.TrackId == trackId) ?? _playlistTrackRepository.Insert(new PlaylistTrack(playlist.Id, trackId, userId));
            var clientPlaylist = _clientPlaylistTrackRepository.FindByConditionAsync(x => x.ClientId == externalPlaylist.Id && x.PlaylistTrackId == playlistTrack.Id && x.ClientServiceName == clientServiceName) ?? _clientPlaylistTrackRepository.Insert(new ClientPlayListTrack(externalPlaylist.Id, clientServiceName, playlistTrack.Id));
            return playlist;
        }
    }
}
