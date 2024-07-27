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
            new Product { Id = 1, Nome = "Produto 1", Descricao = "Descrição breve do produto 1.", ImagemUrl = "https://via.placeholder.com/350x200", Preco = 100.00m },
            new Product { Id = 2, Nome = "Produto 2", Descricao = "Descrição breve do produto 2.", ImagemUrl = "https://via.placeholder.com/350x200", Preco = 200.00m },
            new Product { Id = 3, Nome = "Produto 3", Descricao = "Descrição breve do produto 3.", ImagemUrl = "https://via.placeholder.com/350x200", Preco = 300.00m }
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
