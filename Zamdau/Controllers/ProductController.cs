using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Zamdau.Models;

namespace Zamdau.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = new OrderDetailsViewModel
            {
                Id = 1,
                OrderNumber = "12345",
                Status = "Shipped",
                OrderDate = DateTime.Now.AddDays(-3),
                Total = 199.99m,
                Products = new List<OrderItemViewModel>
                {
                    new OrderItemViewModel { Id=1, Name = "Product 1", Quantity = 2, Price = 49.99m },
                    new OrderItemViewModel { Id=2, Name = "Product 2", Quantity = 1, Price = 99.99m }
                }
            };

            return View(order);
            
        }

        public IActionResult Orders()
        {
            var orders = new List<Orders>
{
    new() { Id = 1, OrderNumber = "12345", Status = "Pending", OrderDate = DateTime.Now.AddDays(-5), Total = 99.99m },
    new () { Id = 2, OrderNumber = "12346", Status = "Processing", OrderDate = DateTime.Now.AddDays(-2), Total = 149.99m },
    new () { Id = 3, OrderNumber = "12347", Status = "Shipped", OrderDate = DateTime.Now.AddDays(-1), Total = 249.99m },
    new () { Id = 4, OrderNumber = "12348", Status = "Cancelled", OrderDate = DateTime.Now.AddDays(-3), Total = 0.00m },
    new () { Id = 5, OrderNumber = "12348", Status = "Completed", OrderDate = DateTime.Now.AddDays(-3), Total = 0.00m }
};


            return View(orders);
        }
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
            new Product { Id =  1, Name = "Berserk", Brand = "teste2",Quantity = 4, Description = "Descrição do produto Berserk.", Price = 99.99, ImageUrl = "https://img.myloview.com.br/fotomurais/space-seamless-pattern-beautiful-galaxy-stars-planets-constellations-in-outer-space-texture-for-wallpapers-fabric-wrap-web-page-backgrounds-vector-illustration-700-170488959.jpg" },
            new Product { Id  =  2,Specifications = new List<Specification>
    {
        new Specification { Name = "Material", Value = "Plastic" },
        new Specification { Name = "Dimensions", Value = "10x20x30cm" },
        new Specification { Name = "Weight", Value = "500g" }
    },Name = "Lord of the rings", Brand = "N/A", Description = "Descrição do produto Berserk.", Price = 99.99, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTZkMjBjNWMtZGI5OC00MGU0LTk4ZTItODg2NWM3NTVmNWQ4XkEyXkFqcGc@._V1_.jpg" },
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
            ViewBag.Brands = produtos.DistinctBy(p => p.Brand).Select(p => p.Brand);
            ViewBag.TotalPages = (int)Math.Ceiling((double)produtos.Count / pageSize);
            ViewBag.CurrentPage = page;

            return PartialView("_ProductList", paginatedProducts);
        }
    }
}
