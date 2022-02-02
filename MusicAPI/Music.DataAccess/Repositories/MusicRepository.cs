using Microsoft.EntityFrameworkCore;
using Music.DataAccess.Database;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Exceptions;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.DataAccess.Repositories
{
    public class MusicRepository : IMusicRepository
    {
        readonly MusicContext _context;
        public MusicRepository(MusicContext context)
        {
            _context = context;
        }
        public Playlist AddPlaylist(Playlist playlist)
        {
            return _context.Playlist.Add(playlist).Entity;
        }
        public Playlist GetPlaylistByClientId(string clientId)
        {
            return _context.Playlist.FirstOrDefault(x => x.ClientId == clientId);
        }
        public Track AddTrack(Track track)
        {
            return _context.Tracks.Add(track).Entity;
        }
        public Track GetTrackById(string trackId)
        {
            return _context.Tracks.FirstOrDefault(x => x.ClientId == trackId);
        }
        public Artist AddArtist(Artist artist)
        {
            return _context.Artist.Add(artist).Entity;
        }
        public Artist GetArtistByClientId(string clientId)
        {
            return _context.Artist.FirstOrDefault(x => x.ClientId == clientId);
        }
        public PlaylistTrack AddPlaylistTrack(PlaylistTrack playlistTrack)
        {
            return _context.PlaylistTrack.Add(playlistTrack).Entity;
        }
        public UserTrack AddUserTrack(UserTrack userTrack)
        {
            return _context.UserTracks.Add(userTrack).Entity;
        }
        public TrackArtist AddTrackArtist(TrackArtist trackArtist)
        {
            return _context.TrackArtists.Add(trackArtist).Entity;
        }
        public List<Track> GetCategorizedMusicList(int userId)
        {
            var a1 = _context.UserTracks.Where(x => x.UserId == userId)
                .Include(ut => ut.Track).ThenInclude(t => t.TrackArtists).ThenInclude(ta => ta.Artist)
                .Include(ut => ut.Track).ThenInclude(t => t.PlaylistTracks.Where(x => x.UserId == userId)).ThenInclude(pt => pt.Playlist)
                .Select(t => t.Track).ToList();
            
            return a1;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
