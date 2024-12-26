using API_Zamdau.DataBase;
using Newtonsoft.Json;
using Zamdau.Interfaces;
using Zamdau.Models;

namespace Zamdau.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductZamdau product;

        public ProductService()
        {
            product = new ProductZamdau();
        }


        public async Task<Product> GetProductAsync( string guid)
        {

           return JsonConvert.DeserializeObject<Product> (await product.GetProductAsync( guid));
        }

        public async Task<List<Product>> GetAllProductAsync()
        {

            return JsonConvert.DeserializeObject<List<Product>>(await product.GetAllProductAsync());
        }
        
    }
}
