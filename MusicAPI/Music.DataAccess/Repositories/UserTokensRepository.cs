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
        readonly MusicContext _context;
        public UserTokensRepository(MusicContext context)
        {
            _context = context;
        }
        public async Task<UserClient> AddTokenById(UserClient userToken)
        {
            return  (await _context.UserClients.AddAsync(userToken)).Entity;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
