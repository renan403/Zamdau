using API_Zamdau.Logger;
using API_Zamdau.User;

namespace API_Zamdau.DataBase
{

    public class Autentication : BaseFirebase
    {
        readonly UserZamdau _userZamdau;

        public Autentication()
        {
            _userZamdau = new UserZamdau();
        }
        private readonly List<string> ErrorMapping = ["INVALID_EMAIL", "INVALID_PASSWORD", "EMAIL_NOT_FOUND", "TOO_MANY_ATTEMPTS_TRY_LATER", "MISSING_PASSWORD", "WEAK_PASSWORD", "INVALID_LOGIN_CREDENTIALS"];

        public string GetErrors(string message)
        {
            foreach (var error in ErrorMapping)
            {
                if (message.Contains(error)) return error;
            }
            return "ERROR";
        }
        public async Task<AP_Auth> SignIn(string email, string senha)
        {
            try
            {
                var authL = await _authProvider.SignInWithEmailAndPasswordAsync(email, senha);

                return new AP_Auth(authenticated: authL.User.IsEmailVerified) { TokenID = (authL).FirebaseToken };
            }
            catch (Exception ex)
            {
                return new AP_Auth(GetErrors(ex.Message));
            }
        }
        public async Task<AP_Auth> CreateEmail(string email, string pwd, string name)
        {
            try
            {
                var create = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, pwd, sendVerificationEmail: true);

                await _userZamdau.RegisterUserDb(create.FirebaseToken, new AP_User()
                {
                    Email = email,
                    Name = name,
                    CreatedAt = create.Created
                });

                return new AP_Auth() { TokenID = create.FirebaseToken };
            }
            catch (Exception ex)
            {
                Log.SaveLog(ex.Message);
                return new AP_Auth(GetErrors(ex.Message));
            }
        }
        public async Task<bool> ReSendVerificationEmail(string tokenClient)
        {
            try
            {
                await _authProvider.SendEmailVerificationAsync(tokenClient);
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveLog(ex.Message);
                return false;
            }
        }
        public async Task<bool> ChangeEmail(string tokenClient, string newEmail)
        {
            try
            {
                await _authProvider.ChangeUserEmail(tokenClient, newEmail);
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> ChangePassword(string tokenClient, string newPWD)
        {
            try
            {
                await _authProvider.ChangeUserPassword(tokenClient, newPWD);
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> ResetPassword(string email)
        {
            try
            {
                await _authProvider.SendPasswordResetEmailAsync(email);
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{ex.Message}");
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
}
