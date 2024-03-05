using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;

namespace SlackApi.Services.RelationRequestService
{
    public interface IRelationRequestService
    {
        Task<RelationRequest> CreateRelationRequest(RelationRequestDto requestDto);


        Task<IEnumerable<RelationRequest>> GetAllRelationRequestsByUserId(long id);
        Task<IEnumerable<RelationRequest>> GetAllRelationRequestsByRequestorId(long id);
        Task<RelationRequest> GetRelationRequestById(long id);
        Task<bool> UpdateRelationRequest(UpdateRelationRequestDto requestDto);
        Task<bool> DeleteRelationRequest(long id);

        Task<RelationRequest> GetRelationRequestByReceiverAndRequestor(long requestorId, long receiverId);
    }
}
