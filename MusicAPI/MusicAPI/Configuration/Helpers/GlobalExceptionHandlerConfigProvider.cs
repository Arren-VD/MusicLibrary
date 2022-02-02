using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Music.Domain.Exceptions;

namespace MusicAPI.Configuration.Helpers
{
    public static class GlobalExceptionHandlerConfigProvider
    {
        public static void ConfigureExceptionHandling(IApplicationBuilder app)
        {
            app.UseGlobalExceptionHandler(config =>
            {
                config.ContentType = "application/json";
                config.ResponseBody((ex, context) => { return JsonConvert.SerializeObject(ex.Message); });

                config.MapExceptions();
            });
        }

        private static void MapExceptions(this ExceptionHandlerConfiguration config)
        {
            //400 Bad Request
            config.Map<ArgumentNullException>().ToStatusCode(HttpStatusCode.BadRequest);

            //401 Unauthorized

            //403 Forbidden

            // 404
            config.Map<EntityNotFoundException>().ToStatusCode(HttpStatusCode.NotFound);

            // 409 Conflict
            config.Map<EntityAlreadyExistsException>().ToStatusCode(HttpStatusCode.BadRequest);

            //412 Precondition failed

            // 500 Internal server error
            config.Map<Exception>().ToStatusCode(HttpStatusCode.InternalServerError);
            //503 Service Unavailable

            //502 Bad Gateway
            config.Map<HttpException>().ToStatusCode(HttpStatusCode.BadGateway);

        }
    }
}
