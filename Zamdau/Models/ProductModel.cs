using API_Zamdau.User;
using System.ComponentModel.DataAnnotations;

namespace Zamdau.Models
{

    public class ListProduct
    {
        public string Key { get; set; }
        public Product Object { get; set; }
    }
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Code { get; set; }
        public string? ImageUrl { get; set; }
        public int? Rating { get; set; }
        public int? ReviewCount { get; set; }
        public List<Specification> Specifications { get; set; } = [];
        public Dictionary<string, Comment> Comments { get; set; } = [];
    }

    public class Comment
    {
        public string OrderNumber { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }
        public int Rating { get; set; } 
    }

    public class Specification
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    

}
