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

        public async Task<List<TrackDTO>> ImportClientMusicToDB(CancellationToken cancellationToken,int userId, List<UserTokenDTO> userTokens)
        {
            var tracks = new List<ExternalTrackDTO>();
            foreach (var userToken in userTokens)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var svc = _externalServices.FirstOrDefault(ms => ms.GetName() == userToken.Name);
                tracks = await svc.GetCurrentUserTracksWithPlaylistAndArtist(cancellationToken,userToken.Value);

                tracks.ForEach(externalTrack =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    using (var transaction = _musicRepository.Transaction())
                    {
                        var track = _trackService.AddTrack(externalTrack, userId);
                        Playlist playlist = null;
                        Artist artist = null;
                        externalTrack.Playlists.ForEach(externalPlaylist =>
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            playlist = _playlistService.AddPlaylist(externalPlaylist, userId, track.Id, externalTrack.ClientServiceName);
                        });
                        externalTrack.Artists.ForEach(externalArtist =>
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            artist = _artistService.AddArtist(externalArtist, track.Id);                     
                        });
                        if (playlist == null || track == null || artist == null)
                            transaction.Rollback();
                        else
                            transaction.Commit();
                    }
                });
            }
            return _mapper.Map<List<TrackDTO>>(_musicRepository.GetCategorizedMusicList(userId));
        }
    }
}
