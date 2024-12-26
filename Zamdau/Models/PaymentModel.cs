using API_Zamdau.User;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Zamdau.Models
{
    public class Card
    {

        public string OrderNumber { get; set; } // "Credit" or "Debit"
        public string PaymentType { get; set; } // "Credit" or "Debit"
        public int? Installments { get; set; } // Number of installments (null if not applicable)

        [Required]
        public string CardholderName { get; set; } // Name of the cardholder
        [Required]
        [StringLength(19, MinimumLength = 13, ErrorMessage = "Card number must be between 13 and 19 digits.")]
        [RegularExpression(@"^\d{13,19}$", ErrorMessage = "Invalid card number. Please enter only digits.")]
        public string CardNumber { get; set; } // Card number
        [Required]
        [StringLength(5, ErrorMessage = "Expiry date must be in MM/YY format.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Expiry date must be in MM/YY format.")]
        public string ExpiryDate { get; set; } // Expiry date in MM/YY format
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV must be 3 digits.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Invalid CVV. It must be exactly 3 digits.")]
        public string CVV { get; set; } // CVV code
        public decimal Amount { get; set; } // Total amount to be paid
    }

    public class GetCheckoutDetails
    {
        public CheckoutDetails? Details { get; set; }
        public PaymentInfo PaymentMethod { get; set; }

    }

    public enum PaymentStatus
    {
        Completed,
        Processing,
        Shipped,
        Pending,
        Cancelled
    }
    public class Pix
    {
        public string OrderNumber { get; set; }
    }

    public class Orders
    {
        public string CustomerId { get; set; }
        public Address CustomerAddress { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime Created { get; set; }
        public string OrderNumber { get; set; }
       
    }
    public class PaymentInfo
    {
        public string SelectedPaymentMethod { get; set; }
    }
    public class CheckoutDetails
    {
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }

        public Address? CustomerAddress { get; set; }
        [Required]
        public List<CartItem> CartItems { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Subtotal must be greater than zero.")]
        public decimal Subtotal { get; set; }

        [Required]
        [Range(0.00, double.MaxValue, ErrorMessage = "Tax cannot be negative.")]
        public decimal Tax { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total must be greater than zero.")]
        public decimal Total { get; set; }



        public CheckoutDetails()
        {
            CartItems = new List<CartItem>();
        }
    }


    public class CartItem
    {
        public bool Commented { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }

        [Required]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string ProductImageUrl { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }


        public string? IsSelected { get; set; }

    }

}
