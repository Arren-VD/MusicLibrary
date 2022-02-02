using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Exceptions
{
    public class HttpException : Exception
    {
        public HttpException(HttpStatusCode statusCode,string message, Exception innerException = null) : base(GetErrorMessage(message))
        {

        }
        private static string GetErrorMessage(string message)
            => message;

    }
}
