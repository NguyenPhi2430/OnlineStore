using OnlineStore.ViewModels.Base;
using OnlineStore.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.App.System.User
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<PagedViewModel<UserViewModel>>> GetListUser(GetUserPagingRequest request);
        Task<ApiResult<bool>> Edit(int id, UserUpdateRequest request);
        Task<ApiResult<UserViewModel>> GetUserById(int id);
    }
}
