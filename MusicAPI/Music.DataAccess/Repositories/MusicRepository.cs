using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        public List<Track> GetCategorizedMusicList(int userId)
        {
            var a1 = _context.UserTracks.Where(x => x.UserId == userId)
                .Include(ut => ut.Track).ThenInclude(t => t.TrackArtists).ThenInclude(ta => ta.Artist)
                .Include(ut => ut.Track).ThenInclude(t => t.PlaylistTracks.Where(x => x.UserId == userId)).ThenInclude(pt => pt.Playlist)
                .Include(ut => ut.Track).ThenInclude(t => t.PlaylistTracks.Where(x => x.UserId == userId)).ThenInclude(pt => pt.ClientPlaylists).Where(x => x.UserId == userId)
                .Include(ut => ut.Track).ThenInclude(t => t.UserTracks).ThenInclude(x => x.ClientTracks).Where(x => x.UserId == userId)
                .Select(t => t.Track).ToList();
            
            return a1;
        }
        public IDbContextTransaction Transaction()
        {
            return  _context.Database.BeginTransaction();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
