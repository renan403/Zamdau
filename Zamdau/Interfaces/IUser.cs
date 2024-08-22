using API_Zamdau.User;
using Zamdau.Models;

namespace Zamdau.Interfaces
{
    public interface IUser
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Error { get; set; }

        Task<AP_Auth> RegisterAccount(string email, string password, string name);
        Task<bool> ResetPasswordAccess(string email);
        Task<bool> ReSendVerificationEmail(string tokenClient);
        Task<bool> DeleteAccount(string email);
        Task<AP_Auth> Login(string email, string password);
        Task<AP_User> InfoUser(string tokenId);
    }
}
