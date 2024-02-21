using SlackApi.Data.Model;
using SlackApi.Data.Repository.SlackApi.Data.Repository;

namespace SlackApi.Data.Repository
{
    public class RelationRequestRepository : GenericRepository<RelationRequest>, IRelationRequestRepository
    {
        public RelationRequestRepository(SlackDbContext dbContext) : base(dbContext)
        {
        }
    }
}
