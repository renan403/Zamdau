using API_Zamdau.DataBase;
using API_Zamdau.User;
using Newtonsoft.Json;
using Zamdau.Interfaces;
using Zamdau.Models;

namespace Zamdau.Services
{
    public class UserService : IUserService
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Error { get; set; }
        public bool Seller { get; set; }

        public Autentication _auth;
        public UserZamdau _zUser;

        public UserService()
        {
            _zUser = new UserZamdau();
            _auth = new Autentication();
        }
        public async Task<string> RegisterAccount(SignUp user)
        {

            return await _auth.CreateEmail(JsonConvert.SerializeObject(user));


        }
        public async Task<bool> AddToCartAsync(string uid, string idProduct, string quantity) => await _zUser.AddToCartAsync(Helpers.Encrypt(uid), idProduct, quantity);

        public async Task<bool> ResetPasswordAccess(string email) => await _auth.ResetPassword(email);

        public async Task<bool> ReSendVerificationEmail(string tokenClient) => await _auth.ReSendVerificationEmail(tokenClient);

        public async Task<bool> DeleteAccount(string email) => await _auth.DeleteEmail(email);

        public async Task<AP_Auth> Login(string email, string password) => await _zUser.Login(Helpers.Encrypt(email), Helpers.Encrypt(password));

        public async Task<Account?> GetUser(string tokenId) => JsonConvert.DeserializeObject<Account>(await _zUser.GetUser(tokenId));

        public async Task<List<CartItem>> GetCartUser(string tokenId) => JsonConvert.DeserializeObject<List<CartItem>>(await _zUser.GetCartUser(tokenId)) ?? [];

        public async Task<string?> GetEmailID(string tokenId) => (await _zUser.GetEmailID(tokenId));

        public async Task<bool> DeleteAddress(string tokenId) => (await _zUser.DeleteAddress(Helpers.Encrypt(tokenId)));

        public async Task<bool> UpdateAddress(string tokenId, Address address) => (await _zUser.UpdateAddress(Helpers.Encrypt(tokenId), JsonConvert.SerializeObject(address)));

        public async Task<Address> GetAddress(string tokenId) => JsonConvert.DeserializeObject<Address>(await _zUser.GetAddress(Helpers.Encrypt(tokenId))) ?? new Address();

        public async Task<bool> SaveUserAsync(string id, Account address) => await _zUser.SaveUserAsync(Helpers.Encrypt(id), JsonConvert.SerializeObject(address));

        public async Task<string> RegisteAddress(string id, Address address) => await _zUser.RegisteAddress(Helpers.Encrypt(id), JsonConvert.SerializeObject(address));

        public async Task<bool> SavePartialUserAsync(string id, Account user) => await _zUser.SavePartialUserAsync(Helpers.Encrypt(id), JsonConvert.SerializeObject(user));

        public async Task<bool> RemoveItemCart(string tokenId, string itemId) => await _zUser.RemoveItemCartAsync(Helpers.Encrypt(tokenId), itemId);

        public async Task<bool> AddComment(Comment comment) => await _zUser.AddComment(JsonConvert.SerializeObject(comment));

    }
}
