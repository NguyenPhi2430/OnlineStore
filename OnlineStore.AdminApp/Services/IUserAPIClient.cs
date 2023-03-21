using OnlineStore.ViewModels.System;

namespace OnlineStore.AdminApp.Services
{
    public interface IUserAPIClient
    {
        Task<string> Authenticate(LoginRequest request);
    }
}
