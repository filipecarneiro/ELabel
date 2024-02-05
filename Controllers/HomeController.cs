using ELabel.Extensions;
using ELabel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
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
            ViewData["KnownNetworks"] = _configuration.GetValue<string>("KnownNetworks");

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
        public IActionResult Error([FromRoute] int? id)
        {
            if(id != null && id == 404)
            {
                StatusCodeReExecuteFeature? statusCodeReExecuteFeature = (StatusCodeReExecuteFeature?)HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

                string baseUrl = UrlHelper.GetBaseUrl(Request);
                string url = baseUrl + statusCodeReExecuteFeature?.OriginalPath;
                ViewBag.Url = url;
                return View("404");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, StatusCode = id });
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