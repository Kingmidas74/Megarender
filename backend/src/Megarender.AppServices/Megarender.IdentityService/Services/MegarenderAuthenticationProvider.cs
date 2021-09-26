using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace Megarender.IdentityService
{
    public class MegarenderAuthenticationProvider:IAuthenticationProvider
    {
        private readonly Client _client;
        private readonly HttpClient _httpClient;

        public MegarenderAuthenticationProvider(Client client, HttpClient httpClient)
        {
            _client = client;
            _httpClient = httpClient;
        }

        public async Task<Dictionary<string,object>> SureUserExist(User user)
        {
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(
                await (await _httpClient.PostAsJsonAsync(_client.ClientUri, new
                {
                    UserId = user.Id
                })).Content.ReadAsStringAsync());
        }
    }
}