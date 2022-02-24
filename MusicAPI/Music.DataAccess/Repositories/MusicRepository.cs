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
        readonly IDbContextFactory<MusicContext> _context;
        public MusicRepository(IDbContextFactory<MusicContext> context)
        {
            _context = context;
        }
        public async Task<List<Track>> GetCategorizedMusicList(int userId)
        {
            using (var context = _context.CreateDbContext())
            {
                var list = await context.UserTracks.Where(x => x.UserId == userId).ToListAsync();
                list = await context.UserTracks.Include(ut => ut.Track).ThenInclude(t => t.TrackArtists).ThenInclude(ta => ta.Artist).ToListAsync();
                list = await context.UserTracks.Include(ut => ut.Track).ThenInclude(t => t.PlaylistTracks.Where(x => x.UserId == userId)).ThenInclude(pt => pt.Playlist).ToListAsync();
                list = await context.UserTracks.Include(ut => ut.Track).ThenInclude(t => t.PlaylistTracks.Where(x => x.UserId == userId)).ThenInclude(pt => pt.ClientPlaylists).Where(x => x.UserId == userId).ToListAsync();
                list = await context.UserTracks.Include(ut => ut.Track).ThenInclude(t => t.UserTracks).ThenInclude(x => x.ClientTracks).Where(x => x.UserId == userId).ToListAsync();
                var result = await context.UserTracks.Select(t => t.Track).ToListAsync();
                return result;
            }
        }
        public async Task<IDbContextTransaction> Transaction()
        {
            using (var context = _context.CreateDbContext())
            {
                return await context.Database.BeginTransactionAsync();
            }
        }
        public async Task CloseConnection()
        {

        }
        public async Task SaveChanges()
        {
            using (var context = _context.CreateDbContext())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
