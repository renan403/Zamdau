using Zamdau.Models;

namespace Zamdau.Interfaces
{
    public interface ISellerService
    {
        public Task<bool> SaveSellerAsync(string id, RegisterSeller seller);
        public Task<bool> UpdateSellerAsync(string id, UpdateSeller seller, IFormFile ProfilePicture);
        public Task<Seller> GetSellerAsync(string id);

    }
}
