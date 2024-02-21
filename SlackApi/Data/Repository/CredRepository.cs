using SlackApi.Data.Model;
using SlackApi.Data.Repository.SlackApi.Data.Repository;

namespace SlackApi.Data.Repository
{
    public class CredRepository : GenericRepository<UserPasswordManager>, ICredRepository
    {
        public CredRepository(SlackDbContext dbContext) : base(dbContext)
        {
        }
    }
}
