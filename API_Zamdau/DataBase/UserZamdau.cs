using API_Zamdau.Logger;
using API_Zamdau.User;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

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
        public async Task<AP_User> InfoUser(string tokenClientId)
        {
            var emailID = await GetEmailID(tokenClientId);
            var user = await _client.Child("users").Child(emailID).OnceSingleAsync<AP_User>();

            return user ?? new AP_User();
        }

    }
}
