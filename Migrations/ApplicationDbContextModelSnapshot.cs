﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi_Project_Internal.Data;

#nullable disable

namespace WebApi_Project_Internal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Channel", b =>
                {
                    b.Property<int>("ChannelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChannelId"));

                    b.Property<string>("Categoery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChannelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("CommentText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("UserId");

                    b.HasIndex("VideoId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.History", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HistoryId"));

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WatchedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("HistoryId");

                    b.HasIndex("UserId");

                    b.HasIndex("VideoId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Like", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LikeId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsLike")
                        .HasColumnType("bit");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("LikeId");

                    b.HasIndex("UserId");

                    b.HasIndex("VideoId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.HasIndex("VideoId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Playlist", b =>
                {
                    b.Property<int>("PlaylistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlaylistId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaylistName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistId");

                    b.HasIndex("UserId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.PlaylistVideo", b =>
                {
                    b.Property<int>("PlaylistVideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlaylistVideoId"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<int?>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistVideoId");

                    b.HasIndex("PlaylistId");

                    b.HasIndex("VideoId");

                    b.ToTable("PlaylistVideos");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.RolePermission", b =>
                {
                    b.Property<int>("RolePermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolePermissionId"));

                    b.Property<string>("Permission")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("RolePermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionId"));

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubscribedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("ChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.UserProfile", b =>
                {
                    b.Property<int>("UserProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserProfileId"));

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserProfileId");

                    b.HasIndex("CurrentUserId")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserRoleId"));

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.User_", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AddUser");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Video", b =>
                {
                    b.Property<int>("VideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VideoId"));

                    b.Property<string>("Categoery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ChannelId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VideoChannelId")
                        .HasColumnType("int");

                    b.Property<byte[]>("VideoData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("VideoName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VideoId");

                    b.HasIndex("ChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Channel", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.User_", "User")
                        .WithMany("Channels")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Comment", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.User_", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.HasOne("WebApi_Project_Internal.Models.UserModel.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.History", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.User_", "User")
                        .WithMany("Histories")
                        .HasForeignKey("UserId");

                    b.HasOne("WebApi_Project_Internal.Models.UserModel.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Like", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.User_", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId");

                    b.HasOne("WebApi_Project_Internal.Models.UserModel.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Notification", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.User_", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");

                    b.HasOne("WebApi_Project_Internal.Models.UserModel.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Playlist", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.User_", "User")
                        .WithMany("Playlists")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.PlaylistVideo", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("PlaylistId");

                    b.HasOne("WebApi_Project_Internal.Models.UserModel.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.RolePermission", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Subscription", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.Channel", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi_Project_Internal.Models.UserModel.User_", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId");

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.UserProfile", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.User_", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("WebApi_Project_Internal.Models.UserModel.UserProfile", "CurrentUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.UserRole", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi_Project_Internal.Models.UserModel.User_", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Video", b =>
                {
                    b.HasOne("WebApi_Project_Internal.Models.UserModel.Channel", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelId");

                    b.HasOne("WebApi_Project_Internal.Models.UserModel.User_", "User")
                        .WithMany("Videos")
                        .HasForeignKey("UserId");

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.Role", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("WebApi_Project_Internal.Models.UserModel.User_", b =>
                {
                    b.Navigation("Channels");

                    b.Navigation("Comments");

                    b.Navigation("Histories");

                    b.Navigation("Likes");

                    b.Navigation("Notifications");

                    b.Navigation("Playlists");

                    b.Navigation("Subscriptions");

                    b.Navigation("UserProfile");

                    b.Navigation("UserRoles");

                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
