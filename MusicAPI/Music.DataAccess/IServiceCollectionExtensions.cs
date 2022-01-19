using Microsoft.Extensions.DependencyInjection;
using Music.DataAccess.Repositories;
using Music.Domain.Contracts.Repositories;
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
            return services;
        }
    }
}
