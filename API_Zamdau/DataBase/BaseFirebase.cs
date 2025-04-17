using API_Zamdau.Logger;
using Firebase.Auth;
using Firebase.Database;


namespace API_Zamdau.DataBase
{

    public abstract class BaseFirebase
    {
        protected readonly FirebaseAuthProvider _authProvider;
        protected FirebaseClient _client;
        protected const string apiKey = "******";

        public enum Path
        {
            Products = 1,
            Sellers = 2
        };

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

                _client = new FirebaseClient("https://projetoport-50b66-default-rtdb.firebaseio.com/", new FirebaseOptions()
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
      // public string  Serialize<T>(T item)
      // {
      //     return JsonConvert.SerializeObject(item); ;
      // }
      // public T Deserialize<T>(string item)
      // {
      //     return JsonConvert.DeserializeObject<T>(item); 
      // }
        protected string DecriptID(string id) => Helpers.Helpers.Decrypt(id);
        /// <summary>
        /// Method used to avoid requiring the user's UserID.
        /// </summary>
        protected async Task InitClientAdmin() {

            var token = (await (new Autentication()).SignIn("zamdaumarketplace@gmail.com", Helpers.Helpers.Encrypt("123456"))).TokenID;
       
            _client = new FirebaseClient("https://projetoport-50b66-default-rtdb.firebaseio.com/", new FirebaseOptions()
            {
                AuthTokenAsyncFactory = () => Task.FromResult(token),
            });


        } 
        
    }   
}
