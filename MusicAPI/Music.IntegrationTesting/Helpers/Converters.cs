using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Music.IntegrationTesting.Helpers
{
    public class Converters
    {
        public static StringContent GetStringContent(object obj)
    => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
    }
}
