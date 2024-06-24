using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Project_Internal.Data;
using WebApi_Project_Internal.Models.BkViewModels;
using WebApi_Project_Internal.Models.UserModel;

namespace WebApi_Project_Internal.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommintesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public CommintesController(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;

        }
        [HttpPost, Route("PostCommints")]
        public async Task<IActionResult> PostCommints(int videoId, Bk_Commints commints)
        {

            if (videoId <= 0)
            {
                return BadRequest("Video Id is invalid ");
            }

            var currentUser = _contextAccessor.HttpContext.Session.GetString("UserId");

            if (currentUser == null)
            {
                return NoContent();
            }

            var currentVideoToComment = (from x in _context.Videos where x.CurrentUserId == Convert.ToInt32(currentUser) select new { x.CurrentUserId }).FirstOrDefault().CurrentUserId;


            if (currentUser == null)
            {
                return BadRequest("unable to find the current video to comment");
            }
            var currentCommints = new Comment() { CommentText = commints.CommentText, CreatedAt = commints.CreatedAt, VideoId = videoId, UserId = Convert.ToInt32(currentUser) };
            if (ModelState.IsValid)
            {
                _context.Comments.Add(currentCommints);
                _context.SaveChanges();
                return Ok("Comments is added");
            }
            return StatusCode(234," Failed to commint ");
        }



      //  public async Task<IActionResult>










        private bool CommintExist(int id)
        {
            return _context.Comments.Any(x => x.VideoId == id);
        }

    }
}
