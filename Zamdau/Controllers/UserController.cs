using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zamdau.Models;
using System.Diagnostics;
using Zamdau.Interfaces;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Zamdau.Services;
using System.Security.Cryptography.X509Certificates;

namespace Zamdau.Controllers
{
    [Authorize]
    public class UserController(IUserService user, IProductService product, IPaymentService payment) : Controller
    {
        private readonly IUserService _user = user;
        private readonly IPaymentService _payment = payment;
        private readonly IProductService _product = product;

        [HttpPost]
        public async Task<ActionResult> AddComment(Comment comment)
        {

            comment.Author = (await _user.GetUser(User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value)).Name ?? $"User{new Random().Next(101)}";
            comment.DatePosted = DateTime.UtcNow;

            await _user.AddComment(comment);


            return RedirectToAction("Details", "User", new { id = comment.OrderNumber });

        }

        [HttpGet]
        public async Task<IActionResult> CancelOrder(string id)
        {
            var order = await _payment.UpdateOrderStatusByIdAsync(id, PaymentStatus.Cancelled);

            return RedirectToAction("Details", "User", new { id });

        }
        [HttpGet]
        public async Task<IActionResult> UpdateOrder(string id)
        {

            var order = await _payment.FindOrderByIdAsync(id);
            switch (order.PaymentStatus)
            {
                case PaymentStatus.Shipped:
                    await _payment.UpdateOrderStatusByIdAsync(id, PaymentStatus.Completed);
                    break;
                case PaymentStatus.Processing:
                    await _payment.UpdateOrderStatusByIdAsync(id, PaymentStatus.Shipped);
                    break;
                case PaymentStatus.Pending:
                    await _payment.UpdateOrderStatusByIdAsync(id, PaymentStatus.Processing);
                    break;

            }


            return RedirectToAction("Details", "User", new { id });

        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var order = await _payment.FindOrderByIdAsync(id);

            return View(order);

        }
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var orders = await _payment.GetOrdersAsync(User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value);


            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Account() => View(await _user.GetUser(User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value));

        [HttpGet]
        public async Task<IActionResult> RemoveItemCart(string itemId)
        {
            await _user.RemoveItemCart(User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value, itemId);
            return RedirectToAction("Cart");
        }

        [HttpGet]
        public async Task<IActionResult> BuyNow(string ProductId, int Quantity)
        {
            if (Quantity > 5)
                Quantity = 5;
            if (ProductId != null)
            {
                await _user.AddToCartAsync(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, ProductId, Quantity.ToString());
                return RedirectToAction("Cart", "User");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Cart()
        {

            var cart = await _user.GetCartUser(User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value);


            return View(cart);
        }
        [HttpPost]
        public async Task<IActionResult> Cart(CheckoutDetails details)
        {

            details.CartItems.RemoveAll(s => s.IsSelected == null);
            if (ModelState.IsValid)
            {
                var user = await _user.GetUser(User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value);
                details.CustomerEmail = user.Email;
                details.CustomerName = user.Name;
                details.CustomerAddress = new Address();

                var b = await _payment.SaveCheckoutAsync(User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value, details);

                return RedirectToAction("Checkout", "Payment");
            }

            return RedirectToAction("Cart");
        }
        [HttpPost, HttpGet]
        public async Task<IActionResult> AddToCart(string ProductId, int Quantity)
        {
            if (Quantity > 5)
                Quantity = 5;
            var product = await _product.GetProductAsync(ProductId);
            if (product != null)
            {
                await _user.AddToCartAsync(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, ProductId, Quantity.ToString());
                return RedirectToAction("Cart", "User");
            }




            return RedirectToAction();
        }
        [HttpGet]
        public async Task<IActionResult> Address()
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var address = await _user.GetAddress(userId);
            return View(address);
        }
        [HttpGet]
        public IActionResult RegisterAddress() => View();
        [HttpPost]
        public async Task<IActionResult> RegisterAddress(Address address)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                await _user.RegisteAddress(userId, address);

                return RedirectToAction("Address");
            }
            return View(address);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAddress()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            await _user.DeleteAddress(userId);

            return RedirectToAction("Address");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAddress(Address address)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                await _user.UpdateAddress(userId, address);

                return RedirectToAction("Address");
            }
            return View(address);
        }

        [HttpGet]
        public async Task<IActionResult> MyAccount()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var user = await _user.GetUser(userId);

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> MyAccount(Account model)
        {
            if (ModelState.IsValid)
            {
                var id = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                if (id != null)
                    if (await _user.SavePartialUserAsync(id, model))
                        return RedirectToAction("MyAccount");
            }
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }


}
