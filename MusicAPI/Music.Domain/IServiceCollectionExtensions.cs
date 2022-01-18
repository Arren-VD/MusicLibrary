using Microsoft.Extensions.DependencyInjection;
using Music.Domain.Contracts.Services;
using Music.Domain.Services;
using Music.Domain.Validators;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Music.Domain
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();

            return services;
        }
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            //services.AddTransient<SpotifyValidators>();
            //services.AddTransient<UserValidators>();
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SpotifyValidators>());

            return services;
        }
    }
}
