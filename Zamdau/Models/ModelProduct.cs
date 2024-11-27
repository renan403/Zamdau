namespace Zamdau.Models
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemViewModel> Products { get; set; }
    }

    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
    public class Orders
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
        public string Name { get; set; } 
        public  string Brand { get; set; } 
        public  string ImageUrl { get; set; }  
        public string? Description { get; set; } 
        public string? Manufacturer { get; set; } 
        public string? Type { get; set; } 

        public string Weight { get; set; } = "*";
        public string Color { get; set; } = "*";
        public string Material { get; set; } = "*";
        public List<Customers>? Customer { get; set; }
        public List<Specification> Specifications { get; set; } = new List<Specification>();
    }
    public class Customers
    {
        public required string Name { get; set; }
        public required string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
    public class Specification
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class ProductFactory
    {
        public static Product CreateProduct(string productType)
        {
            var product = new Product { Name = productType };

            switch (productType)
            {
                case "Electronics":
                    product.Specifications.Add(new Specification { Name = "Storage", Value = "128GB" });
                    product.Specifications.Add(new Specification { Name = "Battery Life", Value = "10 hours" });
                    break;

                case "Clothing":
                    product.Specifications.Add(new Specification { Name = "Size", Value = "M" });
                    product.Specifications.Add(new Specification { Name = "Fabric", Value = "Cotton" });
                    break;

                case "Digital":
                    product.Specifications.Add(new Specification { Name = "Compatibility", Value = "Windows, macOS" });
                    product.Specifications.Add(new Specification { Name = "License", Value = "Lifetime" });
                    break;

                default:
                    // Default specifications for general products
                    product.Specifications.Add(new Specification { Name = "Material", Value = "Plastic" });
                    product.Specifications.Add(new Specification { Name = "Dimensions", Value = "10x20x30cm" });
                    break;
            }

            return product;
        }
    }

}
