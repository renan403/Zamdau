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
}
