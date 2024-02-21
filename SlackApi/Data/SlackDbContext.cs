using Microsoft.EntityFrameworkCore;
using SlackApi.Data.Model;
using Thread = SlackApi.Data.Model.Thread;

namespace SlackApi.Data
{
    public class SlackDbContext : DbContext
    {

        public SlackDbContext(DbContextOptions<SlackDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Relation> Relations { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ImagePost> ImagePosts { get; set; }

        public DbSet<RelationRequest> RelationRequests { get; set; }


        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserAuthorization> UserAuthorization { get; set; }

        public DbSet<Thread>  Thread{ get; set; }
        public DbSet<UserPasswordManager> UserPasswordManagers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relation>()
                .HasOne(r => r.User1)
                .WithMany(u => u.Relations)
                .HasForeignKey(r => r.UserId1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relation>()
                .HasOne(r => r.User2)
                .WithMany()
                .HasForeignKey(r => r.UserId2)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Post>().HasOne(p => p.Author).WithMany(p => p.Posts).HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RelationRequest>().HasOne(a => a.User).WithMany(a => a.Requests).OnDelete(DeleteBehavior.Restrict);
          


        }



    }
}
