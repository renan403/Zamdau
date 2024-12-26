using System.ComponentModel.DataAnnotations;

namespace Zamdau.Models
{
    // Classe base para propriedades comuns
    public class SellerBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }

    public class Seller : SellerBase
    {
        public string? Uid { get; set; }
        public bool IsSeller { get; }
        public bool OwnerUsername { get; set; }
        public List<Product>? Products { get; set; }
    }

    public class RegisterSeller : SellerBase
    {
        public string? IdUser { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
        public new string Email { get; set; }
    }

    public class UpdateSeller : SellerBase { }


}
