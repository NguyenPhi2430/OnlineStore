using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using NuGet.Versioning;
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
        private readonly IHttpContextAccessor _contextAccessor;

        public UserAPIClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
        { 
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _contextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json"); 

            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/api/users/authenticate", stringContent);
            var token = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<ApiResult<string>>(token);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResultSuccess<string>>(token);
            } 
            return JsonConvert.DeserializeObject<ApiResultError<string>>(token);
        }

        public async Task<ApiResult<bool>> CreateUser(RegisterRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/api/users/register", stringContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResultSuccess<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiResultError<bool>>(result);
        }

        public async Task<ApiResult<bool>> Edit(int id, UserUpdateRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var session = _contextAccessor.HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

            var response = await client.PutAsync($"/api/users/edit/{id}", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var kq = new ApiResultSuccess<bool>();
                return kq;
            }
            return JsonConvert.DeserializeObject<ApiResultError<bool>>(" ");
        }

        public async Task<ApiResult<UserViewModel>> GetUserById(int id)
        {
            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var session = _contextAccessor.HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

            var response = await client.GetAsync($"/api/users/getuserbyid/{id}");

            var body = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject <ApiResult<UserViewModel>>(body);
            
            return user;
        }

        public async Task<ApiResult<PagedViewModel<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
        {
            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var session = _contextAccessor.HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

            var response = await client.GetAsync($"/api/users/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}&keyword={request.Keyword}");

            var body = await response.Content.ReadAsStringAsync();
            var listUsers = JsonConvert.DeserializeObject <ApiResult<PagedViewModel<UserViewModel>>>(body);

            return listUsers;
        }
    }
}
