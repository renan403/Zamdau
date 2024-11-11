using API_Zamdau.DataBase;
using API_Zamdau.User;
using System.ComponentModel.DataAnnotations;
using Zamdau.Interfaces;

namespace Zamdau.Models
{
    public class SignIn
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public bool IsAuthenticated { get; set; }
    }
    public class SignUp
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be longer than 6 characters")]
        public string? Password { get; set; }
        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? Email { get; set; }

        public string? Error { get; set; }

    }
    public class ResetPWD
    {
        public string? Email { get; set; }
        public string? Error { get; set; }
    }
    public class Account
    {

        [Required(ErrorMessage = "Name is mandatory.")]
        [StringLength(70, ErrorMessage = "Maximum size of 70 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Just letters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Age is mandatory.")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public required string Gender { get; set; }

        [Required(ErrorMessage = "Country code is required.")]
        public required string CountryCode { get; set; }

        [Required(ErrorMessage = "Email is mandatory.")]
        [EmailAddress(ErrorMessage = "Enter a valid email.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Telephone is mandatory.")]
        [Phone(ErrorMessage = "Enter a valid telephone number in (DDD) DDD-DDDD  format.")]
        public string? Phone { get; set; }


    }






    public class User : IUser
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Error { get; set; }

        public Autentication _auth;
        public UserZamdau _zUser;

        public User()
        {
            _zUser = new UserZamdau();
            _auth = new Autentication();
        }
        public async Task<AP_Auth> RegisterAccount(string email, string password, string name)
        {
            try
            {
                var _apUser = await _auth.CreateEmail(email, password, name);
                return _apUser;
            }
            catch (Exception ex)
            {
                return new AP_Auth(ex.Message);
            }
        }
        public async Task<bool> ResetPasswordAccess(string email) => await _auth.ResetPassword(email);

        public async Task<bool> ReSendVerificationEmail(string tokenClient) => await _auth.ReSendVerificationEmail(tokenClient);

        public async Task<bool> DeleteAccount(string email) => await _auth.DeleteEmail(email);

        public async Task<AP_Auth> Login(string email, string password) => await _zUser.Login(email, password);
        public async Task<AP_User> InfoUser(string tokenId) => await _zUser.InfoUser(tokenId);

    }


}
