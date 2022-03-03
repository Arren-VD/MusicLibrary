using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Models;
using Music.Views;
using Music.Views.ClientViews;
using Music.Views.Results;
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
        private readonly IMapper _mapper;
        private readonly IGenericRepository _repo;

        public PlaylistService(IMapper mapper, IGenericRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Playlist> AddPlaylist(ExternalPlaylistDTO externalPlaylist, int userId, int trackId,string clientServiceName, CancellationToken cancellationToken)
        {
            var mappedPlaylist = _mapper.Map<Playlist>(externalPlaylist);
            mappedPlaylist.UserId = userId;
            var playlist = await _repo.UpsertByCondition(x => x.Name == externalPlaylist.Name && x.UserId == userId, mappedPlaylist);
            var playlistTrack = await _repo.UpsertByCondition(x => x.PlaylistId == playlist.Id && x.UserId == userId && x.TrackId == trackId, new PlaylistTrack(playlist.Id, trackId, userId));
            var clientPlaylist = await _repo.UpsertByCondition(x => x.ClientId == externalPlaylist.Id && x.PlaylistTrackId == playlistTrack.Id && x.ClientServiceName == clientServiceName, new ClientPlayListTrack(externalPlaylist.Id, clientServiceName, playlistTrack.Id));
            return playlist;
        }
        public async Task<List<Playlist>> AddPlaylistCollection(List<ExternalPlaylistDTO> playlistCollection, int userId, int trackId, string clientServiceName, CancellationToken cancellationToken)
        {
            List<Playlist> addedPlaylists = new List<Playlist>();
            playlistCollection.ForEach(async externalPlaylist =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                addedPlaylists.Add(await AddPlaylist(externalPlaylist, userId, trackId, clientServiceName, cancellationToken));
            });
            return  addedPlaylists;
        }
        public async Task<List<PlaylistResult>> GetAllUserPlaylists(int userId, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<PlaylistResult>>(await _repo.FindAllByConditionAsync<Playlist>(x => x.UserId == userId)).Distinct().ToList();
        }
    }
}
