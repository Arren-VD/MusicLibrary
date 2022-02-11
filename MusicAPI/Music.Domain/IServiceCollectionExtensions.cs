using Microsoft.Extensions.DependencyInjection;
using Music.Domain.Contracts.Services;
using Music.Domain.Services;
using Music.Domain.ErrorHandling.Validations;
using Music.Domain.Services.Helper;

namespace Music.Domain
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IUserTrackService, UserTrackService>();
            services.AddTransient<ITrackService, TrackService>();
            services.AddTransient<IClientUserTrackService, ClientUserTrackService>();
            return services;
        }
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddTransient<UserCreationValidator>();
            return services;
        }
        
        public static IServiceCollection RegisterHelpers(this IServiceCollection services)
        {
            services.AddTransient<ImportMusicHelper>();
            return services;
        }
    }
}
