using System.ComponentModel.DataAnnotations;


namespace API_Zamdau.User
{

    public class SellerBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }

    public class AP_Seller : SellerBase
    {
        public string? Uid { get; set; }
        public bool IsSeller { get; }
        public bool OwnerUsername { get; set; }
        public List<AP_Product>? Products { get; set; }
    }

    public class AP_RegisterSeller : SellerBase
    {
        public string? Uid { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
        public new string Email { get; set; }
    }

    public class AP_UpdateSeller : SellerBase {
        public string? Uid { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }

   
}
