using Microsoft.AspNetCore.Mvc;
using Zamdau.Models;
using System.Diagnostics;
using Firebase.Auth;
using Zamdau.Interfaces;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Zamdau.Controllers
{

    public class HomeController(ILogger<HomeController> logger, IUser user) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IUser _user = user;
        
        public async Task<IActionResult> Index()
        {
            //return RedirectToAction("TelaTeste","Home");
            ViewBag.token = HttpContext.Session.GetString("tokenClient");

            if (!string.IsNullOrEmpty(ViewBag.token))
            {
               var user = await _user.InfoUser(ViewBag.token);
               ViewBag.NameUser =  user.Name;
            }
            var produtos = new List<Product>
        {
            new Product { Id = 1, Nome = "Monitor HD10k", Descricao = "Monitor with advanced 22nd century technology", ImagemUrl = "./images/Products/Monitor/Designer (19).jpeg", Preco = 2500.00m },
            new Product { Id = 2, Nome = "Keyboard XXIIV", Descricao = "Keyboard with advanced 23nd century technology", ImagemUrl = "./images/Products/KeyBoard/Designer (15).jpeg", Preco = 500.00m },
            new Product { Id = 3, Nome = "Zamdau console", Descricao = "Exclusive Zamdau console", ImagemUrl = "./images/Products/Console/Designer (23).jpeg", Preco = 1000.00m },
        };
            return View(produtos);
        }
        [HttpGet]
        public IActionResult Filtrar(string brand, string priceRange, string productName, int page)
        {

            // Obtenha a lista completa de produtos
            var produtos = LoadProducts(); // Método que busca todos os produtos

            // Filtra por nome
            if (!string.IsNullOrEmpty(productName))
            {
                produtos = produtos.Where(p => p.NomeProd.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();
                ViewBag.FilterProdName = productName;
            }

            // Filtra por marca
            if (!string.IsNullOrEmpty(brand))
            {
                produtos = produtos.Where(p => p.MarcaProd == brand).ToList();
                ViewBag.FilterBrand = brand;
            }

            // Filtra por faixa de preço
            if (!string.IsNullOrEmpty(priceRange))
            {
                produtos = priceRange switch
                {
                    "0-50" => produtos.Where(p => p.PrecoProd <= 50).ToList(),
                    "51-100" => produtos.Where(p => p.PrecoProd > 50 && p.PrecoProd <= 100).ToList(),
                    "101-200" => produtos.Where(p => p.PrecoProd > 100 && p.PrecoProd <= 200).ToList(),
                    "200+" => produtos.Where(p => p.PrecoProd > 200).ToList(),
                    _ => produtos
                };
                ViewBag.FilterPrice = priceRange;
            }

            // Paginação
            int pageSize = 9;

            // Paginação
            
            var paginatedProducts = produtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            
            ViewBag.TotalPages = (int)Math.Ceiling((double)produtos.Count / pageSize);
            ViewBag.CurrentPage = page;

            return PartialView("_ProductList", paginatedProducts);
        }

        public IActionResult TelaTeste()
        {
            
            
            return View();
        }
        [HttpGet]
        public IActionResult Products(string searchTerm, string brand, string priceRange, int page = 1)
        {
            Filtrar(brand, priceRange, searchTerm, page);
            
            return View();
        }

        private List<ModelTest> LoadProducts()
        {
            // Aqui você deve buscar os produtos da API ou do banco de dados.
            // Exemplo de produto (substitua com a lógica real):
            return new List<ModelTest>
        {
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 1.99, UrlImg = "https://assetsio.gnwcdn.com/guts-berserk-manga.jpeg?width=1200&height=1200&fit=bounds&quality=70&format=jpg&auto=webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 929.99, UrlImg = "https://sm.ign.com/ign_br/news/b/berserk-ma/berserk-manga-to-continue-after-creators-death_pbr4.jpg" },
            new ModelTest { NomeProd = "Berserkasasas", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 122.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserkaasasasasasasasas", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 919.99, UrlImg = "https://sm.ign.com/ign_br/news/b/berserk-ma/berserk-manga-to-continue-after-creators-death_pbr4.jpg" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 1.99, UrlImg = "https://assetsio.gnwcdn.com/guts-berserk-manga.jpeg?width=1200&height=1200&fit=bounds&quality=70&format=jpg&auto=webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 929.99, UrlImg = "https://sm.ign.com/ign_br/news/b/berserk-ma/berserk-manga-to-continue-after-creators-death_pbr4.jpg" },
            new ModelTest { NomeProd = "Berserkasasas", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 122.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserkaasasasasasasasas", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 919.99, UrlImg = "https://sm.ign.com/ign_br/news/b/berserk-ma/berserk-manga-to-continue-after-creators-death_pbr4.jpg" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "marca1", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 1.99, UrlImg = "https://assetsio.gnwcdn.com/guts-berserk-manga.jpeg?width=1200&height=1200&fit=bounds&quality=70&format=jpg&auto=webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 929.99, UrlImg = "https://sm.ign.com/ign_br/news/b/berserk-ma/berserk-manga-to-continue-after-creators-death_pbr4.jpg" },
            new ModelTest { NomeProd = "Berserkasasas", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk2.", PrecoProd = 99.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserk", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 122.99, UrlImg = "https://criticalhits.com.br/wp-content/uploads/2024/06/berserk-fan-motion-comic-by-stud-696x348.webp" },
            new ModelTest { NomeProd = "Berserkaasasasasasasasas", MarcaProd = "N/A", DescriProd = "Descrição do produto Berserk.", PrecoProd = 919.99, UrlImg = "https://sm.ign.com/ign_br/news/b/berserk-ma/berserk-manga-to-continue-after-creators-death_pbr4.jpg" },
            // Adicione mais produtos conforme necessário
        };
        }



        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult HelpCenter()
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
