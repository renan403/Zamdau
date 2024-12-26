using API_Zamdau.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Zamdau.Interfaces;
using Zamdau.Models;

namespace Zamdau.Controllers
{
    public class ProductController(IProductService productService, ISellerService sellerService,  IUserService user) : Controller
    {
        
        private readonly IProductService _productService = productService;
        private readonly ISellerService _sellerService = sellerService;
        
        private readonly IUserService _user = user;

        

        public async Task<IActionResult> DeleteProductSeller(string productId)
        {
            await _sellerService.DeleteProductSellerAsync(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, productId);
            return RedirectToAction("SellerProfile","Seller");
        }
       
        
        [HttpGet]
        public async Task<IActionResult> ViewProduct(string productId)
        {
            var prod = await _productService.GetProductAsync(productId);
            //var prod = LoadProducts().Where(p => p.Id == id).FirstOrDefault();
            return View(prod);
        }

        [HttpGet]
        public async Task<IActionResult> Products(string searchTerm, double minPrice, double maxPrice, string brand, int page = 1)
        {
            var products = await _productService.GetAllProductAsync();



           products = Filtrar(products, brand, minPrice, maxPrice, searchTerm);





            int pageSize = 9;
            ViewBag.Brands = products.DistinctBy(p => p.Brand).Select(p => p.Brand);
            ViewBag.TotalPages = (int)Math.Ceiling((double)products.Count / pageSize);
            ViewBag.CurrentPage = page;
            return View(products);
        }

        [HttpGet]
        public List<Product> Filtrar(List<Product> products, string brand, double minPrice, double maxPrice, string productName)
        {
          


            // Filtra por nome
            if (!string.IsNullOrEmpty(productName))
            {
                products = products.Where(p => p.Name.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();
                ViewBag.FilterProdName = productName;
            }
            
            // Filtra por marca
            if (!string.IsNullOrEmpty(brand))
            {
                products = products.Where(p => p.Brand == brand).ToList();
                ViewBag.FilterBrand = brand;
            }

            // Filtra por faixa de preço
            //Min
            if (minPrice > 0)
            {
                products = products.Where(mp => mp.Price >= minPrice).ToList();
                ViewBag.FilterMinPrice = minPrice;
            }
            //Max
            if (minPrice < maxPrice || maxPrice > 0)
            {
                products = products.Where(mp => mp.Price <= maxPrice).ToList();
                ViewBag.FilterMaxPrice = maxPrice;
            }

            // Paginação
            int pageSize = 9;

            // Paginação

           // var paginatedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //ViewBag.Brands = products.DistinctBy(p => p.Brand).Select(p => p.Brand);
            //ViewBag.TotalPages = (int)Math.Ceiling((double)products.Count / pageSize);
            //ViewBag.CurrentPage = page;

            return products;
        }
    }
}
