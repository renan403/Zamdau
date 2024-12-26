using API_Zamdau.DataBase;
using Newtonsoft.Json;
using Zamdau.Interfaces;
using Zamdau.Models;

namespace Zamdau.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentZamdau payment;

        public PaymentService()
        {
            payment = new PaymentZamdau();
        }
        public async Task<bool> SaveCheckoutAsync(string tokenClientId, CheckoutDetails details) => await payment.SaveCheckoutAsync(Helpers.Encrypt(tokenClientId), JsonConvert.SerializeObject(details));
        public async Task<bool> RemoveItemCheckoutAsync(string tokenClientId, string itemId) => await payment.RemoveItemCheckoutAsync(Helpers.Encrypt(tokenClientId), itemId);
        public async Task<bool> CreateOrdersAsync(Orders order) => await payment.CreateOrderAsync(JsonConvert.SerializeObject(order));
        public async Task<List<Orders>> GetOrdersAsync(string tokenClientId) => JsonConvert.DeserializeObject<List<Orders>>(await payment.GetOrdersAsync(Helpers.Encrypt(tokenClientId)));
        public async Task<Orders> FindOrderByIdAsync(string orderId) => JsonConvert.DeserializeObject<Orders>(await payment.FindOrderByIdAsync(orderId));
        public async Task<GetCheckoutDetails?> GetCheckoutAsync(string tokenClientId) => JsonConvert.DeserializeObject<GetCheckoutDetails>(await payment.GetCheckoutAsync(Helpers.Encrypt(tokenClientId)));
        public async Task<bool> UpdateOrderStatusByIdAsync(string orderId, PaymentStatus status) => await payment.UpdateOrderStatusByIdAsync(orderId,(int)status);
    }
}
