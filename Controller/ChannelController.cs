using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Channels;
using WebApi_Project_Internal.Models.BkViewModels;
using WebApi_Project_Internal.Models.UserModel;

namespace WebApi_Project_Internal.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public ChannelController(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;

            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ServerLink");
        }

        [HttpGet("GetAllChannels")]
        public async Task<IActionResult> GetAllChannels()
        {
            try
            {
                string query = "SELECT * FROM Channels";
                using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync(query);
                if (result.Any()) { return Ok(result); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("unable to process the get channel query");
        }


        [HttpPost("Create")]

        public async Task<IActionResult> CreateChannel([FromForm] Bk_ChennelViewModel model)
        {
            var currentUser = _contextAccessor.HttpContext.Session.GetString("UserId");

            if (currentUser == null)
            {
                return BadRequest("user Id is null");
            }


            var query = "INSERT INTO Channels (CurrentUserId, ChannelName, Description, CreatedAt) VALUES (@CurrentUserId, @ChannelName, @Description, @CreatedAt)";

            using var connection = new SqlConnection(_connectionString);
            var currentChannel = new WebApi_Project_Internal.Models.UserModel.Channel() { Categoery = model.Categoery, ChannelName = model.ChannelName, CreatedAt = DateTime.Now, CurrentUserId = Convert.ToInt32(currentUser), Description = model.Description };

            var result = await connection.ExecuteAsync(query, currentChannel);

            if (result > 0) { return Ok("Channel is created"); }

            return BadRequest("unable to create the channel");
        }

        // hello world
    }
}
