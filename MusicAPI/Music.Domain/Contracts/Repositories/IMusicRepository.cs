using Microsoft.EntityFrameworkCore.Storage;
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
        public Task<List<Track>> GetCategorizedMusicList(int userId);
        public void SaveChanges();
        Task<IDbContextTransaction> Transaction();
    }
}
