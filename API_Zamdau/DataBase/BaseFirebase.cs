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

        protected const string apiKey = "AIzaSyDbUmirlVMrH3LNitSahxl2-gbAh9Vxs70";

        protected BaseFirebase()
        {
            try
            {
                _authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }

    public class Autentication : BaseFirebase
    {
        public Autentication() : base(){ }

        public async Task<string> SignIn(string email, string senha) 
        {
            try
            {
                return (await _authProvider.SignInWithEmailAndPasswordAsync(email, senha)).FirebaseToken;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("INVALID_PASSWORD"))
                    return "INVALID_PASSWORD";
                if (ex.Message.Contains("EMAIL_NOT_FOUND"))
                    return "EMAIL_NOT_FOUND";
                if (ex.Message.Contains("TOO_MANY_ATTEMPTS_TRY_LATER"))
                    return "TOO_MANY_ATTEMPTS_TRY_LATER";

                return "ERROR";
            }         
        }

        public async Task<bool> CreateEmail(string email, string senha)
        {
            try
            {
               
                var create = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, senha, sendVerificationEmail: true);
               
                var dtCreate = create.Created;
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveLog(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteEmail(string tokenClient)
        {
            try
            {
                await _authProvider.DeleteUserAsync(tokenClient);
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveLog(ex.Message);
                return false;
            }
        }
    }
    public class UserZamdau : BaseFirebase // Class with only functions about users
    {
        private FirebaseClient _client;

        public async Task<string> Login(string email, string pwd) 
        {
            try
            {
                return (await (new Autentication()).SignIn(email, pwd));
            }
            catch (Exception ex)
            {
                Log.SaveLog(ex.Message);
                if (ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
                    return "Error : INVALID_LOGIN_CREDENTIALS";
                return $"Error : {ex.Message}";
            }           
        } 

        public async Task<string?> GetEmailID(string tokenClient)
        {
            try
            {
                var id = (await _authProvider.GetUserAsync(tokenClient)).LocalId;

                _client = new FirebaseClient("https://zamdau-f3b04-default-rtdb.firebaseio.com/", new FirebaseOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(id)
                });
                return id;
            }
            catch (Exception ex)
            {
                Log.SaveLog(ex.Message);
                return null;
            }
            
        }

        private async Task<bool> Insert( string tokenClientId, UserRepository user)
        {
            try
            {
                var userID = await GetEmailID(tokenClientId);
                await _client.Child("users").Child(userID).PutAsync<UserRepository>(user);
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{ex.Message}");
                return false;
            }
            
        } 

        public async Task CreateDirUser(string email, string senha)
        {
                
           // _client.Child("users").
        }

    }
}
