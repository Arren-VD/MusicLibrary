using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Models;
using Music.Views;
using Music.Views.ClientViews;
using Music.Views.GlobalViews;
using Music.Views.Inputs;
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
        private readonly IEnumerable<IExternalService> _externalServices;

        public PlaylistService(IMapper mapper, IGenericRepository repo, IEnumerable<IExternalService> externalServices)
        {
            _mapper = mapper;
            _repo = repo;
            _externalServices = externalServices;
        }

        public async Task<Playlist> AddPlaylist(CancellationToken cancellationToken, NameDTO<string> externalPlaylist, int userId, int trackId, string clientServiceName)
        {
            var mappedPlaylist = _mapper.Map<Playlist>(externalPlaylist);
            mappedPlaylist.UserId = userId;
            var playlist = await _repo.UpsertByCondition<Playlist>(x => x.Name == externalPlaylist.Name && x.UserId == userId, mappedPlaylist);
            var playlistTrack = await _repo.UpsertByCondition<PlaylistTrack>(x => x.PlaylistId == playlist.Id && x.UserId == userId && x.TrackId == trackId, new PlaylistTrack(playlist.Id, trackId, userId));
            var clientPlaylist = await _repo.UpsertByCondition<ClientPlayListTrack>(x => x.ClientPlaylistId == externalPlaylist.Id && x.PlaylistTrackId == playlistTrack.Id && x.ClientServiceName == clientServiceName, new ClientPlayListTrack(externalPlaylist.Id, clientServiceName, playlistTrack.Id));
            return playlist;
        }
        public async Task<List<Playlist>> AddPlaylistCollection(CancellationToken cancellationToken, List<NameDTO<string>> playlistCollection, int userId, int trackId, string clientServiceName)
        {
            List<Playlist> addedPlaylists = new List<Playlist>();
            playlistCollection.ForEach(async externalPlaylist =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                addedPlaylists.Add(await AddPlaylist(cancellationToken, externalPlaylist, userId, trackId, clientServiceName));
            });
            return addedPlaylists;
        }
        public async Task<List<PlaylistDTO>> GetAllUserPlaylists(CancellationToken cancellationToken, int userId)
        {
            return _mapper.Map<List<PlaylistDTO>>(await _repo.FindAllByConditionAsync<Playlist>(x => x.UserId == userId)).Distinct().ToList();
        }
        public async Task<Playlist> AddPlaylistToUserTrack(int userId, int trackId, PlaylistInput playlistInput, CancellationToken cancellationToken)
        {
            var playlist = _mapper.Map<Playlist>(playlistInput);
            playlist.UserId = userId;
            playlist = await _repo.UpsertByCondition(x => x.Name == playlist.Name, playlist);
            var playlistTrack = await _repo.UpsertByCondition(x => x.TrackId == trackId && x.UserId == userId && x.PlaylistId == playlist.Id, new PlaylistTrack(playlist.Id, trackId, userId));
            foreach (var clientPlaylist in playlistInput.ClientPlaylists)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var svc = _externalServices.FirstOrDefault(ms => ms.GetName() == clientPlaylist.ServiceName);
                var addedClientPlaylist = await svc.UpsertPlaylist(clientPlaylist.AuthToken, userId, new NameDTO<string>(clientPlaylist.Id, playlistInput.Name), cancellationToken);
                await _repo.UpsertByCondition(x => x.ClientPlaylistId == addedClientPlaylist.Id && x.PlaylistTrackId == playlistTrack.Id && x.ClientServiceName == clientPlaylist.ServiceName, new ClientPlayListTrack(addedClientPlaylist.Id, clientPlaylist.ServiceName, playlistTrack.Id));
            }   
            return playlist;
        }
    }
}
