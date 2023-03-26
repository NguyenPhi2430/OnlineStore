using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.AdminApp.Services;
using OnlineStore.ViewModels.System;
using OnlineStoreSolution.App.System.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.AdminApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAPIClient _userAPIClient;
        private readonly IConfiguration _configs;
        public UserController(IUserAPIClient userAPIClient, IConfiguration configs)
        {
            _userAPIClient = userAPIClient;
            _configs = configs;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)               
        {
            var request = new GetUserPagingRequest
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            var data = await _userAPIClient.GetUsersPaging(request);
            return View(data.ResultObject);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login","User");
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            var result = await _userAPIClient.CreateUser(request);
            if (result.IsSuccess == true) 
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _userAPIClient.GetUserById(id);
            if (result.IsSuccess)
            {
                var user = result.ResultObject;
                var updateRequest = new UserUpdateRequest()
                {
                    BirthDay = user.BirthDay,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = id
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return View(ModelState);
            }
            var result = await _userAPIClient.Edit(request.Id ,request);
            if (result.IsSuccess == true)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return View(ModelState);
            }
            var token =await _userAPIClient.Authenticate(request);
           
            var principle = this.ValidateToken(token.ResultObject);
            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                IsPersistent = true,
            };

            HttpContext.Session.SetString("Token", token.ResultObject);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principle, authProperties);

            return RedirectToAction("Index","Home");
        }

        private ClaimsPrincipal ValidateToken(string token) 
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidAudience = _configs["Tokens:Issuer"],
                ValidIssuer = _configs["Tokens:Issuer"],     
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["Tokens:Key"])),
            };
            ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);
            return claimsPrincipal;
        }
    }
}
