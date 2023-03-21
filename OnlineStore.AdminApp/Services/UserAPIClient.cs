using Newtonsoft.Json;
using OnlineStore.ViewModels.System;
using System.Text;

namespace OnlineStore.AdminApp.Services
{
    public class UserAPIClient : IUserAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserAPIClient(IHttpClientFactory httpClientFactory) 
        { 
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json"); 
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5002");
            var response = await client.PostAsync("/api/users/authenticate", stringContent);
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }
    }
}
