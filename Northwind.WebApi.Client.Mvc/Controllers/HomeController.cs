using Microsoft.AspNetCore.Mvc;
using Northwind.WebApi.Client.Mvc.Models;
using ApiDlyaKursovoyTry2.Models;
using System.Diagnostics;

namespace Northwind.WebApi.Client.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger,
             IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [Route("home/archives/{name?}")]
        public async Task<IActionResult> Archives(string? name = "cha")
        {
            HomeProductsViewModel model = new();
            HttpClient client = _httpClientFactory.CreateClient(
            name: "Northwind.WebApi.Service");
            model.NameContains = name;
            model.BaseAddress = client.BaseAddress;
            HttpRequestMessage request = new(
            method: HttpMethod.Get,
            requestUri: $"api/archives/{name}");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                model.Archives = await response.Content
                .ReadFromJsonAsync<IEnumerable<Archive>>();
            }
            else
            {
                model.Archives = Enumerable.Empty<Archive>();
                string content = await response.Content.ReadAsStringAsync();
                string exceptionMessage = content;
                int indexOfQuote = content.IndexOf("'");
                if (indexOfQuote > 0)
                {
                    exceptionMessage = content[..indexOfQuote];
                }
                model.ErrorMessage = string.Format("{0}: {1}:",
                response.ReasonPhrase, exceptionMessage);
            }
            return View(model);
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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
