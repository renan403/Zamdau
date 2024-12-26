using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Zamdau.Interfaces;
using Zamdau.Models;

namespace Zamdau.Controllers
{

    public class HomeController(ILogger<HomeController> logger, IUserService user, IProductService product, IPaymentService payment) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IUserService _user = user;
        private readonly IPaymentService _payment = payment;
        private readonly IProductService _product = product;

        public async Task<IActionResult> Index()
        {          
            var products = (await _product.GetAllProductAsync()).Where(b => b.Brand == "Zamdau");
        
            return View(products);
        }
        public async Task<IActionResult> Confirmation(string OrderNumber)
        {
            var order = await _payment.UpdateOrderStatusByIdAsync(OrderNumber, PaymentStatus.Shipped);

            return View();
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
