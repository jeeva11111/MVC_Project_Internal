﻿using Azure.Core;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WebApi_Project_Internal.Data;
using WebApi_Project_Internal.Models.BkViewModels;
using WebApi_Project_Internal.Models.UserModel;
//using WebApi_Project_Internal.SignalR;

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
        //  private readonly
        //private readonly IHubContext<HubNotifications> _hubContext;

        public VideoController(IHttpContextAccessor contextAccessor, ApplicationDbContext context, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ServerLink");
            //_hubContext = hubContext;
        }

        [HttpGet("GetVideoAllVideos")]

        public async Task<IActionResult> GeAllVideos()
        {
            string query = "SELECT * FROM Videos";

            using var connection = new SqlConnection(_connectionString);
            var result = await connection.QueryAsync<Video>(query);
            if (result.Any()) { return Ok(result.ToList()); }
            return BadRequest("video is empty");
        }



        [HttpGet("GetById")]
        public async Task<IActionResult> GetByUserId()
        {
            var userId = _contextAccessor.HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return BadRequest("UserId is required");
            }

            int parsedUserId;
            if (!int.TryParse(userId, out parsedUserId))
            {
                return BadRequest("Invalid UserId");
            }

            try
            {
                var listOfVideos = await _context.Videos
                    .Where(v => v.CurrentUserId == parsedUserId).Select(x => new
                    { x.ImageName, x.VideoName, x.Categoery, x.Description, x.CreatedAt })
                    .ToListAsync();

                if (listOfVideos.Any())
                {
                    return Ok(listOfVideos);
                }
                return NotFound("No videos available for this user");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetbyCategory")]
        public async Task<IActionResult> GetVideoByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return BadRequest("Enter the category");
            }

            var userId = _contextAccessor.HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return BadRequest("UserId is required");
            }

            string query = "SELECT * FROM Videos WHERE Categoery = @Category AND CurrentUserId = @UserId";

            using var connection = new SqlConnection(_connectionString);
            var parameters = new { Category = category, UserId = Convert.ToInt32(userId) };
            var result = await connection.QueryAsync<Video>(query, parameters);

            if (result.Any())
            {
                return Ok(result.ToList());
            }

            return BadRequest("Unable to find the matching record");
        }




        //[HttpPost("uploadVideo")]
        //public async Task<IActionResult> PostVideo([FromForm] Bk_VideoViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = _contextAccessor.HttpContext.Session.GetString("UserId");
        //        if (userId == null)
        //        {
        //            return BadRequest("UserId is required.");
        //        }

        //        var channelId = _context.Channels
        //            .Where(x => x.CurrentUserId == Convert.ToInt32(userId))
        //            .Select(x => x.ChannelId)
        //            .SingleOrDefault();

        //        if (channelId == 0)
        //        {
        //            return BadRequest("No channel found for the user.");
        //        }

        //        var videoModel = new Models.UserModel.Video
        //        {
        //            Description = model.Description,
        //            Categoery = model.Categoery,
        //            Title = model.Title,
        //            CurrentUserId = Convert.ToInt32(userId),
        //            VideoChannelId = channelId,
        //            CreatedAt = DateTime.UtcNow
        //        };

        //        // Image upload
        //        if (model.ImageFile != null && model.ImageFile.Length > 0)
        //        {
        //            using var stream = new MemoryStream();
        //            await model.ImageFile.CopyToAsync(stream);
        //            videoModel.ImageData = stream.ToArray();
        //            videoModel.ImageName = model.ImageFile.FileName;
        //        }

        //        // Video Upload
        //        if (model.VideoFile != null && model.VideoFile.Length > 0)
        //        {
        //            using var stream = new MemoryStream();
        //            await model.VideoFile.CopyToAsync(stream);
        //            videoModel.VideoData = stream.ToArray();
        //            videoModel.VideoName = model.VideoFile.FileName;
        //        }

        //        var insertVideoQuery = "INSERT INTO Videos (Title, Description, CreatedAt, ImageData, ImageName, VideoData, VideoName, Categoery, VideoChannelId, CurrentUserId) OUTPUT INSERTED.VideoId VALUES (@Title, @Description, @CreatedAt, @ImageData, @ImageName, @VideoData, @VideoName, @Categoery, @VideoChannelId, @CurrentUserId)";

        //        using var connection = new SqlConnection(_connectionString);

        //        var videoId = await connection.QuerySingleAsync<int>(insertVideoQuery, new
        //        {
        //            videoModel.Title,
        //            videoModel.Description,
        //            videoModel.CreatedAt,
        //            videoModel.ImageData,
        //            videoModel.ImageName,
        //            videoModel.VideoData,
        //            videoModel.VideoName,
        //            videoModel.Categoery,
        //            videoModel.VideoChannelId,
        //            videoModel.CurrentUserId,
        //        });

        //        if (videoId > 0)
        //        {
        //            var subscribers = _context.Subscriptions
        //                .Where(s => s.ChannelId == channelId)
        //                .Select(s => s.SubscriptionId)
        //                .ToList();

        //            foreach (var subscriber in subscribers)
        //            {
        //                _context.Notifications.Add(new Notification
        //                {
        //                    VideoId = videoId,
        //                    CreatedAt = DateTime.Now,
        //                    CurrentUserId = Convert.ToInt32(userId),
        //                    Message = $"New video is uploaded: {videoModel.Title}",
        //                    IsRead = false,
        //                });
        //            }

        //            await _context.SaveChangesAsync();
        //            return Ok("Video is uploaded");
        //        }
        //    }
        //    return BadRequest("Unable to upload the video");
        //}


        //[HttpPost("uploadVideo")]
        //public async Task<IActionResult> PostVideo([FromForm] Bk_VideoViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = _contextAccessor.HttpContext.Session.GetString("UserId");
        //        if (userId == null)
        //        {
        //            return BadRequest("UserId is required.");
        //        }

        //        var channelId = _context.Channels
        //            .Where(x => x.CurrentUserId == Convert.ToInt32(userId))
        //            .Select(x => x.ChannelId)
        //            .SingleOrDefault();

        //        if (channelId == 0)
        //        {
        //            return BadRequest("No channel found for the user.");
        //        }

        //        var videoModel = new Models.UserModel.Video
        //        {
        //            Description = model.Description,
        //            Categoery = model.Categoery,
        //            Title = model.Title,
        //            CurrentUserId = Convert.ToInt32(userId),
        //            VideoChannelId = channelId,
        //            CreatedAt = DateTime.UtcNow
        //        };

        //        // Image upload
        //        if (model.ImageFile != null && model.ImageFile.Length > 0)
        //        {
        //            using var stream = new MemoryStream();
        //            await model.ImageFile.CopyToAsync(stream);
        //            videoModel.ImageData = stream.ToArray();
        //            videoModel.ImageName = model.ImageFile.FileName;
        //        }

        //        // Video Upload
        //        if (model.VideoFile != null && model.VideoFile.Length > 0)
        //        {
        //            using var stream = new MemoryStream();
        //            await model.VideoFile.CopyToAsync(stream);
        //            videoModel.VideoData = stream.ToArray();
        //            videoModel.VideoName = model.VideoFile.FileName;
        //        }

        //        var insertVideoQuery = "INSERT INTO Videos (Title, Description, CreatedAt, ImageData, ImageName, VideoData, VideoName, Categoery, VideoChannelId, CurrentUserId) OUTPUT INSERTED.VideoId VALUES (@Title, @Description, @CreatedAt, @ImageData, @ImageName, @VideoData, @VideoName, @Categoery, @VideoChannelId, @CurrentUserId)";

        //        using var connection = new SqlConnection(_connectionString);

        //        var videoId = await connection.QuerySingleAsync<int>(insertVideoQuery, new
        //        {
        //            videoModel.Title,
        //            videoModel.Description,
        //            videoModel.CreatedAt,
        //            videoModel.ImageData,
        //            videoModel.ImageName,
        //            videoModel.VideoData,
        //            videoModel.VideoName,
        //            videoModel.Categoery,
        //            videoModel.VideoChannelId,
        //            videoModel.CurrentUserId,
        //        });

        //        if (videoId > 0)
        //        {
        //            var subscribers = _context.Subscriptions
        //                .Where(s => s.ChannelId == channelId)
        //                .Select(s => s.SubscriptionId)
        //                .ToList();

        //            foreach (var subscriber in subscribers)
        //            {
        //                _context.Notifications.Add(new Notification
        //                {
        //                    VideoId = videoId,
        //                    CreatedAt = DateTime.Now,
        //                    CurrentUserId = Convert.ToInt32(userId),
        //                    Message = $"New video is uploaded: {videoModel.Title}",
        //                    IsRead = false,
        //                });
        //            }

        //            await _context.SaveChangesAsync();

        //            // Send notification via SignalR
        //            var notificationMessage = $"New video is uploaded: {videoModel.Title}";
        //            // await _hubContext.Clients.All.SendAsync("Notify", notificationMessage);

        //            return Ok("Video is uploaded");
        //        }
        //    }
        //    return BadRequest("Unable to upload the video");
        //}



        [HttpPost("uploadVideo")]
        public async Task<IActionResult> PostVideo([FromForm] Bk_VideoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Read cookie from IHttpContextAccessor
                    string userId = _contextAccessor.HttpContext.Request.Cookies["UserId"];
                    if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int UserId))
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

                    var videoModel = new Video
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
                    else return Content("Image Should not be Empty");

                    // Video Upload
                    if (model.VideoFile != null && model.VideoFile.Length > 0)
                    {
                        using var stream = new MemoryStream();
                        await model.VideoFile.CopyToAsync(stream);
                        videoModel.VideoData = stream.ToArray();
                        videoModel.VideoName = model.VideoFile.FileName;
                    }
                    else return Content("Image Should not be Empty");

                    var insertVideoQuery = "INSERT INTO Videos (Title, Description, CreatedAt, ImageData, ImageName, VideoData, VideoName, Categoery, VideoChannelId, CurrentUserId) OUTPUT INSERTED.VideoId VALUES (@Title, @Description, @CreatedAt, @ImageData, @ImageName, @VideoData, @VideoName, @Categoery, @VideoChannelId, @CurrentUserId)";

                    using var connection = new SqlConnection(_connectionString);

                    var videoId = await connection.QuerySingleAsync<int>(insertVideoQuery, new
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

                    if (videoId > 0)
                    {
                        var subscribers = _context.Subscriptions
                            .Where(s => s.ChannelId == channelId)
                            .Select(s => s.SubscriptionId)
                            .ToList();

                        foreach (var subscriber in subscribers)
                        {
                            _context.Notifications.Add(new Notification
                            {
                                VideoId = videoId,
                                CreatedAt = DateTime.Now,
                                CurrentUserId = Convert.ToInt32(userId),
                                Message = $"New video is uploaded: {videoModel.Title}",
                                IsRead = false,
                            });
                        }

                        await _context.SaveChangesAsync();

                        // Send notification via SignalR
                        var notificationMessage = $"New video is uploaded: {videoModel.Title}";
                        // await _hubContext.Clients.All.SendAsync("Notify", notificationMessage);

                        return Ok("Video is uploaded");
                    }
                }
                return BadRequest("Model state is invalid.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPut("UpdateVideo")]
        public async Task<IActionResult> UpdateVideo([FromForm] Bk_VideoViewModel model, int videoId)
        {
            try
            {
                var userId = _contextAccessor.HttpContext.Items["UserId"] as string;
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
