namespace Zamdau.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string Brand { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string Description { get; set; } = "";
        public string Type { get; set; } = "";
        public string Availability { get; set; } = "";
        public string Manufacturer { get; set; } = "";
        public string Dimensions { get; set; } = "";
        public string Weight { get; set; } = "";
        public string Color { get; set; } = "";
        public string Material { get; set; } = "";
        public List<Customers>? Customer { get; set; }

    }
    public class Customers
    {
        public string Name { get; set; } = "";
        public string Comment { get; set; } = "";
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
