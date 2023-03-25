using Microsoft.AspNetCore.Mvc;
using OnlineStore.BackendAPI.Models;
using System.Diagnostics;

namespace OnlineStore.BackendAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
    }
}