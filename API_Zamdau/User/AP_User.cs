namespace API_Zamdau.User
{
    public class AP_User
    {
        public string? Name { get; set; }  
        public string? SellerName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? Pwd { get; set; }
        public int? Age { get; set; }
        public bool IsSeller { get; set; }
        public DateTime CreatedAt { get; set; }


    }
    public class AP_Address
    {

        public string Recipient { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
    }
    public class AP_Cart
    {
        public AP_Product Product { get; set; }
        public int Quantity { get; set; }
        public bool IsSelected { get; set; }
    }
    public class IdCart
    {
        public bool IsSelected { get; set; } 
        public int Quantity { get; set; }
    }

}
