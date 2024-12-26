using Zamdau.Models;

namespace Zamdau.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> SaveCheckoutAsync(string tokenClientId, CheckoutDetails details);
        Task<GetCheckoutDetails?> GetCheckoutAsync(string tokenClientId);
        Task<bool> RemoveItemCheckoutAsync(string tokenClientId, string itemId);
        Task<bool> CreateOrdersAsync(Orders order);
        Task<List<Orders>> GetOrdersAsync(string tokenClientId);
        Task<bool> UpdateOrderStatusByIdAsync(string orderId, PaymentStatus status);
        Task<Orders> FindOrderByIdAsync(string orderId);

    }
}
