using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MusicAPI.Configuration.Helpers
{
    /// <summary>
    /// Adds Swagger configuration to the <see cref="IServiceCollection"/> ServiceCollection on startup
    /// </summary>
    public static class SwaggerConfigProvider
    {
        /// <summary>
        /// Add a swagger configuration to the <see cref="IServiceCollection"/> ServiceCollection on startup
        /// </summary>
        /// <param name="services">The ServiceCollection to add the Swagger configuration to.</param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Music Library", Version = "v1", Description = "The API for managing music", });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Spotify Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
            });
        }


    }
}
