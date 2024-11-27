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

        Task<string> RegisterAccount(SignUp user);
        Task<bool> ResetPasswordAccess(string email);
        Task<bool> ReSendVerificationEmail(string tokenClient);
        Task<bool> DeleteAccount(string email);
        Task<AP_Auth> Login(string email, string password);
        Task<bool> SaveUserAsync(string id, Account user);
        Task<bool> SavePartialUserAsync(string id, Account user);
        Task<Account?> GetUser(string tokenId);
    }
}
