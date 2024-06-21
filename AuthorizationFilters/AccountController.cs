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

[ApiController]
[Route("api/[controller]")]
//[ServiceFilter(typeof(ApiKeyValidation))]
[ServiceFilter(typeof(ErrorHandler))]

public class AccountController : ControllerBase
{
    private readonly string _connectionstring;
    private readonly IConfiguration _configuration;

    public AccountController(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionstring = _configuration.GetConnectionString("ServerLink");
    }

    [HttpGet, Route("Get")]
    public async Task<IActionResult> GetPost()
    {
        var query = "SELECT * FROM AddUsers";
        using var connection = new SqlConnection(_connectionstring);
        var content = await connection.QueryAsync<AddUsers>(query);

        if (content.Any())
        {
            return Ok(content);
        }

        return BadRequest("user doesn't exist");
    }
    [HttpGet, Route("Error")]
    public Task<IActionResult> ErroMessage()
    {
        throw new Exception("unable to return the ErrorMessage");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel login)
    {
        if (!string.IsNullOrWhiteSpace(login.Email) && !string.IsNullOrWhiteSpace(login.Password))
        {
            var isValid = _accountServices.IsValidUser(login.Email, login.Password);

            if (isValid)
            {
                return Ok(true);
            }
        }
        ModelState.AddModelError("", "Invalid user inputs");

        return BadRequest("Invalid user inputs");
    }


}
