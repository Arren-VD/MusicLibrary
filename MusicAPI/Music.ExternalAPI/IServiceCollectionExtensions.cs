using Microsoft.Extensions.DependencyInjection;
using Music.Domain.Contracts.Clients;
using Music.ExternalAPI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Music.ExternalAPI
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterClients(this IServiceCollection services)
        {
            services.AddTransient<ISpotifyClient, SpotifyClient>();
            return services;
        }
    }
}
