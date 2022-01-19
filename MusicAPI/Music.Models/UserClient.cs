using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
    public class UserClient
    {
        public UserClient()
        {

        }
        public UserClient(string clientId, string clientName, int userId)
        {
            ClientId = clientId;
            ClientName = clientName;
            UserId = userId;    
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
    }
}
