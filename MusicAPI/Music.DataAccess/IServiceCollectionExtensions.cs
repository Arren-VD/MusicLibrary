using Microsoft.Extensions.DependencyInjection;
using Music.DataAccess.Repositories;
using Music.Domain.Contracts.Repositories;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Music.DataAccess
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataAccess(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserTokensRepository, UserTokensRepository>();
            services.AddTransient<IMusicRepository, MusicRepository>();
            services.AddTransient<IGenericRepository<Artist>,GenericRepository<Artist>>();
            services.AddTransient<IGenericRepository<TrackArtist>, GenericRepository<TrackArtist>>();
            services.AddTransient<IGenericRepository<Track>, GenericRepository<Track>>();
            services.AddTransient<IGenericRepository<UserTrack>, GenericRepository<UserTrack>>();
            services.AddTransient<IGenericRepository<Playlist>, GenericRepository<Playlist>>();
            services.AddTransient<IGenericRepository<PlaylistTrack>, GenericRepository<PlaylistTrack>>();
            return services;
        }
    }
}
