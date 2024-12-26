using Zamdau.Models;

namespace Zamdau.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductAsync( string guid);
        Task<List<Product>> GetAllProductAsync();
    }
}
