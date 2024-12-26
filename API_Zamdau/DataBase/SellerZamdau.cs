using API_Zamdau.User;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace API_Zamdau.DataBase
{
    public class SellerZamdau : BaseFirebase
    {
        public async Task DeleteProductSellerAsync(string id, string guid)
        {
            var _id = DecriptID(id);
            var emailID = await GetEmailID(_id);
            var product = await _client.Child("products").Child(guid).OnceSingleAsync<AP_Product>();

            if (product.Uid == emailID)
            {
                await _client.Child("products").Child(guid).DeleteAsync();
                if (product.ImageUrl != null)
                    await (new StorageFirebase().DeleteOneImageAsync(_id, emailID, Path.Products, product.ImageUrl));

            }

            //await (new StorageFirebase().DeleteOneImageAsync(_tokenClientId, emailID, Path.Sellers, guid));
        }
        public async Task<bool> CreateProduct(string id, string product, IFormFile ImageUrl)
        {
            try
            {
                var _guid = Guid.NewGuid();
                var _id = DecriptID(id);
                var emailID = await GetEmailID(_id);
                var _product = JsonConvert.DeserializeObject<AP_Product>(product);
                _product.Uid = emailID;
                _product.Id = _guid.ToString();
                var url = await (new StorageFirebase()).UploadImage(_id, Path.Products, ImageUrl, emailID);
                _product.ImageUrl = url;
                await _client.Child("products").Child(_guid.ToString()).PutAsync(_product);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> SaveSellerAsync(string id, string seller)
        {
            try
            {
                string idDecrypt = Helpers.Helpers.Decrypt(id);
                var objSeller = JsonConvert.DeserializeObject<AP_RegisterSeller>(seller);

                var emailID = await GetEmailID(idDecrypt);
                objSeller.Uid = emailID;


                if (await (new UserZamdau()).SavePartialUserAsync(id, JsonConvert.SerializeObject(new AP_User() { IsSeller = true, SellerName = objSeller.Name })))
                    await _client.Child("sellers").Child(objSeller.Name).PutAsync(objSeller);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateSellerAsync(string id, string seller, IFormFile ProfilePicture)
        {
            try
            {
                string? url = null;

                var _tokenClientId = Helpers.Helpers.Decrypt(id);
                var newData = JsonConvert.DeserializeObject<AP_UpdateSeller>(seller);
                var existingData = JsonConvert.DeserializeObject<AP_UpdateSeller>(await GetSellerAsync(id));
                var emailID = await GetEmailID(_tokenClientId);

                if (ProfilePicture is not null && emailID is not null)
                    url = await (new StorageFirebase().UploadImage(_tokenClientId, Path.Sellers, ProfilePicture, emailID));

                var updatedUser = new AP_UpdateSeller
                {
                    Description = newData.Description ?? existingData.Description,
                    Name = newData.Name ?? existingData.Name,
                    Email = newData.Email ?? existingData.Email,
                    Phone = newData.Phone ?? existingData.Phone,
                    ProfilePictureUrl = url ?? existingData.ProfilePictureUrl,
                    Uid = existingData.Uid
                };
                if (newData.Name == existingData.Name)
                {
                    if (!string.IsNullOrEmpty(await GetSellerByNameAsync(id, existingData.Name)))
                    {
                        await _client.Child("sellers").Child(existingData.Name).PatchAsync(updatedUser);
                        if (url != null && existingData.ProfilePictureUrl is not null)
                            await (new StorageFirebase().DeleteOneImageAsync(_tokenClientId, emailID, Path.Sellers, existingData.ProfilePictureUrl));
                        return true;
                    }
                }
                else if (!string.IsNullOrEmpty(newData.Name))
                    if (await GetSellerByNameAsync(id, newData.Name) == "null")
                    {
                        await _client.Child("sellers").Child(updatedUser.Name).PatchAsync(updatedUser);
                        (new UserZamdau()).SavePartialUserAsync(id, JsonConvert.SerializeObject(new AP_User() { SellerName = updatedUser.Name }));
                        await _client.Child("sellers").Child(existingData.Name).DeleteAsync();
                        return true;
                    }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string?> GetSellerByNameAsync(string id, string name)
        {
            var idDecrypt = Helpers.Helpers.Decrypt(id);
            var emailID = await GetEmailID(idDecrypt);
            if (emailID is not null)
            {
                string? json = JsonConvert.SerializeObject(await _client.Child("sellers").Child(name).OnceSingleAsync<AP_Seller?>());
                return json;
            }

            return null;
        }
        public async Task<string> GetSellerAsync(string id)
        {
            try
            {
                string idDecrypt = Helpers.Helpers.Decrypt(id);
                var emailID = await GetEmailID(idDecrypt);
                var user = JsonConvert.DeserializeObject<AP_User>(await (new UserZamdau()).GetUser(idDecrypt));
                var seller = await _client.Child("sellers").Child(user.SellerName).OnceSingleAsync<AP_Seller>();
                seller.Products = await GetProductByIdSeller(emailID);
                string json = JsonConvert.SerializeObject(seller);
                return json;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        private async Task<List<AP_Product>> GetProductByIdSeller(string idSeller)
        {


            try
            {
                var products = await _client.Child("products").OnceAsync<AP_Product>();
                List<AP_Product> productList = products
                .Where(product => product.Object.Uid == idSeller) // Filtra pelo Uid
                .Select(product => product.Object) // Extrai apenas o objeto do tipo AP_Product
                .ToList();
                return productList;
            }
            catch (Exception ex)
            {
                return new();
            }


            //List<AP_Product> product1 = (await _client.Child("products").OnceAsync<AP_Product>()).Where(id => id.Object.Uid == idSeller).ToList<AP_Product>();

        }

        private async Task<AP_Seller> GetObjSeller(string id)
        {
            try
            {
                string idDecrypt = DecriptID(id);
                var emailID = await GetEmailID(idDecrypt);
                var user = JsonConvert.DeserializeObject<AP_User>(await (new UserZamdau()).GetUser(idDecrypt));
                var seller = await _client.Child("sellers").Child(user.SellerName).OnceSingleAsync<AP_Seller>();
                return seller;
            }
            catch (Exception ex)
            {
                return new AP_Seller();
            }
        }

    }
}
