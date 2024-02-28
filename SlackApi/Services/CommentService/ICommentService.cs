using MongoDB.Bson;
using SocialTree.Data.Dto.RequestDto;
using SocialTree.Data.Model.MongoModel;

namespace SocialTree.Services.CommentService
{
    public interface ICommentService
    {

        Task<MComment> GetComment(string id);
        Task<List<MSubComment>> GetSubCommentByComment(string id, int pageSize, int pageNo);
        Task<List<MComment>> GetCommentsByPost(long postId, int pageSize, int pageNo);

        Task<bool> DeleteComment(string id);
        Task<bool> UpdateComment(string id, MComment updatedComment);

        Task<bool> UpdateSubComment(string id, MSubComment updatedSubComment);
        Task<bool> DeleteSubComment(string id);

        Task<bool> AddCommentToPost(MComment comment);



        Task<bool> AddSubCommentToAComment(ObjectId parentId, SubCommentDto subComment);

    }
}
