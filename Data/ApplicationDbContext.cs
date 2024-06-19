using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC_Project_Internal.Filters;
using WebApi_Project_Internal.Models.UserModel;

namespace MVC_Project_Internal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AddUserModel> Students { get; set; }
        public DbSet<User_> AddUser { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PlaylistVideo> PlaylistVideos { get; set; }
    }
}