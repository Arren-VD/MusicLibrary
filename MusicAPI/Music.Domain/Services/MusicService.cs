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
        private IGenericRepository<Track> _trackRepository;
        private IGenericRepository<UserTrack> _userTrackRepository;
        private IGenericRepository<ClientUserTrack> _clientUserTrackRepository;
        public MusicService(IMapper mapper, IMusicRepository musicRepository, IEnumerable<IExternalService> externalServices, ImportMusicHelper importMusicHelper,
        IGenericRepository<Track> trackRepository, IGenericRepository<UserTrack> userTrackRepository, IGenericRepository<ClientUserTrack> clientUserTrackRepository)
        {
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

                tracks.ForEach(track =>
                {
                    var existingTrack = _trackRepository.FindByConditionAsync(x => x.ISRC_Id == track.ISRC_Id);
                   

                    Track addedTrack = null;
                    if (existingTrack == null)
                    {
                        addedTrack = _trackRepository.Insert(_mapper.Map<Track>(track));
                        _userTrackRepository.Insert(new UserTrack(addedTrack.Id, userId));
                        _clientUserTrackRepository.Insert(new ClientUserTrack(addedTrack.Id, track.ClientServiceName, track.Id, track.Preview_url));

                    }
                    else
                    {
                        var existingUserTrack = _userTrackRepository.FindByConditionAsync(x => x.TrackId == existingTrack.Id && x.UserId == userId);
                        if(existingUserTrack == null)
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

                    _importMusicHelper.AddPlaylists(track, addedTrack ?? existingTrack, userId);
                    _importMusicHelper.AddArtists(track, addedTrack ?? existingTrack);
                });
            }
            var a = _musicRepository.GetCategorizedMusicList(userId);
            var r = _mapper.Map<List<TrackDTO>>(a);
            return r;
        }
    }
}
