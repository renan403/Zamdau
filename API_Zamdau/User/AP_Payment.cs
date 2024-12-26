using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API_Zamdau.User
{
    public class AP_Orders
    {

        public string CustomerId { get; set; }
        public AP_Address CustomerAddress { get; set; }
        public List<AP_CartItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime Created { get; set; }
        public string OrderNumber { get; set; }
   


    }

    public class AP_CheckoutDetails
    {
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public List<AP_CartItem> CartItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public AP_Address? CustomerAddress { get; set; }

        public AP_CheckoutDetails()
        {
            CartItems = [];
        }
    }
    public class AP_GetCheckoutDetails
    {
        public AP_CheckoutDetails? Details { get; set; }
        public AP_PaymentInfo PaymentMethod { get; set; }


    }
    public class AP_PaymentInfo
    {
        public string SelectedPaymentMethod { get; set; }
    }
    public class AP_CartItem
    {
        public bool Commented { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string IsSelected { get; set; }
    }
}
