using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SocialTree.Data.Dto.ResponseDto;

namespace SlackApi.Services.PostService
{
    public interface IPostService
    {
        Task<IQueryable<Post>> GetAllPosts();
        Task<PostDto> GetPostById(long id);

       



        Task<IEnumerable<PostDto>> GetPostsByVisibilityOfPerson(long personId, string visibility);
        Task<IEnumerable<PostDto>> GetPostsOfPerson(long personId);
        Task<Post> AddPost(AddPostDto postDto);
        Task<bool> UpdatePost(UpdatePostDto postDto);
        Task<bool> DeletePost(long id);
    }
}
