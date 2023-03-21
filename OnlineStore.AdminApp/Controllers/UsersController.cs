using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.AdminApp.Services;
using OnlineStore.ViewModels.System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.AdminApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserAPIClient _userAPIClient;
        private readonly IConfiguration _configs;
        public UsersController(IUserAPIClient userAPIClient, IConfiguration configs)
        {
            _userAPIClient = userAPIClient;
            _configs = configs;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Users");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return View(ModelState);
            }
            var token =await _userAPIClient.Authenticate(request);
            var principle = this.ValidateToken(token);
            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                IsPersistent = true,
            };
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
