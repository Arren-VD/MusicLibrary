using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Clients.ClientErrors
{
    public class Error
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class ClientError
    {
        public Error Error { get; set; }
    }
}
