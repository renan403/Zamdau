
using System.Runtime.Serialization;

namespace API_Zamdau.User
{
    public class AP_Auth(string? erro = null, bool authenticated = false)
    {
        public string? TokenID {  get; set; }
        public bool IsAuthenticated { get; } = authenticated;
        public string? Error { get; } = erro;
    }
}
