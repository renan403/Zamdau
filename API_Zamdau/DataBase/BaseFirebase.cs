using API_Zamdau.Logger;
using API_Zamdau.User;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System.ComponentModel.DataAnnotations;


namespace API_Zamdau.DataBase
{

    public abstract class BaseFirebase
    {
        protected readonly FirebaseAuthProvider _authProvider;
        protected FirebaseClient _client;
        protected const string apiKey = "AIzaSyDbUmirlVMrH3LNitSahxl2-gbAh9Vxs70";

        protected BaseFirebase()
        {
            try
            {
                _authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            }
            catch (Exception)
            {
                throw;
            }
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
    }   
}
