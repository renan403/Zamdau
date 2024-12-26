using API_Zamdau.DataBase;
using Newtonsoft.Json;
using Zamdau.Interfaces;
using Zamdau.Models;



namespace Zamdau.Services
{
    public class SellerService : ISellerService
    {
        private readonly SellerZamdau _SellerZamdau;

        public SellerService()
        {
            _SellerZamdau = new SellerZamdau();
        }

        public async Task<bool> SaveSellerAsync(string id, RegisterSeller seller) => (await _SellerZamdau.SaveSellerAsync(Helpers.Encrypt(id), JsonConvert.SerializeObject(seller)));
        public async Task<bool> UpdateSellerAsync(string id, UpdateSeller seller, IFormFile ProfilePicture) => (await _SellerZamdau.UpdateSellerAsync(Helpers.Encrypt(id), JsonConvert.SerializeObject(seller), ProfilePicture));       
        public async Task<Seller> GetSellerAsync(string id) => (JsonConvert.DeserializeObject<Seller>(await _SellerZamdau.GetSellerAsync(Helpers.Encrypt(id))) ?? new Seller());
        public async Task<Seller> GetSellerByNameAsync(string id, string name) => (JsonConvert.DeserializeObject<Seller>(await _SellerZamdau.GetSellerByNameAsync(Helpers.Encrypt(id), name)) ?? new Seller());
        public async Task<bool> CreateProductAsync(string id, Product product, IFormFile ImageUrl) => (await _SellerZamdau.CreateProduct(Helpers.Encrypt(id), JsonConvert.SerializeObject(product), ImageUrl));
        public async Task DeleteProductSellerAsync(string id, string guid) => await _SellerZamdau.DeleteProductSellerAsync(Helpers.Encrypt(id),guid);
    }
}
