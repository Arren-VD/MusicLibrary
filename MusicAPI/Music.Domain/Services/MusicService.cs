using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.ErrorHandling.Validations;
using Music.Domain.Services.Helper;
using Music.Models;
using Music.Views;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services
{


    public class MusicService : IMusicService
    {
        private readonly IMapper _mapper;
        private readonly IMusicRepository _musicRepository;
        private readonly IEnumerable<IExternalService> _externalServices;
        private readonly ImportMusicHelper _importMusicHelper;
        private readonly IGenericRepository<Track> _trackRepository;
        private readonly IGenericRepository<UserTrack> _userTrackRepository;
        private readonly IGenericRepository<ClientUserTrack> _clientUserTrackRepository;
        private readonly IUserTrackService _userTrackService;
        private readonly ITrackService _trackService;
        private readonly IClientUserTrackService _clientUserTrackService;
        public MusicService(IMapper mapper, IMusicRepository musicRepository, IEnumerable<IExternalService> externalServices, ImportMusicHelper importMusicHelper, IClientUserTrackService clientUserTrackService,
        IGenericRepository<Track> trackRepository, IGenericRepository<UserTrack> userTrackRepository, IGenericRepository<ClientUserTrack> clientUserTrackRepository, ITrackService trackService, IUserTrackService userTrackService)
        {
            _clientUserTrackService = clientUserTrackService;
            _userTrackService = userTrackService;
            _trackService = trackService;
            _clientUserTrackRepository = clientUserTrackRepository;
            _userTrackRepository = userTrackRepository;
             _trackRepository = trackRepository;
            _importMusicHelper = importMusicHelper;
            _mapper = mapper;
            _musicRepository = musicRepository;
            _externalServices = externalServices;
        }
        public List<TrackDTO> ImportClientMusicToDB(int userId, List<UserTokenDTO> userTokens)
        {
            var tracks = new List<ExternalTrackDTO>();
            foreach (var userToken in userTokens)
            {
                var svc = _externalServices.FirstOrDefault(ms => ms.GetName() == userToken.Name);
                tracks = svc.GetCurrentUserTracksWithPlaylistAndArtist(userToken.Value);

                tracks.ForEach(externalTrack =>
                {
                    var track = _trackRepository.Insert(_mapper.Map<Track>(externalTrack)) ?? _trackRepository.FindByConditionAsync(x => x.ISRC_Id == externalTrack.ISRC_Id);
                    var userTrack = _userTrackRepository.Insert(new UserTrack(track.Id, userId)) ?? _userTrackRepository.FindByConditionAsync(x => x.UserId == userId && x.TrackId == track.Id);
                    var clientUserTrack = _clientUserTrackRepository.Insert(new ClientUserTrack(userTrack.Id, externalTrack.ClientServiceName, externalTrack.Id, externalTrack.Preview_url)) ?? _clientUserTrackRepository.FindByConditionAsync(x =>x.ClientId == externalTrack.Id && x.UserTrackId == userTrack.Id);

                    _importMusicHelper.AddPlaylists(externalTrack, addedTrack ?? existingTrack, userId);
                    _importMusicHelper.AddArtists(externalTrack, addedTrack ?? existingTrack);
                });
            }
            var a = _musicRepository.GetCategorizedMusicList(userId);
            var r = _mapper.Map<List<TrackDTO>>(a);
            return r;
        }
    }
}
