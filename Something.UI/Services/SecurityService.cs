using Something.UI.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Something.UI.Services
{
    public class SecurityService : ISecurityService
    {
        public SecurityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly HttpClient _httpClient;

        public Token SecurityHeader { get; private set; }

        public async Task GetHeader()
        {
            string requestEndpoint = @"home/authenticate";
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(requestEndpoint).Result;
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                SecurityHeader = JsonSerializer.Deserialize<Token>(responseBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + _httpClient.BaseAddress + requestEndpoint);
            }
        }
    }
}
