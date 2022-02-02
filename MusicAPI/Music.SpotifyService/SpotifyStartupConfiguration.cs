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
using Music.Spotify.Clients.Helpers;

namespace Music.Spotify
{
    public static class SpotifyStartupConfiguration
    {
        public static IServiceCollection AddSpotify(this IServiceCollection services, Action<SpotifyOptions> options)
        {
            var o = new SpotifyOptions();
            options(o);
            services.AddOptions<SpotifyOptions>().Configure(options);
            services.AddTransient<SpotifyOptions>().Configure(options);
            ConfigureServices(services);
            Configure(services, o);
            return services;
        }
        private static IServiceCollection ConfigureServices (this IServiceCollection services)
        {
            services.RegisterServices();
            services.RegisterClients();
            services.RegisterHelpers();
            return services;
        }
        private static IServiceCollection Configure(this IServiceCollection services, SpotifyOptions options)
        {
            services.AddAutoMapper(typeof(SpotifyMappingProfile).GetTypeInfo().Assembly);
            services.AddHttpClient<HttpRequestHelper>(c => c.BaseAddress = new System.Uri(options.URL));
            return services;
        }
    }
}
