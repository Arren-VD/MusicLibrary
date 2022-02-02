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
        private readonly IMusicRepository _musicRepository;
        private readonly IMapper _mapper;

        public ImportMusicHelper(IMusicRepository musicRepository, IMapper mapper)
        {
            _musicRepository = musicRepository;
            _mapper = mapper;
        }

        public void AddArtists(ClientTrackDTO track, Track addedTrack)
        {
            track.Artists.ForEach(artist =>
            {
                var existingArtist = _musicRepository.GetArtistByClientId(artist.Id);
                if (existingArtist == null)
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
        }
        public void AddPlaylists(ClientTrackDTO track, Track addedTrack, int userId)
        {
            track.Playlists.ForEach(playlist =>
            {
                var existingPlaylist = _musicRepository.GetPlaylistByClientId(playlist.Id);
                if (existingPlaylist == null)
                {
                    var result = _mapper.Map<Playlist>(playlist);
                    result.UserId = userId;
                    var addedPlaylist = _musicRepository.AddPlaylist(result);
                    _musicRepository.SaveChanges();
                    _musicRepository.AddPlaylistTrack(new PlaylistTrack(addedPlaylist.Id, addedTrack.Id, userId));
                }
                else
                {
                    _musicRepository.AddPlaylistTrack(new PlaylistTrack(existingPlaylist.Id, addedTrack.Id, userId));
                }
            });
        }
    }
}
