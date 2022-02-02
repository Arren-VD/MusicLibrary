using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.ErrorHandling.Validations;
using Music.Models;
using Music.Spotify.Domain.Contracts.Services;
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
        private readonly UserCreationValidator _userValidation;
        public MusicService(IMapper mapper, IMusicRepository musicRepository, IEnumerable<IExternalService> externalServices, IUserTokensRepository userTokensRepository, UserCreationValidator userValidation)
        {
            _userValidation = userValidation;
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
                        var addedTrack = _musicRepository.AddTrack(_mapper.Map<Track>(track));
                        _musicRepository.SaveChanges();
                        _musicRepository.AddUserTrack(new UserTrack(addedTrack.Id,userId));
                        track.Playlists.ForEach(playlist =>
                        {
                            var existingPlaylist = _musicRepository.GetPlaylistByClientId(playlist.Id);
                            if (existingPlaylist == null)
                            {
                                var result = _mapper.Map<Playlist>(playlist);
                                result.UserId = userId;
                                var addedPlaylist = _musicRepository.AddPlaylist(result);
                                _musicRepository.SaveChanges();
                                _musicRepository.AddPlaylistTrack(new PlaylistTrack(addedPlaylist.Id, addedTrack.Id,userId));
                            }
                            else
                            {
                                _musicRepository.AddPlaylistTrack(new PlaylistTrack(existingPlaylist.Id, addedTrack.Id, userId));
                            }
                        });
                        track.Artists.ForEach(artist =>
                        {
                            var existingArtist = _musicRepository.GetArtistByClientId(artist.Id);
                            if (existingArtist  == null)
                            {
                                var addedArtist = _musicRepository.AddArtist(_mapper.Map<Artist>(artist));
                                _musicRepository.SaveChanges();
                                _musicRepository.AddTrackArtist(new TrackArtist(addedTrack.Id, addedArtist.Id));
                            }
                            else
                            {
                                _musicRepository.AddTrackArtist(new TrackArtist(addedTrack.Id, existingArtist.Id));
                            }
                        });
                        _musicRepository.SaveChanges();
                    }
                });
            }
            var r = _musicRepository.GetCategorizedMusicList(userId);
            var r2 = _mapper.Map<List<TrackDTO>>(r);
            return r2;
        }
    }
}
