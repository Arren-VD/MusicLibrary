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
        private readonly IUserTokensRepository _userTokensRepository;
        private readonly ImportMusicHelper _importMusicHelper;
        private IGenericRepository<Artist> _artistRepository;
        private IGenericRepository<TrackArtist> _trackArtistRepository;
        private IGenericRepository<Track> _trackRepository;
        private IGenericRepository<PlaylistTrack> _playlistTrackRepository;
        private IGenericRepository<Playlist> _playlistRepository;
        private IGenericRepository<UserTrack> _userTrackRepository;
        public MusicService(IMapper mapper, IMusicRepository musicRepository, IEnumerable<IExternalService> externalServices, IUserTokensRepository userTokensRepository, ImportMusicHelper importMusicHelper,
            IGenericRepository<Artist> artistRepository, IGenericRepository<TrackArtist> trackArtistRepository, IGenericRepository<Track> trackRepository,
            IGenericRepository<PlaylistTrack> playlistTrackRepository, IGenericRepository<Playlist> playlistRepository, IGenericRepository<UserTrack> userTrackRepository)
        {
            _userTrackRepository = userTrackRepository;
            _artistRepository = artistRepository;
            _trackArtistRepository = trackArtistRepository;
            _trackRepository = trackRepository;
            _playlistTrackRepository = playlistTrackRepository;
            _playlistRepository = playlistRepository;
            _importMusicHelper = importMusicHelper;
            _mapper = mapper;
            _musicRepository = musicRepository;
            _externalServices = externalServices;
            _userTokensRepository = userTokensRepository;
        }
        public List<TrackDTO> ImportClientMusicToDB(int userId, List<UserTokenDTO> userTokens)
        {
            var tracks = new List<ClientTrackDTO>();
            foreach (var userToken in userTokens)
            {
                var svc = _externalServices.FirstOrDefault(ms => ms.GetName() == userToken.Name);
                tracks = svc.GetCurrentUserTracksWithPlaylistAndArtist(userToken.Value);

                tracks.ForEach(track =>
                {
                    if (_musicRepository.GetTrackById(track.Id) == null)
                    {
                        var addedTrack = _trackRepository.Insert(_mapper.Map<Track>(track));

                        _userTrackRepository.Insert(new UserTrack(addedTrack.Id, userId));

                        _importMusicHelper.AddPlaylists(track, addedTrack, userId);
                        _importMusicHelper.AddArtists(track, addedTrack);

                        _musicRepository.SaveChanges();
                    }
                });
            }
            return _mapper.Map<List<TrackDTO>>(_musicRepository.GetCategorizedMusicList(userId));
        }
    }
}
