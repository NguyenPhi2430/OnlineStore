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
        Task<string> Authenticate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
        Task<PagedViewModel<UserViewModel>> GetListUser(GetUserPagingRequest request);
    }
}
