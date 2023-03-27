using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineStore.ViewModels.Base;
using OnlineStore.ViewModels.System;
using System.Data.Entity.Core.Mapping;

namespace OnlineStore.AdminApp.Services
{
    public interface IUserAPIClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<PagedViewModel<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);
        Task<ApiResult<bool>> CreateUser(RegisterRequest request);
        Task<ApiResult<bool>> Edit(int id, UserUpdateRequest request);
        Task<ApiResult<UserViewModel>> GetUserById(int id);
        Task<ApiResult<bool>> Delete(int id);
    }
}
