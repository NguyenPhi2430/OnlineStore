using Microsoft.AspNetCore.Identity;
using OnlineStore.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStoreSolution.Data.Entities;
using System.Security.AccessControl;
using OnlineShop.Ultilities.Exceptions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using OnlineStore.ViewModels.Base;
using Microsoft.EntityFrameworkCore;

namespace OnlineStoreSolution.App.System.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<Role> roleManager, IConfiguration config) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) { return new ApiResultError<string>("User not exist"); }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, request.IsRememberd, false);
            if (!result.Succeeded) { return new ApiResultError<string>("Login unsuccessful"); };

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.firstName),
                new Claim(ClaimTypes.Role, string.Join(",", roles)),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new ApiResultSuccess<string>()
            {
                ResultObject = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) 
            {
                return new ApiResultError<bool>("Can't not find any user with that ID");
            }
            await _userManager.DeleteAsync(user);
            return new ApiResultSuccess<bool>();
        }

        public async Task<ApiResult<bool>> Edit(int id, UserUpdateRequest request)
        {
            var userWithSameEmail = await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id);
            if (userWithSameEmail)
            {
                return new ApiResultError<bool>("Email đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Email = request.Email;
            user.firstName = request.FirstName;
            user.lastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.dateOfBirth = request.BirthDay;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiResultSuccess<bool>();
            }
            return new ApiResultError<bool>("Edit not successful.");
        }

        public async Task<ApiResult<PagedViewModel<UserViewModel>>> GetListUser(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword));
            }

            int totalRows = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1)* request.PageSize).Take(request.PageSize)
                                   .Select(x=> new UserViewModel()
                                   {
                                       FirstName = x.firstName,
                                       LastName = x.lastName,
                                       Id = x.Id,
                                       Email = x.Email,
                                       PhoneNumber = x.PhoneNumber,
                                       UserName  = x.UserName,
                                   }).ToListAsync();
            var result =  new PagedViewModel<UserViewModel>()
            {
                items = data,
                totalRecords = totalRows,
            };
            return new ApiResultSuccess<PagedViewModel<UserViewModel>> { ResultObject = result };
        }

        public async Task<ApiResult<UserViewModel>> GetUserById(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiResultError<UserViewModel>("Cannot find any user with that id");
            }
            var userGet = new UserViewModel()
            {
                FirstName = user.firstName,
                LastName = user.lastName,
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                BirthDay = user.dateOfBirth
            };
            return new ApiResultSuccess<UserViewModel>(userGet);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var userName = await _userManager.FindByNameAsync(request.UserName);
            if (userName != null) 
            {

                return new ApiResultError<bool>("User Name already exits.");
            }
            var email  = await _userManager.FindByEmailAsync(request.Email);
            if (email != null)
            {

                return new ApiResultError<bool>("Email already exits.");
            }

            var user = new AppUser()
            {
                dateOfBirth = request.BirthDay,
                firstName = request.FirstName,
                lastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) 
            { return new ApiResultSuccess<bool>(); }
            return new ApiResultError<bool>("Unsuccessfully add new user.");
        }
    }
}
