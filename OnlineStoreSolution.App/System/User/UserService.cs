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
        public async Task<string> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) { return null; }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, request.IsRememberd, false);
            if(!result.Succeeded) { return null; };

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

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        public async Task<PagedViewModel<UserViewModel>> GetListUser(GetUserPagingRequest request)
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
            return new PagedViewModel<UserViewModel>()
            {
                items = data,
                totalRecords = totalRows,
            };
        }

        public async Task<bool> Register(RegisterRequest request)
        {
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
            { return true; }
            return false;
        }
    }
}
