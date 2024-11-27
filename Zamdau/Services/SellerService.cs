using Zamdau.Interfaces;
using Zamdau.Models;
using API_Zamdau.DataBase;
using Newtonsoft.Json;
using API_Zamdau.User;



namespace Zamdau.Services
{
    public class SellerService : ISellerService
    {
        SellerZamdau _SellerZamdau;

        public SellerService()
        {
            _SellerZamdau = new SellerZamdau();
        }

        public async Task<bool> SaveSellerAsync(string id, RegisterSeller seller) => (await _SellerZamdau.SaveSellerAsync(Helpers.Encrypt(id), JsonConvert.SerializeObject(seller)));
           
        public async Task<Seller> GetSellerAsync(string id) {
            return JsonConvert.DeserializeObject<Seller>(await _SellerZamdau.GetSellerAsync(Helpers.Encrypt(id)));
           
        } 

    }
}
