using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Zamdau.User
{
    public class AP_Seller
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool OwnerUsername { get; set; } = true;
        public List<ProductViewModel> Products { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
    public class AP_RegisterSeller
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
