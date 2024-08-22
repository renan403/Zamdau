using Microsoft.AspNetCore.Mvc;
using Zamdau.Models;
using System.Diagnostics;
using Firebase.Auth;
using Zamdau.Interfaces;

namespace Zamdau.Controllers
{

    public class HomeController(ILogger<HomeController> logger, IUser user) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IUser _user = user;

        public async Task<IActionResult> Index()
        {
            ViewBag.token = HttpContext.Session.GetString("tokenClient");

            if (!string.IsNullOrEmpty(ViewBag.token))
            {
               var user = await _user.InfoUser(ViewBag.token);
            }
            var produtos = new List<Product>
        {
            new Product { Id = 1, Nome = "Monitor HD10k", Descricao = "Monitor with advanced 22nd century technology", ImagemUrl = "./images/Products/Monitor/Designer (19).jpeg", Preco = 2500.00m },
            new Product { Id = 2, Nome = "Keyboard XXIIV", Descricao = "Keyboard with advanced 23nd century technology", ImagemUrl = "./images/Products/KeyBoard/Designer (15).jpeg", Preco = 500.00m },
            new Product { Id = 3, Nome = "Zamdau console", Descricao = "Exclusive Zamdau console", ImagemUrl = "./images/Products/Console/Designer (23).jpeg", Preco = 1000.00m },
        };
            return View(produtos);
        }

        public IActionResult TelaTeste()
        {
            return View();
        }
        public IActionResult SignIn()
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
