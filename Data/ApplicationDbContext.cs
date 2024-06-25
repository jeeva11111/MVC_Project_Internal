using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Threading.Channels;
using WebApi_Project_Internal.Models.UserModel;

namespace WebApi_Project_Internal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Models.UserModel.Channel> Channels { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<User_> AddUser { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistVideo> PlaylistVideos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User_>()
        .HasOne(u => u.UserProfile)
        .WithOne(up => up.User)
        .HasForeignKey<UserProfile>(up => up.CurrentUserId);

            base.OnModelCreating(modelBuilder);
        }
    }

}
