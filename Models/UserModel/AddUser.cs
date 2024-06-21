using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Project_Internal.Models.UserModel
{
    public class User_
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
        public ICollection<Channel>? Channels { get; set; }
        public ICollection<Video>? Videos { get; set; }
        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public UserProfile? UserProfile { get; set; }
        public ICollection<Playlist>? Playlists { get; set; }
        public ICollection<Like>? Likes { get; set; }
        public ICollection<History>? Histories { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }

    public class Channel
    {
        [Key]
        public int ChannelId { get; set; }
        public int CurrentUserId { get; set; }
        public string? ChannelName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Categoery { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public User_? User { get; set; }
    }

    public class Video
    {

        [Key]
        public int VideoId { get; set; }
        public int VideoChannelId { get; set; }
        public int CurrentUserId { get; set; }
        public string? Title { get; set; }
        public string? Categoery { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageName { get; set; }
        public byte[]? VideoData { get; set; }
        public string? VideoName { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        public IFormFile? VideoFile { get; set; }
        public Channel? Channel { get; set; }
        public User_? User { get; set; }
    }

    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public DateTime SubscribedAt { get; set; }

        public User_? User { get; set; }
        public Channel? Channel { get; set; }
    }

    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public User_? User { get; set; }
        public Video? Video { get; set; }
    }

    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public string Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public User_? User { get; set; }
    }

    public class Playlist
    {
        [Key]
        public int PlaylistId { get; set; }
        public int UserId { get; set; }
        public string? PlaylistName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public User_? User { get; set; }
    }

    public class PlaylistVideo
    {
        [Key]
        public int PlaylistVideoId { get; set; }
        public int PlaylistId { get; set; }
        public int VideoId { get; set; }
        public DateTime AddedAt { get; set; }

        public Playlist? Playlist { get; set; }
        public Video? Video { get; set; }
    }

    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public bool IsLike { get; set; }
        public DateTime CreatedAt { get; set; }

        public Video? Video { get; set; }
        public User_? User { get; set; }
    }

    public class History
    {
        [Key]
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public DateTime WatchedAt { get; set; }

        public User_? User { get; set; }
        public Video? Video { get; set; }
    }

    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CreatedAt { get; set; }

        public Video? Video { get; set; }
        public User_? User { get; set; }
    }

    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }
    }

    public class RolePermission
    {
        [Key]
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public string? Permission { get; set; }
        public Role? Role { get; set; }
    }

    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User_? User { get; set; }
        public Role? Role { get; set; }
    }
}
