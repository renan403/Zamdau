namespace Zamdau.Interfaces
{
    public interface IUser
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

        Task<bool> RegisterAccount();
        Task<bool> ResetAccess();
        Task<bool> ConfirmAccount();
        Task<bool> DeleteAccount();
    }
}
