using API_Zamdau.User;
using Firebase.Auth;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Globalization;

namespace API_Zamdau.DataBase
{
    public class SellerZamdau : BaseFirebase
    {

        public async Task<bool> SaveSellerAsync(string id, string seller)
        {
            try
            {
                string idDecrypt = Helpers.Helpers.Decrypt(id);
                var objSeller = JsonConvert.DeserializeObject<AP_RegisterSeller>(seller);

                var emailID = await GetEmailID(idDecrypt);
                if (await (new UserZamdau()).SavePartialUserAsync(id, JsonConvert.SerializeObject(new AP_User() { IsSeller = true })))
                    await _client.Child("users").Child(emailID).Child("Seller").PutAsync(objSeller);
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
                var _tokenClientId = Helpers.Helpers.Decrypt(id);
                
                var newData = JsonConvert.DeserializeObject<AP_UpdateSeller>(seller);

                newData.ProfilePicture = ProfilePicture;


                var existingData = JsonConvert.DeserializeObject<AP_UpdateSeller>(await GetSellerAsync(_tokenClientId));

               var emailID = await GetEmailID(_tokenClientId);
               
               var updatedUser = new AP_UpdateSeller
               {
                   Description = newData.Description ?? existingData.Description,
                   Name = newData.Name ?? existingData.Name,
                   Email = newData.Email ?? existingData.Email,
                   Phone = newData.Phone ?? existingData.Phone,
                   ProfilePictureUrl = newData.ProfilePictureUrl ?? existingData.ProfilePictureUrl,
               };
               
               
               await _client.Child("users").Child(emailID).Child("Seller").PatchAsync(updatedUser);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> GetSellerAsync(string id)
        {
            try
            {
                string idDecrypt = Helpers.Helpers.Decrypt(id);
                var emailID = await GetEmailID(idDecrypt);

                return JsonConvert.SerializeObject(await _client.Child("users").Child(emailID).Child("Seller").OnceSingleAsync<AP_Seller>());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
