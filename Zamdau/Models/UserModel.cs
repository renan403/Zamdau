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
        public string? ReturnUrl { get; set; }
    }
    public class SignUp
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be longer than 6 characters")]
        public string? Pwd { get; set; }
        [Required(ErrorMessage = "Age is mandatory.")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int? Age { get; set; }
        public bool IsSeller { get; } = false;
        public DateTime CreatedAt { get; set; }
        [Required]
        [Compare("Pwd")]
        public string? ConfirmPassword { get; set; }

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
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is mandatory.")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int? Age { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is mandatory.")]
        [EmailAddress(ErrorMessage = "Enter a valid email.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Enter a valid telephone number in (DDD) DDD-DDDD  format.")]
        public string? Phone { get; set; }

        public bool IsSeller { get; set; }

    }

    public class UserModel 
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        
        public string? Error { get; set; }

    }


}
