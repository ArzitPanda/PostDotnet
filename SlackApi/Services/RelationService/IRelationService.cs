using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;

namespace SlackApi.Services.RelationService
{
    public interface IRelationService
    {
        Task<Relation> CreateRelation(RelationCreateDto relationDto);
        Task<IQueryable<Relation>> GetAllRelations();
        Task<Relation> GetRelationById(long relationId);
        Task<Relation>  GetRelationByBothId(long userID1, long userID2);
        Task<IEnumerable<Relation>> GetRelationsBySenderId(long senderId);
        Task<IEnumerable<Relation>> GetRelationsByReceiverId(long receiverId);

        Task<Relation> UpdateRelationById(long id, string type);

    }
}
