using Newtonsoft.Json;
using OnlineStore.ViewModels.Base;
using OnlineStore.ViewModels.System;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineStore.AdminApp.Services
{
    public class UserAPIClient : IUserAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration; 

        public UserAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
        { 
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json"); 

            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/api/users/authenticate", stringContent);
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }

        public async Task<bool> CreateUser(RegisterRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/api/users/register", stringContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<PagedViewModel<UserViewModel>> GetUsersPaging(GetUserPagingRequest request)
        {
            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.BearerToken);

            var respone = await client.GetAsync($"/api/users/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}&keyword={request.Keyword}");

            var body = await respone.Content.ReadAsStringAsync();
            var listUsers = JsonConvert.DeserializeObject<PagedViewModel<UserViewModel>>(body);
            return listUsers;   
        }
    }
}
