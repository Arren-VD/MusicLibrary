using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Contracts.Repositories
{
    public interface IUserTokensRepository
    {
        UserClient AddTokenById(UserClient userClient);

        void SaveChanges();
    }
}
