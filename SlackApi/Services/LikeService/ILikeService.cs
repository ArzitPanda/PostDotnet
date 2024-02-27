using SocialTree.Data.Model;

namespace SocialTree.Services.LikeService
{
    public interface ILikeService 
    {
        Task<Like> AddLikeAsync(long userId, long postId);
        Task<List<Like>> GetLikesAsync(long postId);
        Task DeleteLikeAsync(long userId, long postId);


    }
}
