using Microsoft.Extensions.DependencyInjection;
using Music.Domain.Contracts.Services;
using Music.Domain.Services;
using Music.Spotify.Domain.Contracts.Services;
using Music.Spotify.Domain.Services;
using Music.Domain.ErrorHandling.Validations;

namespace Music.Domain
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IExternalService, SpotifyService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMusicService, MusicService>();

            return services;
        }
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddTransient<UserCreationValidator>();
            return services;
        }
    }
}
