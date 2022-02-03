using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
    public class ClientUserTrack
    {
        public ClientUserTrack(int userTrackId, string clientName, string clientId, string preview_url)
        {
            UserTrackId = userTrackId;
            ClientServiceName = clientName;
            ClientId = clientId;
            Preview_url = preview_url;
        }
        public ClientUserTrack()
        {

        }

        public int Id { get; set; }
        public int UserTrackId { get; set; }
        public string ClientServiceName { get; set; }
        public string ClientId { get; set; }
        public string Preview_url { get; set; }
    }
}
