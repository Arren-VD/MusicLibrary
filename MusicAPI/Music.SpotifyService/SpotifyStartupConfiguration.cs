using Microsoft.Extensions.DependencyInjection;
using Music.Spotify.Clients;
using Music.Spotify.Domain.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using Music.Spotify.Domain.Contracts.Clients;

namespace Music.Spotify
{
    public static class SpotifyStartupConfiguration
    {
        public static IServiceCollection SpotifyConfiguration(this IServiceCollection services)
        {
            ConfigureServices(services);
            Configure(services);
            return services;
        }
        private static IServiceCollection ConfigureServices (this IServiceCollection services)
        {
            services.RegisterServices().RegisterClients();
            return services;
        }
        private static void Configure(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SpotifyMappingProfile).GetTypeInfo().Assembly);
            services.AddHttpClient<ISpotifyClient>(c => c.BaseAddress = new System.Uri("https://api.spotify.com/v1"));
        }
    }
}
