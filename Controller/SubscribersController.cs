using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Project_Internal.Data;


namespace WebApi_Project_Internal.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public SubscribersController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration; _context = context;
        }


         

    }
}
