using Microsoft.AspNetCore.Mvc;
using Rcsp.Models;
using System.Diagnostics;

namespace Rcsp.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
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
