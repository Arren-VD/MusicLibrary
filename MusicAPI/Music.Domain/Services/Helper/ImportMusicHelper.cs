using AutoMapper;
using Music.Domain.Contracts.Repositories;
using Music.Models;
using Music.Views.ClientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Services.Helper
{
    public class ImportMusicHelper
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Artist> _artistRepository;
        private readonly IGenericRepository<TrackArtist> _trackArtistRepository;
        private readonly IGenericRepository<PlaylistTrack> _playlistTrackRepository;
        private readonly IGenericRepository<Playlist> _playListRepository;

        public ImportMusicHelper(IMapper mapper, IGenericRepository<Artist> artistRepository, IGenericRepository<TrackArtist> trackArtistRepository,
            IGenericRepository<PlaylistTrack> playlistTrackRepository, IGenericRepository<Playlist> playListRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
            _trackArtistRepository = trackArtistRepository;
            _playlistTrackRepository = playlistTrackRepository;
            _playListRepository = playListRepository;
        }

        public void AddArtists(ClientTrackDTO track, Track addedTrack)
        {
            track.Artists.ForEach(artist =>
            {
                var existingArtist = _artistRepository.FindByConditionAsync(x => x.ClientId == artist.Id);
                if (existingArtist == null)
                {
                    var addedArtist = _artistRepository.Insert(_mapper.Map<Artist>(artist));
                    _trackArtistRepository.Insert(new TrackArtist(addedTrack.Id, addedArtist.Id));
                }
                else
                {
                    _trackArtistRepository.Insert(new TrackArtist(addedTrack.Id, existingArtist.Id));
                }
            });
        }
        public void AddPlaylists(ClientTrackDTO track, Track addedTrack, int userId)
        {
            track.Playlists.ForEach(playlist =>
            {
                var existingPlaylist = _playListRepository.FindByConditionAsync(x => x.ClientId == playlist.Id);
                if (existingPlaylist == null)
                {
                    var result = _mapper.Map<Playlist>(playlist);
                    result.UserId = userId;
                    var addedPlaylist = _playListRepository.Insert(result);
                    _playlistTrackRepository.Insert(new PlaylistTrack(addedPlaylist.Id, addedTrack.Id, userId));
                }
                else
                {
                    _playlistTrackRepository.Insert(new PlaylistTrack(existingPlaylist.Id, addedTrack.Id, userId));
                }
            });
        }
    }
}
