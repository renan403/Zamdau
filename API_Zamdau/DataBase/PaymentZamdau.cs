using API_Zamdau.User;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace API_Zamdau.DataBase
{

    public class PaymentZamdau : BaseFirebase
    {
        public async Task<bool> CreateOrderAsync(string jsonOrder)
        {
            try
            {

                AP_Orders order = JsonConvert.DeserializeObject<AP_Orders>(jsonOrder);

                var jsonUid = order.CustomerId;
                var uid = Helpers.Helpers.Decrypt(order.CustomerId);
                if (string.IsNullOrEmpty(uid))
                    return false;
                var _uid = await GetEmailID(uid);
                order.CustomerId = _uid;
              
                await _client.Child("orders").PostAsync(order);
                foreach(var i in order.Items)
                {
                   await new UserZamdau().RemoveItemCartAsync(jsonUid, i.ProductId);
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
           
        }
        public async Task<string?> GetOrdersAsync(string customerId)
        {
            try
            {

            
                var uid = Helpers.Helpers.Decrypt(customerId);
                if (string.IsNullOrEmpty(uid))
                    return null;
                var _uid = await GetEmailID(uid);


                var orders = (await _client.Child("orders").OnceAsync<AP_Orders>()).Where(u => u.Object.CustomerId.Contains(_uid)).Select( o => o.Object);
                var json = JsonConvert.SerializeObject(orders);
                return json;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public async Task<string?> FindOrderByIdAsync(string orderId)
        {
            try
            {
                await InitClientAdmin();
                var orders = (await _client.Child("orders").OnceAsync<AP_Orders>()).Where(u => u.Object.OrderNumber.Contains(orderId)).FirstOrDefault().Object;
                var json = JsonConvert.SerializeObject(orders);
                return json;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public async Task<bool> UpdateOrderStatusByIdAsync(string orderId, int status)
        {
            try
            {

                await InitClientAdmin();
                var orders = (await _client.Child("orders").OnceAsync<AP_Orders>()).Where(u => u.Object.OrderNumber.Contains(orderId)).FirstOrDefault();
                 await _client.Child("orders").Child(orders.Key).PatchAsync(new { PaymentStatus = status.ToString() });


                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public async Task<bool> SaveCheckoutAsync(string tokenClientId, string details)
        {
            try
            {
                var token = Helpers.Helpers.Decrypt(tokenClientId);

                var emailID = await GetEmailID(token);
                AP_CheckoutDetails check = JsonConvert.DeserializeObject<AP_CheckoutDetails>(details);

                await _client.Child("users").Child(emailID).Child("Checkout").PutAsync(check);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public async Task<bool> RemoveItemCheckoutAsync(string tokenClientId, string itemId)
        {
            var token = Helpers.Helpers.Decrypt(tokenClientId);
            var emailID = await GetEmailID(token);
            try
            {
                var items = await _client.Child("users").Child(emailID).Child("Checkout").Child("CartItems").OnceSingleAsync<List<AP_CartItem>>();
                var index = items.FindIndex(item => item.ProductId == itemId);

                await _client.Child("users").Child(emailID).Child("Checkout").Child("CartItems").Child($"{index}").DeleteAsync();
                await _client.Child("users").Child(emailID).Child("Cart").Child(itemId).DeleteAsync();

                var checkout = await _client.Child("users").Child(emailID).Child("Checkout").OnceSingleAsync<AP_CheckoutDetails>();

                decimal total = 0;
                decimal tax = 0;
                decimal subtotal = 0;

                foreach (var item in checkout.CartItems)
                {
                    subtotal += item.Price * item.Quantity;
                }

                tax = subtotal * 0.10m;
                total = subtotal + tax;

                checkout.Tax = tax;
                checkout.Subtotal = subtotal;
                checkout.Total = total;

                await _client.Child("users").Child(emailID).Child("Checkout").PutAsync(checkout);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        public async Task<string> GetCheckoutAsync(string tokenClientId)
        {
            var token = Helpers.Helpers.Decrypt(tokenClientId);
            var emailID = await GetEmailID(token);
            try
            {
                var details = await _client.Child("users").Child(emailID).Child("Checkout").OnceSingleAsync<AP_CheckoutDetails>();
                details.CustomerAddress = JsonConvert.DeserializeObject<AP_Address>(await new UserZamdau().GetAddress(tokenClientId));
                

                var get = new AP_GetCheckoutDetails() { Details = details };

                var jsonInfoUser = JsonConvert.SerializeObject(get);
                return jsonInfoUser;
            }
            catch (Exception ex)
            {

                throw;
            }



        }
    }
}
