using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Music.DataAccess.Database;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using MusicAPI.Configuration.Helpers;
using Music.DataAccess;
using Music.Domain;
using AutoMapper;
using Music.Domain.MappingProfiles;
using FluentValidation;
using System.Text.Json.Serialization;
using Music.Spotify;

namespace MusicAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {          
            services.AddDbContextFactory<MusicContext>(
            options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MusicAPI"))
            //, ServiceLifetime.Transient
            );

            services.ConfigureSwagger();
            services.AddAutoMapper(typeof(PlaylistProfile).GetTypeInfo().Assembly);

            services.AddSpotify(options => Configuration.GetSection(nameof(SpotifyOptions)).Bind(options));
                        
            services.RegisterDataAccess().RegisterServices().RegisterValidators().RegisterHelpers();


            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200",
                                            "http://localhost:5000");
                    });
            });

            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
                app.UseDeveloperExceptionPage();
            }
            GlobalExceptionHandlerConfigProvider.ConfigureExceptionHandling(app);

            

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}

