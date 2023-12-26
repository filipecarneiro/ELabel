using ELabel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ELabel.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Info()
        {
            ViewData["KnownProxy"] = _configuration.GetValue<string>("KnownProxy");

            ViewData["RemoteIpAddress"] = HttpContext.Connection.RemoteIpAddress?.ToString();

            System.Text.StringBuilder headers = new();
            foreach (var header in HttpContext.Request.Headers)
            {
                headers.Append(header.Key);
                headers.Append('=');
                headers.AppendLine(header.Value);
            }

            ViewData["Headers"] = headers.ToString();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string urlPath)
        {
            // Use a query string to set the CultureInfo
            string returnUrl = $"{urlPath}?culture={culture}";

            return LocalRedirect(returnUrl);
        }
    }
}