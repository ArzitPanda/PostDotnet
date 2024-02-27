using Microsoft.EntityFrameworkCore;
using SlackApi.Data.Model;
using SlackApi.Data.Repository.SlackApi.Data.Repository;

namespace SlackApi.Data.Repository
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(SlackDbContext dbContext) : base(dbContext)
        {
        }

        public async  Task<IEnumerable<Post>> GetFeedById(long id)
        {
            var relations = await _dbContext.Relations
       .Where(r => r.UserId1 == id)
       .ToListAsync();

            // Get the user IDs of the related users
            var relatedUserIds = relations.Select(r => r.UserId2);

         
            var feed = await _dbContext.Posts
                .Where(p => relatedUserIds.Contains(p.AuthorId)) 
                .OrderByDescending(p => p.CreatedAt) 
                .Take(5)
                .ToListAsync();

            return feed;
        }
    }
}
