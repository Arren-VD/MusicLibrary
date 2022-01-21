using Microsoft.Extensions.DependencyInjection;
using Music.Spotify.Clients;
using Music.Spotify.Domain.Contracts;
using Music.Spotify.Domain.Contracts.Clients;
using Music.Spotify.Domain.Contracts.Services;
using Music.Spotify.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Music.Spotify
{
    public static class IServiceCollectionExtensions
    {
        internal static IServiceCollection RegisterClients(this IServiceCollection services)
        {
            services.AddTransient<IClient, SpotifyClient>();

            return services;
        }
        internal static IServiceCollection RegisterServices(this IServiceCollection services)
        {
   

            return services;
        }
    }
}
