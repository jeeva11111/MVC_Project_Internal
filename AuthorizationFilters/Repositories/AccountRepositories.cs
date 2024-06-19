using Microsoft.Identity.Client;
using MVC_Project_Internal.Data;
using WebApi_Project_Internal.AuthorizationFilters.Services;

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
            throw new NotImplementedException();
        }

        public string HashPassword(string password, string salt)
        {
            throw new NotImplementedException();
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
