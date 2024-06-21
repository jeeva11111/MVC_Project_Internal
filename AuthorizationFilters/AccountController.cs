using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using WebApi_Project_Internal.Models;
using Microsoft.Data.SqlClient;
using BackEnd.AuthorizationFilters.AuthFilter;

[ApiController]
[Route("api/[controller]")]
//[ServiceFilter(typeof(ApiKeyValidation))]
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

}
