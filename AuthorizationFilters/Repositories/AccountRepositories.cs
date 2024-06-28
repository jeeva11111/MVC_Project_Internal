using Microsoft.Identity.Client;
using System.Security.Cryptography;
using System.Text;
using WebApi_Project_Internal.AuthorizationFilters.Services;
using WebApi_Project_Internal.Data;

namespace WebApi_Project_Internal.AuthorizationFilters.Repositories
{
    public class AccountRepositories : IAccountServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public AccountRepositories(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        public string GenerateSalt()
        {
            var saltBytes = new byte[16];
            using (var provider = new RNGCryptoServiceProvider()) { provider.GetBytes(saltBytes); }

            return Convert.ToBase64String(saltBytes);
        }

        public string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {

                var saltedPassword = password + salt;
                byte[] saltedPasswordByte = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hasBytes = sha256.ComputeHash(saltedPasswordByte);
                return Convert.ToBase64String(hasBytes);
            }

        }
        public bool IsValidUser(string Email, string password)
        {
            bool isValid = false;
            var user = _context.AddUser.SingleOrDefault(u => u.Email == Email.ToLower());

            if (user != null)
            {
                var hash = HashPassword(password, user.PasswordSalt);
                if (user.PasswordHash == hash)
                {
                    _httpContext.HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    _httpContext.HttpContext.Session.SetString("UserName", user.UserName);
                    isValid = true;
                }
            }

            return isValid;
        }


    }

}



