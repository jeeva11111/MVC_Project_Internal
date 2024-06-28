using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApi_Project_Internal.Data;
using WebApi_Project_Internal.Models.UserModel;
//using WebApi_Project_Internal.SignalR;

namespace WebApi_Project_Internal.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        //private readonly IHubContext<HubNotifications> _hubContext;
        private readonly ApplicationDbContext _context;
        public NotificationController(ApplicationDbContext context)
        {
            //  _hubContext = hubContext;
            _context = context;
        }

        [HttpGet("GetNotify")]

        public async Task<IActionResult> Notification()
        {
            var list = await _context.Notifications.ToListAsync();


            return Ok(list);
        }
    }

    public class MessageModel
    {
        public string? Message { get; set; }
    }
}
