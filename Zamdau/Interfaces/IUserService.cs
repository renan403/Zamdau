using API_Zamdau.User;
using Zamdau.Models;

namespace Zamdau.Interfaces
{
    public interface IUserService
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool Seller { get; set; }
        public string? Error { get; set; }

        Task<bool> AddToCartAsync(string uid, string idProduct, string quantity);
        Task<string> RegisterAccount(SignUp user);
        Task<bool> ResetPasswordAccess(string email);
        Task<bool> ReSendVerificationEmail(string tokenClient);
        Task<bool> DeleteAccount(string email);
        Task<AP_Auth> Login(string email, string password);
        Task<bool> SaveUserAsync(string id, Account user);
        Task<bool> SavePartialUserAsync(string id, Account user);
        Task<Account?> GetUser(string tokenId);
        Task<List<CartItem>> GetCartUser(string tokenId);
        Task<string?> GetEmailID(string tokenId);
        Task<Address> GetAddress(string tokenId);
        Task<string> RegisteAddress(string id, Address address);
        Task<bool> RemoveItemCart(string tokenId,string itemId);
        Task<bool> DeleteAddress(string tokenId);
        Task<bool> UpdateAddress(string tokenId, Address address);
        Task<bool> AddComment(Comment comment);
    }
}
