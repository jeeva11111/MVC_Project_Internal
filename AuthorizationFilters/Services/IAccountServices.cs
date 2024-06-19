namespace WebApi_Project_Internal.AuthorizationFilters.Services
{
    public interface IAccountServices
    {
        bool IsValidUser(string Email, string password);
        string GenerateSalt();
        string HashPassword(string password, string salt);
    }
}
