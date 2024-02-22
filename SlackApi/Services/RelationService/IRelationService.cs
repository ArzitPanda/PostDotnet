using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;

namespace SlackApi.Services.RelationService
{
    public interface IRelationService
    {
        Task<Relation> CreateRelation(RelationCreateDto relationDto);
        Task<IEnumerable<Relation>> GetAllRelations();
        Task<Relation> GetRelationById(long relationId);
        Task<IEnumerable<Relation>> GetRelationsBySenderId(long senderId);
        Task<IEnumerable<Relation>> GetRelationsByReceiverId(long receiverId);
    }
}
