using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.ErrorHandling.Validations;
using Music.Models;
using Music.Views;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.Domain.Services
{


    public class MusicService : IMusicService
    {
        private readonly IMapper _mapper;
        private readonly IMusicRepository _musicRepository;
        private readonly IEnumerable<IExternalService> _externalServices;
        private readonly ITrackService _trackService;
        private readonly IPlaylistService _playlistService;
        private readonly IArtistService _artistService;

        public MusicService(IMapper mapper, IMusicRepository musicRepository, IEnumerable<IExternalService> externalServices, ITrackService trackService, IPlaylistService playlistService, IArtistService artistService)
        {
            _mapper = mapper;
            _musicRepository = musicRepository;
            _externalServices = externalServices;
            _trackService = trackService;
            _playlistService = playlistService;
            _artistService = artistService;
        }

        public async Task<List<TrackDTO>> ImportClientMusicToDB(int userId, List<UserTokenDTO> userTokens, CancellationToken cancellationToken)
        {
            var tracks = new List<ExternalTrackDTO>();
            foreach (var userToken in userTokens)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var svc = _externalServices.FirstOrDefault(ms => ms.GetName() == userToken.Name);
                tracks = await svc.GetCurrentUserTracksWithPlaylistAndArtist( userToken.Value, cancellationToken);

                tracks.ForEach(async externalTrack =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    {
                        var track = await _trackService.AddTrack( externalTrack, userId, cancellationToken);
                        var playlistCollection = await _playlistService.AddPlaylistCollection(externalTrack.Playlists, userId, track.Id, externalTrack.ClientServiceName, cancellationToken);
                        var artistCollection = await _artistService.AddArtistCollection( externalTrack.Artists, track.Id, cancellationToken);
                    }
                });
            }
            return _mapper.Map<List<TrackDTO>>(await _musicRepository.GetCategorizedMusicList(userId));
        }
        public async Task<TrackCollectionDTO> GetAllTracksWithPlaylistAndArtist( int userId, List<UserTokenDTO> userTokens, List<int> playlistIds, int page, int pageSize, CancellationToken cancellationToken)
        {
            var trackCollection = new TrackCollectionDTO();
            var categorizedMusicList = await _musicRepository.GetCategorizedMusicList(userId);
            if (playlistIds.Any())
                categorizedMusicList = categorizedMusicList.Where(x => x.PlaylistTracks.Any(y => playlistIds.Any(z => z == y.PlaylistId))).ToList();
            trackCollection.TotalPages = (int)Math.Ceiling((double) categorizedMusicList.Count / pageSize);
            categorizedMusicList = categorizedMusicList.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            trackCollection.Tracks = _mapper.Map<List<TrackDTO>>(categorizedMusicList);
            return trackCollection;
        }
    }
}
