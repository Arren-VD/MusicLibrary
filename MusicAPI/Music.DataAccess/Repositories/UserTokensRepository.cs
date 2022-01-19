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
        public UserClient AddTokenById(UserClient userToken)
        {
            return  _context.UserClients.Add(userToken).Entity;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
