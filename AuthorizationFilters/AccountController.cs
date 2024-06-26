using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using WebApi_Project_Internal.Models;
using Microsoft.Data.SqlClient;
using BackEnd.AuthorizationFilters.AuthFilter;
using BackEnd.Middleware;
using WebApi_Project_Internal.AuthorizationFilters.Services;
using WebApi_Project_Internal.Models.BkViewModels;
using System.Reflection.Metadata.Ecma335;
using System.Linq;
using WebApi_Project_Internal.Models.UserModel;
using WebApi_Project_Internal.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

[ApiController]
[Route("api/[controller]")]
//[ServiceFilter(typeof(ApiKeyValidation))]
//[ServiceFilter(typeof(ErrorHandler))]

public class AccountController : ControllerBase
{
    private readonly string _connectionstring;
    private readonly IConfiguration _configuration;
    private readonly IAccountServices _accountServices;
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;

    public AccountController(IConfiguration configuration, IAccountServices accountServices, ApplicationDbContext context, IHttpContextAccessor contextAccessor)
    {
        _configuration = configuration;
        _connectionstring = _configuration.GetConnectionString("ServerLink");
        _accountServices = accountServices;
        _context = context;
        _contextAccessor = contextAccessor;
    }

    [HttpGet, Route("Get")]
    public async Task<IActionResult> GetPost()
    {
        var query = "SELECT * FROM AddUser";
        using var connection = new SqlConnection(_connectionstring);
        var content = await connection.QueryAsync<AddUsers>(query);

        if (content.Any())
        {
            return Ok(content);
        }

        return BadRequest("user doesn't exist");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] BK_LoginViewModel login)
    {
        if (!string.IsNullOrWhiteSpace(login.Email) && !string.IsNullOrWhiteSpace(login.Password))
        {
            var user = _context.AddUser.SingleOrDefault(u => u.Email == login.Email.ToLower());
            if (user != null)
            {
                var isValid = _accountServices.IsValidUser(login.Email, login.Password);
                if (isValid)
                {
                    var cookieUser = _contextAccessor.HttpContext.Request.Cookies["UserId"];
                    CookieOptions options = new CookieOptions() { HttpOnly = true, Expires = DateTime.Now.AddMinutes(5), Secure = true };

                    _contextAccessor.HttpContext.Response.Cookies.Append("UserId", user.UserId.ToString(), options);
                    return Ok(new { UserId = user.UserId });
                }
            }
        }
        return BadRequest(new { Message = "Invalid login attempt" });
    }




    [HttpPost("Register")]

    public async Task<IActionResult> Resigter(BK_RegesterViewModel register)
    {
        if (ModelState.IsValid)
        {
            var isExisting = (from x in _context.AddUser
                              where x.Email == register.Email
                              select new { email = x }).SingleOrDefault();

            if (isExisting == null)
            {
                string passwordSalt = _accountServices.GenerateSalt();
                string passwordHash = _accountServices.HashPassword(register.Password, passwordSalt);

                _context.AddUser.Add(new User_()
                {
                    Email = register.Email,
                    UserName = register.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,

                });

                _context.SaveChanges();
                return Ok(true);
            }
            return BadRequest("User already exists.");

        }

        return BadRequest("Unable to Add the model in DB");
    }

}
