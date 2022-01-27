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
        public void AddTracks(List<Track> track)
        {
            _context.Track.AddRange(track);
        }
        public void AddPlaylist(List<TrackGenre> track)
        {
            _context.Track.AddRange(track);
        }
        public void AddTrackPlaylist(List<Track> track)
        {
            _context.Track.AddRange(track);
        }
        public void AddTrackArtist(List<Track> track)
        {
            _context.Track.AddRange(track);
        }
        public void AddArtist(List<Track> track)
        {
            _context.Track.AddRange(track);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
