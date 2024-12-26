using Zamdau.Models;

namespace Zamdau.Interfaces
{
    public interface ISellerService
    {
        public Task<bool> SaveSellerAsync(string id, RegisterSeller seller);
        public Task<bool> UpdateSellerAsync(string id, UpdateSeller seller, IFormFile ProfilePicture);
        public Task<Seller> GetSellerAsync(string id);
        public Task<Seller> GetSellerByNameAsync(string id, string name);
        public Task<bool> CreateProductAsync(string id, Product product, IFormFile ImageUrl);
        public Task DeleteProductSellerAsync(string id, string guid);
    }
}
