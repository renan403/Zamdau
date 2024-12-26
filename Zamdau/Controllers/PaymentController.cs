using API_Zamdau.Helpers;
using Firebase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zamdau.Interfaces;
using Zamdau.Models;

namespace Zamdau.Controllers
{

    [Authorize]
    public class PaymentController(IUserService user, IPaymentService payment) : Controller
    {
        private readonly IUserService _user = user;
        private readonly IPaymentService _payment = payment;
        [HttpPost]
        public async Task<IActionResult> ProcessPayment(GetCheckoutDetails paymentData)
        {
            if (!ModelState.IsValid)
            {
                return View("Checkout", paymentData);
            }

            var userId = User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value;

            string guid = Guid.NewGuid().ToString();

            var order = new Orders
            {
                OrderNumber = guid,
                CustomerAddress = paymentData.Details.CustomerAddress,
                Created = DateTime.Now,
                CustomerId = Helpers.Encrypt(userId),
                Items = paymentData.Details.CartItems,
                TotalAmount = paymentData.Details.Total,
                PaymentStatus = PaymentStatus.Pending
            };
            if (await _payment.CreateOrdersAsync(order))
                switch (paymentData.PaymentMethod.SelectedPaymentMethod)
                {
                    case "Pix":
                        return View("Pix", new Pix() { OrderNumber = guid });
                    case "CreditCard":
                        return View("Card", new Card() { OrderNumber = guid, Amount = paymentData.Details.Total });
                }
            return RedirectToAction("Orders", "User");


        }
        
       
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Card(Card model)
        {
            if (ModelState.IsValid)
            {
                if (await _payment.UpdateOrderStatusByIdAsync(model.OrderNumber, PaymentStatus.Shipped))
                    return RedirectToAction("Confirmation", "Home");
                return View("PaymentForm", model);
            }

            return View("Card", model); 
        }
        [HttpGet]
        public IActionResult Pix(Pix px) => View(px);
        [HttpGet]
        public async Task<IActionResult> RemoveFromCheckout(string itemId)
        {
            await _payment.RemoveItemCheckoutAsync(User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value, itemId);
            // Lógica para remover o item do banco
            // Atualizar o estado do checkout e redirecionar
            return RedirectToAction("Checkout");
        }
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            GetCheckoutDetails? checkoutDetailsawait = await _payment.GetCheckoutAsync(User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value);

            if (!ModelState.IsValid || checkoutDetailsawait is null || checkoutDetailsawait.Details.CartItems.Count == 0)
                return RedirectToAction("Cart", "User");

            return View(checkoutDetailsawait);
        }

       


    }
}
