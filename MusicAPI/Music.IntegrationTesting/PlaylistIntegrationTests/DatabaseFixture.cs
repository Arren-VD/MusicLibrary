using Microsoft.Extensions.DependencyInjection;
using Music.DataAccess.Database;
using Music.Models;
using MusicAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.IntegrationTesting.PlaylistIntegrationTests
{
    public static class DatabaseFixture
    {
        public static MusicContext getDB(MusicWebApplicationFactory<Startup> factory)
        {
            var scopeFactory = factory.Services.GetService<IServiceScopeFactory>();

            var scope = scopeFactory.CreateScope();

            var myDataContext = scope.ServiceProvider.GetService<MusicContext>();
            return myDataContext;

        }
    }
}
