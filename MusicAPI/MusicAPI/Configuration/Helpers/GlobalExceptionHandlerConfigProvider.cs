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
                config.ResponseBody((ex, context) => JsonConvert.SerializeObject(ex.Message));

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
            config.Map<EntityAlreadyExistsException>().ToStatusCode(HttpStatusCode.NotFound);

            //412 Precondition failed

            // 500 Internal server error
            config.Map<Exception>().ToStatusCode(HttpStatusCode.NotFound);
            //503 Service Unavailable

        }
    }
}
