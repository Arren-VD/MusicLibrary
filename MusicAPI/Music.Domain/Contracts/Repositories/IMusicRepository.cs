using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Repositories
{
    public interface IMusicRepository
    {
        public Playlist AddPlaylist(Playlist playlist);
        public Playlist GetPlaylistByClientId(string clientId);
        public  Artist AddArtist(Artist artist);
        public Artist GetArtistByClientId(string clientId);

        public Track AddTrack(Track track);
        public Track GetTrackById(string trackId);
        public PlaylistTrack AddPlaylistTrack(PlaylistTrack playlistTrack);
        public UserTrack AddUserTrack(UserTrack userTrack);
        public TrackArtist AddTrackArtist(TrackArtist trackArtist);
        public List<Track> GetCategorizedMusicList(int userId);
        public void SaveChanges();
    }
}
