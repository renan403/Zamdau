using API_Zamdau.Logger;
using API_Zamdau.User;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using API_Zamdau.Helpers;
using System.Text.Json;

namespace API_Zamdau.DataBase
{
    public class UserZamdau : BaseFirebase // Class with only functions about users
    {
        private FirebaseClient _client;

        public async Task<AP_Auth> Login(string email, string password)
        {
            return await (new Autentication()).SignIn(email, password);
        }
        public async Task<string?> GetEmailID(string tokenClient)
        {
            try
            {
                var id = (await _authProvider.GetUserAsync(tokenClient)).LocalId;

                _client = new FirebaseClient("https://zamdau-f3b04-default-rtdb.firebaseio.com/", new FirebaseOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(tokenClient),
                });
                return id;
            }
            catch (Exception ex)
            {
                Log.SaveLog(ex.Message);
                return null;
            }
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
            };


            await _client.Child("users").Child(emailID).PatchAsync(updatedUser);
            return true;
        }
    }
}
