using API_Zamdau.User;
using Firebase.Auth;
using Firebase.Database.Query;
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
