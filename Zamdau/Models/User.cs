using Microsoft.AspNetCore.Identity.Data;

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
}
