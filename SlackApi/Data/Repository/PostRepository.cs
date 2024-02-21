using SlackApi.Data.Model;
using SlackApi.Data.Repository.SlackApi.Data.Repository;

namespace SlackApi.Data.Repository
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(SlackDbContext dbContext) : base(dbContext)
        {
        }
    }
}
