using Zamdau.Models;

namespace Zamdau.Interfaces
{
    public interface ISellerService
    {
        public Task<bool> SaveSellerAsync(string id, RegisterSeller seller);
        public Task<Seller> GetSellerAsync(string id);
    }
}
