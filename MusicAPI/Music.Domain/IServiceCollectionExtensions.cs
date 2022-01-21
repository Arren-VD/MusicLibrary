using Microsoft.Extensions.DependencyInjection;
using Music.Domain.Contracts.Services;
using Music.Domain.Services;
using Music.Domain.Validators;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Music.Spotify.Domain.Contracts.Services;
using System.Linq;
using AutoMapper;
using Music.Spotify.Domain.Services;

namespace Music.Domain
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

   
            services.AddTransient<IExternalService,SpotifyTestService>();
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IExternalService, SpotifyService>();
            services.AddTransient<IMusicService,MusicService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidators>());

            return services;
        }
        public enum ServiceEnum
        {
            A,
            B
        }
    }
}
