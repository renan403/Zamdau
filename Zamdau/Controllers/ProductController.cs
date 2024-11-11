using Microsoft.AspNetCore.Mvc;
using Zamdau.Models;

namespace Zamdau.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ViewProduct(int id)
        {
            var prod = LoadProducts().Where(p => p.Id == id).FirstOrDefault();
            return View(prod);
        }

        [HttpGet]
        public IActionResult Products(string searchTerm, string brand, string priceRange, int page = 1)
        {
            Filtrar(brand, priceRange, searchTerm, page);
            return View();
        }

        private List<Product> LoadProducts()
        {
            // Aqui você deve buscar os produtos da API ou do banco de dados.
            // Exemplo de produto (substitua com a lógica real):
            return
        [
            new Product { Id =  1, Name = "Berserk", Brand = "N/A",Quantity = 4, Description = "Descrição do produto Berserk.", Price = 99.99, ImageUrl = "https://img.myloview.com.br/fotomurais/space-seamless-pattern-beautiful-galaxy-stars-planets-constellations-in-outer-space-texture-for-wallpapers-fabric-wrap-web-page-backgrounds-vector-illustration-700-170488959.jpg" },
            new Product { Id  =  2,Name = "Berserk", Brand = "N/A", Description = "Descrição do produto Berserk.", Price = 1.99, ImageUrl = "https://i.pinimg.com/736x/58/6a/3f/586a3f9bebe61e500f68813d4ece3442.jpg" },
            new Product { Id  =  3,Name = "Berserk", Brand = "N/A", Description = "Descrição do produto Berserk.", Price = 99.99, ImageUrl = "https://pngimg.com/d/dog_PNG50371.png" },
            new Product { Id  =  4,Name = "Uchiha", Brand = "N/A", Description = "Descrição do produto Berserk.", Price = 99.99, ImageUrl = "https://4kwallpapers.com/images/wallpapers/itachi-uchiha-naruto-amoled-black-background-minimal-art-2048x2048-6478.jpg" },
            new Product { Id  =  5,Name = "Lord of the rings", Brand = "N/A", Description = "Descrição do produto Berserk.", Price = 99.99, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTZkMjBjNWMtZGI5OC00MGU0LTk4ZTItODg2NWM3NTVmNWQ4XkEyXkFqcGc@._V1_.jpg" },
        // Adicione mais produtos conforme necessário
        ];
        }
        [HttpGet]
        public IActionResult Filtrar(string brand, string priceRange, string productName, int page)
        {

            // Obtenha a lista completa de produtos
            var produtos = LoadProducts(); // Método que busca todos os produtos

            // Filtra por nome
            if (!string.IsNullOrEmpty(productName))
            {
                produtos = produtos.Where(p => p.Name.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();
                ViewBag.FilterProdName = productName;
            }

            // Filtra por marca
            if (!string.IsNullOrEmpty(brand))
            {
                produtos = produtos.Where(p => p.Brand == brand).ToList();
                ViewBag.FilterBrand = brand;
            }

            // Filtra por faixa de preço
            if (!string.IsNullOrEmpty(priceRange))
            {
                produtos = priceRange switch
                {
                    "0-50" => produtos.Where(p => p.Price <= 50).ToList(),
                    "51-100" => produtos.Where(p => p.Price > 50 && p.Price <= 100).ToList(),
                    "101-200" => produtos.Where(p => p.Price > 100 && p.Price <= 200).ToList(),
                    "200+" => produtos.Where(p => p.Price > 200).ToList(),
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
    }
}
