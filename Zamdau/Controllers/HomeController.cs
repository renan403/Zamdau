using Microsoft.AspNetCore.Mvc;
using Zamdau.Models;
using System.Diagnostics;

using Firebase.Auth;
using Zamdau.Interfaces;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Zamdau.Services;

namespace Zamdau.Controllers
{

    public class HomeController(ILogger<HomeController> logger, IUserService user) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IUserService _user = user;

        public async Task<IActionResult> Index()
        {
            //return RedirectToAction("Telateste", "Home");
            
            
            var produtos = new List<Product>
            {
               new Product { Id = 1,Brand="test",Code="00001",Manufacturer="fab",Type="tip", Name = "Monitor HD10k", Description = "Monitor with advanced 22nd century technology", ImageUrl = "./images/Products/Monitor/Designer (19).jpeg", Price = 2500.00 },
                new Product { Id = 2,Brand="test",Code="00001",Manufacturer="fab",Type="tip", Name = "Keyboard XXIIV", Description = "Keyboard with advanced 23nd", ImageUrl = "./images/Products/KeyBoard/Designer (15).jpeg", Price = 500.00 },
                new Product { Id = 3,Brand="test",Code="00001",Manufacturer="fab",Type="tip", Name = "Zamdau console", Description = "Exclusive Zamdau console", ImageUrl = "./images/Products/Console/Designer (23).jpeg", Price = 1000.00 },
                new Product { Id = 4,Brand="test",Code="00001",Manufacturer="fab",Type="tip", Name = "Zamdau robot", Description = "Exclusive Zamdau robot", ImageUrl = "./images/Products/Robot/Designer (28).jpeg", Price = 1000.00 },
            };
            return View(produtos);
        }
        [HttpPost]
        public IActionResult TelaTeste(ModelTest model)
        {

            if (ModelState.IsValid)
            {
                var teste = "teste";
            }





            return View(model);
        }


        public IActionResult TelaTeste()
        {
            
            

            return View(new Seller());
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
