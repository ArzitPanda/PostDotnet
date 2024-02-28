using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;

namespace SlackApi.Services.PostService
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPostById(long id);

       



        Task<IEnumerable<Post>> GetPostsByVisibilityOfPerson(long personId, string visibility);
        Task<IEnumerable<Post>> GetPostsOfPerson(long personId);
        Task<Post> AddPost(AddPostDto postDto);
        Task<bool> UpdatePost(UpdatePostDto postDto);
        Task<bool> DeletePost(long id);
    }
}
