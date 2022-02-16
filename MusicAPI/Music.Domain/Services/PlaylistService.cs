using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Models;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.Domain.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IGenericRepository<PlaylistTrack> _playlistTrackRepository;
        private readonly IGenericRepository<Playlist> _playListRepository;
        private readonly IGenericRepository<ClientPlayListTrack> _clientPlaylistTrackRepository;
        private readonly IMapper _mapper;
        private readonly ITrueGenericRepository _repo;

        public PlaylistService(IGenericRepository<PlaylistTrack> playlistTrackRepository, IGenericRepository<Playlist> playListRepository, IGenericRepository<ClientPlayListTrack> clientPlaylistTrackRepository, IMapper mapper, ITrueGenericRepository repo)
        {
            _playlistTrackRepository = playlistTrackRepository;
            _playListRepository = playListRepository;
            _clientPlaylistTrackRepository = clientPlaylistTrackRepository;
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Playlist> AddPlaylist(CancellationToken cancellationToken, ExternalPlaylistDTO externalPlaylist, int userId, int trackId,string clientServiceName)
        {
            var mappedPlaylist = _mapper.Map<Playlist>(externalPlaylist);
            mappedPlaylist.UserId = userId;
            var playlist = await _repo.FindByConditionAsync<Playlist>(x => x.Name == externalPlaylist.Name && x.UserId == userId) ?? await _repo.Insert(mappedPlaylist);

            var playlistTrack = await _repo.FindByConditionAsync<PlaylistTrack>(x => x.PlaylistId == playlist.Id && x.UserId == userId && x.TrackId == trackId) ?? await _repo.Insert<PlaylistTrack>(new PlaylistTrack(playlist.Id, trackId, userId));
            var clientPlaylist = await _repo.FindByConditionAsync<ClientPlayListTrack>(x => x.ClientId == externalPlaylist.Id && x.PlaylistTrackId == playlistTrack.Id && x.ClientServiceName == clientServiceName) ?? await _repo.Insert<ClientPlayListTrack>(new ClientPlayListTrack(externalPlaylist.Id, clientServiceName, playlistTrack.Id));
            return playlist;
        }
        public async Task<List<Playlist>> AddPlaylistCollection(CancellationToken cancellationToken, List<ExternalPlaylistDTO> playlistCollection, int userId, int trackId, string clientServiceName)
        {
            List<Playlist> addedPlaylists = new List<Playlist>();
            playlistCollection.ForEach(async externalPlaylist =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                addedPlaylists.Add(await AddPlaylist(cancellationToken, externalPlaylist, userId, trackId, clientServiceName));
            });
            return  addedPlaylists;
        }
    }
}
