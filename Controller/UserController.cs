using BackEnd.AuthorizationFilters.AuthFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project_Internal.Data;
using MVC_Project_Internal.Filters;

namespace MVC_Project_Internal.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    // [ServiceFilter(typeof(ApiKeyValidation))]

    public class UserController : ControllerBase
    {
        private readonly IUserAccount _userAccount;
        private readonly ApplicationDbContext _context;
        public UserController(IUserAccount userAccount, ApplicationDbContext context)
        {
            _userAccount = userAccount;
            _context = context;
        }
        //Getting the user
        [HttpGet, Route("GetUsers")]

        // Adding new user
        public async Task<IActionResult> GetUser()
        {
            var currentUsers = await _userAccount.GetUsers();
            if (currentUsers.Count() > 0)
            {
                return Ok(currentUsers);
            }
            return BadRequest("unable to read the user information");
        }
        [HttpPost, Route("AddUser")]
        public async Task<IActionResult> AddUser([FromForm] AddUserModel userModel)
        {
            if (userModel == null) { return BadRequest("unable to read the data"); }
            _userAccount.AddUser(userModel);
            return Ok(userModel);
        }

        // Updating the User 
        [HttpPut, Route("updateUser")]
        public async Task<IActionResult> UpdateUser([FromForm] AddUserModel model)
        {
            if (model.Id <= 0) { return BadRequest("unable to read the Id"); }
            if (model == null)
            {
                //var user = new AddUserModel() { Email = "hello", Name = "user", Password = "password", Id = 0 };
                return BadRequest("unable to read the model");
            }
            var updatedUser = await _userAccount.UpdateUser(model);


            return updatedUser > 0 ? Ok("model is update successfully") : BadRequest("unable to update");
        }
    }

}