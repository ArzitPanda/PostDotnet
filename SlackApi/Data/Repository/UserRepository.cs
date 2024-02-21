using SlackApi.Data.Model;
using SlackApi.Data.Repository.SlackApi.Data.Repository;

namespace SlackApi.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SlackDbContext dbContext) : base(dbContext)
        {
        }
    }
}
