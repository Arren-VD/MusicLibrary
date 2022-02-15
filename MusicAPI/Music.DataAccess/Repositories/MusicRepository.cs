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
        public async Task<List<Track>> GetCategorizedMusicList(int userId)
        {
            var list = await _context.UserTracks.Where(x => x.UserId == userId)
                .Include(ut => ut.Track).ThenInclude(t => t.TrackArtists).ThenInclude(ta => ta.Artist)
                .Include(ut => ut.Track).ThenInclude(t => t.PlaylistTracks.Where(x => x.UserId == userId)).ThenInclude(pt => pt.Playlist)
                .Include(ut => ut.Track).ThenInclude(t => t.PlaylistTracks.Where(x => x.UserId == userId)).ThenInclude(pt => pt.ClientPlaylists).Where(x => x.UserId == userId)
                .Include(ut => ut.Track).ThenInclude(t => t.UserTracks).ThenInclude(x => x.ClientTracks).Where(x => x.UserId == userId)
                .Select(t => t.Track).ToListAsync();
            
            return list;
        }
        public async Task<IDbContextTransaction> Transaction()
        {
            return  await _context.Database.BeginTransactionAsync();
        }
        public async void SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
