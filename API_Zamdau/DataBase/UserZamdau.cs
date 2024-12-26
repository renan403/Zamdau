using API_Zamdau.Logger;
using API_Zamdau.User;
using Firebase.Database.Query;
using Newtonsoft.Json;

namespace API_Zamdau.DataBase
{
    public class UserZamdau : BaseFirebase // Class with only functions about users
    {
        public async Task<bool> AddComment(string jsonComment)
        {
            try
            {
                var comment = JsonConvert.DeserializeObject<AP_Comment>(jsonComment);

                
                await _client.Child("products").Child(comment.ProductId).Child("Comments").PostAsync(comment);

                var order = (await _client.Child("orders").OnceAsync<AP_Orders>()).Where(u => u.Object.OrderNumber.Contains(comment.OrderNumber)).FirstOrDefault();

                var index = order.Object.Items.FindIndex(p => p.ProductId == comment.ProductId);

                await _client.Child("orders").Child(order.Key).Child("Items").Child($"{index}").PatchAsync(new { Commented = true });
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public async Task<bool> AddToCartAsync(string tokenClientId, string ProductId, string Quantity)
        {
            try
            {
                var emailID = await GetEmailID(Helpers.Helpers.Decrypt(tokenClientId));

                await _client.Child("users").Child(emailID).Child("Cart").Child(ProductId).PutAsync(new { Quantity, IsSelected = true });
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public async Task<string> RegisteAddress(string tokenClientId, string address)
        {
            try
            {

                var emailID = await GetEmailID(Helpers.Helpers.Decrypt(tokenClientId));
                var _address = JsonConvert.DeserializeObject<AP_Address>(address);
                await _client.Child("users").Child(emailID).Child("Address").PutAsync(_address);
                return "Successful";
            }
            catch (Exception)
            {
                return "Unable to register a new address";
            }

        }
        public async Task<bool> DeleteAddress(string tokenClientId)
        {
            try
            {

                var emailID = await GetEmailID(Helpers.Helpers.Decrypt(tokenClientId));

                await _client.Child("users").Child(emailID).Child("Address").DeleteAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> UpdateAddress(string tokenClientId, string address)
        {
            try
            {

                var emailID = await GetEmailID(Helpers.Helpers.Decrypt(tokenClientId));
                var newAddress = JsonConvert.DeserializeObject<AP_Address>(address);
                var oldAddress = JsonConvert.DeserializeObject<AP_Address>(await GetAddress(tokenClientId));
                var _address = new AP_Address()
                {
                    Recipient = newAddress.Recipient ?? oldAddress.Recipient,
                    Street = newAddress.Street ?? oldAddress.Street,
                    State = newAddress.State ?? oldAddress.State,
                    Zip = newAddress.Zip ?? oldAddress.Zip,
                    Country = newAddress.Country ?? oldAddress.Country,
                };
                await _client.Child("users").Child(emailID).Child("Address").PatchAsync(_address);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<string> GetAddress(string tokenClientId)
        {
            try
            {

                var emailID = await GetEmailID(Helpers.Helpers.Decrypt(tokenClientId));

                var address = await _client.Child("users").Child(emailID).Child("Address").OnceSingleAsync<AP_Address>();
                var _address = JsonConvert.SerializeObject(address);
                return _address;
            }
            catch (Exception)
            {
                return "Address not found";
            }

        }
        public async Task<bool> RemoveItemCartAsync(string tokenClientId, string ProductId)
        {
            try
            {
                var emailID = await GetEmailID(Helpers.Helpers.Decrypt(tokenClientId));
                await _client.Child("users").Child(emailID).Child("Cart").Child(ProductId).DeleteAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<AP_Auth> Login(string email, string password)
        {
            return await (new Autentication()).SignIn(Helpers.Helpers.Decrypt(email), Helpers.Helpers.Decrypt(password));
        }
        public async Task<bool> RegisterUserDb(string tokenClientId, AP_User user)
        {
            try
            {
                user.Pwd = null;
                var emailID = await GetEmailID(tokenClientId);
                await _client.Child("users").Child(emailID).PutAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{ex.Message}");
                return false;
            }

        }
        public async Task<string> GetCartUser(string tokenClientId)
        {
            var emailID = await GetEmailID(tokenClientId);
            var cart = await _client.Child("users").Child(emailID).Child("Cart").OnceAsync<IdCart>();
            //List<AP_Cart> returnCart = [];
            List<AP_CartItem> returnCart = [];

            foreach (var item in cart)
            {
                var prod = JsonConvert.DeserializeObject<AP_Product>(await (new ProductZamdau()).GetProductAsync(item.Key));
                var productInCart = new AP_CartItem()
                {
                    ProductId = prod.Id,
                    Price = (decimal)prod.Price,
                    ProductImageUrl = prod.ImageUrl,
                    ProductName = prod.Name,
                    IsSelected = item.Object.IsSelected.ToString(),
                    Quantity = item.Object.Quantity,
                };


                // returnCart.Add(new AP_Cart() { Product = prod, Quantity = item.Object.Quantity ,IsSelected = item.Object.IsSelected});
                returnCart.Add(productInCart);
            };

            var jsonInfoUser = JsonConvert.SerializeObject(returnCart);
            return jsonInfoUser;
        }
        public async Task<string> GetUser(string tokenClientId)
        {
            var emailID = await GetEmailID(tokenClientId);
            var user = await _client.Child("users").Child(emailID).OnceSingleAsync<AP_User>();

            var jsonInfoUser = JsonConvert.SerializeObject(user);
            return jsonInfoUser;
        }
        public async Task<bool> SaveUserAsync(string tokenClientId, string user)
        {
            var emailID = await GetEmailID(Helpers.Helpers.Decrypt(tokenClientId));
            await _client.Child("users").Child(emailID).PatchAsync(JsonConvert.DeserializeObject<AP_User>(user));
            return true;
        }
        public async Task<bool> SavePartialUserAsync(string tokenClientId, string user)
        {

            var _tokenClientId = Helpers.Helpers.Decrypt(tokenClientId);
            var newData = JsonConvert.DeserializeObject<AP_User>(user);

            var existingData = JsonConvert.DeserializeObject<AP_User>(await GetUser(_tokenClientId));
            var emailID = await GetEmailID(_tokenClientId);

            var updatedUser = new AP_User
            {
                Age = newData.Age ?? existingData.Age,
                Name = newData.Name ?? existingData.Name,
                Email = newData.Email ?? existingData.Email,
                Gender = newData.Gender ?? existingData.Gender,
                Phone = newData.Phone ?? existingData.Phone,
                CreatedAt = existingData.CreatedAt,
                IsSeller = existingData.IsSeller ? existingData.IsSeller : newData.IsSeller,
                SellerName = newData.SellerName ?? existingData.SellerName,

            };


            await _client.Child("users").Child(emailID).PatchAsync(updatedUser);
            return true;
        }
    }
}
