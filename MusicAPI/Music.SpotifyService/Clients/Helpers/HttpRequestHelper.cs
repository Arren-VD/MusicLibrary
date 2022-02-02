using Music.Domain.Exceptions;
using Music.Spotify.Clients.ClientErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Clients.Helpers
{
    public  class HttpRequestHelper
    {
        private readonly HttpClient _client;
        public HttpRequestHelper(HttpClient client)
        {
            _client = client;
        }
        public async Task<T> Get<T>(string authToken,string path)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, _client.BaseAddress + path.Replace(_client.BaseAddress.AbsoluteUri,"")))
            {
                requestMessage.Headers.Add("Authorization", "Bearer " + authToken);

                var result = await _client.SendAsync(requestMessage);

                if (result.IsSuccessStatusCode)
                    return result.Content.ReadFromJsonAsync<T>().Result;
                throw new HttpException(result.StatusCode, result.Content.ReadFromJsonAsync<ClientError>().Result.Error.Message);
            }
        }
    }
}
