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
    public class UserTokensRepository : IUserTokensRepository
    {
        readonly IDbContextFactory<MusicContext> _context;
        public UserTokensRepository(IDbContextFactory<MusicContext> context)
        {
            _context = context;
        }
        public async Task<UserClient> AddTokenById(UserClient userToken)
        {
            using (var context = _context.CreateDbContext())
            {
                return (await context.UserClients.AddAsync(userToken)).Entity;
            }
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
