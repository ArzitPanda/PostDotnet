using SlackApi.Data.Model;
using SlackApi.Data.Repository.SlackApi.Data.Repository;

namespace SlackApi.Data.Repository
{
    public class RelationalRepository : GenericRepository<Relation>, IRelationalRepository
    {
        public RelationalRepository(SlackDbContext dbContext) : base(dbContext)
        {
        }
    }
}
