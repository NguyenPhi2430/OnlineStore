using OnlineStore.ViewModels.Base;
using OnlineStore.ViewModels.System;

namespace OnlineStore.AdminApp.Services
{
    public interface IUserAPIClient
    {
        Task<string> Authenticate(LoginRequest request);
        Task<PagedViewModel<UserViewModel>> GetUsersPaging(GetUserPagingRequest request);
        Task<bool> CreateUser(RegisterRequest request);
    }
}
