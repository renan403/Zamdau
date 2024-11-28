using System.ComponentModel.DataAnnotations;

namespace Zamdau.Models
{
    public class Seller
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public bool IsSeller { get; }
        public bool OwnerUsername { get; set; }
        public List<ProductViewModel>? Products { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
    public class RegisterSeller
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
    public class UpdateSeller : RegisterSeller
    {
        public IFormFile ProfilePicture { get; set; }
    }
}
