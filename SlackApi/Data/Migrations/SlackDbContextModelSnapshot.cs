﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SlackApi.Data;

#nullable disable

namespace SlackApi.Data.Migrations
{
    [DbContext(typeof(SlackDbContext))]
    partial class SlackDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SlackApi.Data.Model.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CommentorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CommentorUserId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SlackApi.Data.Model.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Visibility")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Posts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Post");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SlackApi.Data.Model.Relation", b =>
                {
                    b.Property<long>("RelationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RelationId"));

                    b.Property<string>("RelationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId1")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId2")
                        .HasColumnType("bigint");

                    b.HasKey("RelationId");

                    b.HasIndex("UserId1");

                    b.HasIndex("UserId2");

                    b.ToTable("Relations");
                });

            modelBuilder.Entity("SlackApi.Data.Model.RelationRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RelationShip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RequestorUserId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RequestorUserId");

                    b.HasIndex("UserId");

                    b.ToTable("RelationRequests");
                });

            modelBuilder.Entity("SlackApi.Data.Model.Thread", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CommentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.ToTable("Thread");
                });

            modelBuilder.Entity("SlackApi.Data.Model.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"));

                    b.Property<long?>("CommentId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("CommentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SlackApi.Data.Model.UserAuthorization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("UserAuthorizeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAuthorization");
                });

            modelBuilder.Entity("SlackApi.Data.Model.UserPasswordManager", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserPasswordManagers");
                });

            modelBuilder.Entity("SlackApi.Data.Model.UserToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Validity")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("SocialTree.Data.Model.UserVerification", b =>
                {
                    b.Property<int>("VerificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VerificationId"));

                    b.Property<DateTime?>("EmailVerificationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailVerificationToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("bit");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("VerificationId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserVerification");
                });

            modelBuilder.Entity("SlackApi.Data.Model.ImagePost", b =>
                {
                    b.HasBaseType("SlackApi.Data.Model.Post");

                    b.Property<string>("Imageurl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ImagePost");
                });

            modelBuilder.Entity("SlackApi.Data.Model.Comment", b =>
                {
                    b.HasOne("SlackApi.Data.Model.User", "Commentor")
                        .WithMany()
                        .HasForeignKey("CommentorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SlackApi.Data.Model.Post", "Post")
                        .WithMany("Comment")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commentor");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("SlackApi.Data.Model.Post", b =>
                {
                    b.HasOne("SlackApi.Data.Model.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("SlackApi.Data.Model.Relation", b =>
                {
                    b.HasOne("SlackApi.Data.Model.User", "User1")
                        .WithMany("Relations")
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SlackApi.Data.Model.User", "User2")
                        .WithMany()
                        .HasForeignKey("UserId2")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("SlackApi.Data.Model.RelationRequest", b =>
                {
                    b.HasOne("SlackApi.Data.Model.User", "Requestor")
                        .WithMany()
                        .HasForeignKey("RequestorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SlackApi.Data.Model.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Requestor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SlackApi.Data.Model.Thread", b =>
                {
                    b.HasOne("SlackApi.Data.Model.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("SlackApi.Data.Model.User", b =>
                {
                    b.HasOne("SlackApi.Data.Model.Comment", null)
                        .WithMany("Likes")
                        .HasForeignKey("CommentId");
                });

            modelBuilder.Entity("SlackApi.Data.Model.UserAuthorization", b =>
                {
                    b.HasOne("SlackApi.Data.Model.User", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("SlackApi.Data.Model.UserPasswordManager", b =>
                {
                    b.HasOne("SlackApi.Data.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SlackApi.Data.Model.UserToken", b =>
                {
                    b.HasOne("SlackApi.Data.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialTree.Data.Model.UserVerification", b =>
                {
                    b.HasOne("SlackApi.Data.Model.User", "User")
                        .WithOne("UserVerification")
                        .HasForeignKey("SocialTree.Data.Model.UserVerification", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SlackApi.Data.Model.Comment", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("SlackApi.Data.Model.Post", b =>
                {
                    b.Navigation("Comment");
                });

            modelBuilder.Entity("SlackApi.Data.Model.User", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("Relations");

                    b.Navigation("Requests");

                    b.Navigation("UserVerification")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
