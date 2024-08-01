using Zamdau.Interfaces;
using API_Zamdau.DataBase;
using Firebase.Auth;
namespace Rcsp.Models
{
    public class SignIn
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
    public class SignUp
    {

    }
    public class ResetPWD
    {
        public string? Email { get; set; }
        public string? Error { get; set; }
    }

    public class User : IUser
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Autentication _auth;
        public UserZamdau _zUser;
        public User()
        {
            _zUser = new UserZamdau();
            _auth = new Autentication();
        }
        public async Task<bool> RegisterAccount()
        {
            try
            {
                return await _auth.CreateEmail("renancporto94@gmail.com", "12345678");
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> ResetAccess()
        {
            return true;
        }
        public async Task<bool> ConfirmAccount()
        {

            return true;
        }
        public async Task<bool> DeleteAccount()
        {
            await _auth.DeleteEmail("renancporto94@gmail.com");
            return true;
        }

    }


}
