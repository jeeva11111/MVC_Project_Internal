using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVC_Project_Internal.Data;
using WebApi_Project_Internal.Models.BkViewModels;

namespace WebApi_Project_Internal.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public VideoController(IHttpContextAccessor contextAccessor, ApplicationDbContext context, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ServerLink");

        }

        [HttpPost("uploadVideo")]
        public async Task<IActionResult> PostVideo([FromForm] Bk_VideoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _contextAccessor.HttpContext.Session.GetString("UserId");
                if (userId == null)
                {
                    return BadRequest("UserId is required.");
                }

                var channelId = _context.Channels
                    .Where(x => x.CurrentUserId == Convert.ToInt32(userId))
                    .Select(x => x.ChannelId)
                    .SingleOrDefault();

                if (channelId == 0)
                {
                    return BadRequest("No channel found for the user.");
                }

                var videoModel = new Models.UserModel.Video
                {
                    Description = model.Description,
                    Categoery = model.Categoery,
                    Title = model.Title,
                    CurrentUserId = Convert.ToInt32(userId),
                    VideoChannelId = channelId,
                    CreatedAt = DateTime.UtcNow
                };

                // Image upload
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    using var stream = new MemoryStream();
                    await model.ImageFile.CopyToAsync(stream);
                    videoModel.ImageData = stream.ToArray();
                    videoModel.ImageName = model.ImageFile.FileName;
                }

                // Video Upload
                if (model.VideoFile != null && model.VideoFile.Length > 0)
                {
                    using var stream = new MemoryStream();
                    await model.VideoFile.CopyToAsync(stream);
                    videoModel.VideoData = stream.ToArray();
                    videoModel.VideoName = model.VideoFile.FileName;
                }

                var insertVideoQuery = "INSERT INTO Videos (Title, Description, CreatedAt, ImageData, ImageName, VideoData, VideoName, Categoery, VideoChannelId, CurrentUserId) VALUES (@Title, @Description, @CreatedAt, @ImageData, @ImageName, @VideoData, @VideoName, @Categoery, @VideoChannelId, @CurrentUserId)";

                using var connection = new SqlConnection(_connectionString);
            
                var result = await connection.ExecuteAsync(insertVideoQuery, new
                {
                    videoModel.Title,
                    videoModel.Description,
                    videoModel.CreatedAt,
                    videoModel.ImageData,
                    videoModel.ImageName,
                    videoModel.VideoData,
                    videoModel.VideoName,
                    videoModel.Categoery,
                    videoModel.VideoChannelId,
                    videoModel.CurrentUserId,
                });

                if (result > 0)
                {
                    return Ok("Video is uploaded");
                }
            }
            return BadRequest("Unable to upload the video");
        }


        [HttpPut("UpdateVideo")]
        public async Task<IActionResult> UpdateVideo([FromForm] Bk_VideoViewModel model, int videoId)
        {
            try
            {
                var userId = _contextAccessor.HttpContext.Session.GetString("UserId");
                if (userId == null)
                {
                    return BadRequest("UserId is required.");
                }

                var existingVideo = await _context.Videos.FindAsync(videoId);
                if (existingVideo == null)
                {
                    return NotFound("Video not found.");
                }

                if (existingVideo.CurrentUserId != Convert.ToInt32(userId))
                {
                    return Forbid("You are not authorized to update this video.");
                }

                existingVideo.Description = model.Description;
                existingVideo.Categoery = model.Categoery;
                existingVideo.Title = model.Title;
                existingVideo.CreatedAt = DateTime.UtcNow;

                // Image upload
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    using var stream = new MemoryStream();
                    await model.ImageFile.CopyToAsync(stream);
                    existingVideo.ImageData = stream.ToArray();
                    existingVideo.ImageName = model.ImageFile.FileName;
                }

                // Video Upload
                if (model.VideoFile != null && model.VideoFile.Length > 0)
                {
                    using var stream = new MemoryStream();
                    await model.VideoFile.CopyToAsync(stream);
                    existingVideo.VideoData = stream.ToArray();
                    existingVideo.VideoName = model.VideoFile.FileName;
                }

                var updateVideoQuery = "UPDATE Videos SET Title = @Title, Description = @Description, CreatedAt = @CreatedAt, ImageData = @ImageData, ImageName = @ImageName, VideoData = @VideoData, VideoName = @VideoName, Categoery = @Categoery WHERE VideoId = @VideoId AND CurrentUserId = @CurrentUserId";

                using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(updateVideoQuery, new
                {
                    existingVideo.Title,
                    existingVideo.Description,
                    existingVideo.CreatedAt,
                    existingVideo.ImageData,
                    existingVideo.ImageName,
                    existingVideo.VideoData,
                    existingVideo.VideoName,
                    existingVideo.Categoery,
                    existingVideo.VideoId,
                    existingVideo.CurrentUserId
                });

                if (result > 0)
                {
                    return Ok("Video is updated successfully");
                }

                return BadRequest("Unable to update the video");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
