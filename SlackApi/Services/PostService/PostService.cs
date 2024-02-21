using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;

namespace SlackApi.Services.PostService
{
    public class PostService :IPostService
    {

        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository,IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _postRepository.GetAll();
        }

        public async Task<Post> GetPostById(long id)
        {
           var res= await _postRepository.Find(p => p.Id == id);
            return res.FirstOrDefault();
        }

        public async Task<Post> AddPost(AddPostDto postDto)
        {
             // Validate the incoming DTO

            var userQuery = await _userRepository.Find(a => a.UserId == postDto.AuthorId);
            var user  = userQuery.FirstOrDefault();

            var post = new Post
            {
                Title = postDto.Title,
                Description = postDto.Description,
                Visibility = postDto.Visibility,
                AuthorId = postDto.AuthorId,
                Author = user,
                CreatedAt = DateTime.Now,
                Comment = new List<Comment>()
            };

           var data = await _postRepository.Insert(post);
            return data;
        }

        public async Task<bool> UpdatePost(UpdatePostDto postDto)
        {
             // Validate the incoming DTO

            var query = await _postRepository.Find(p => p.Id == postDto.Id);
            var existingPost = query.FirstOrDefault();

            if (existingPost == null)
                return false;

            existingPost.Title = postDto.Title;
            existingPost.Description = postDto.Description;
            existingPost.Visibility = postDto.Visibility;

           var res= await _postRepository.Update(existingPost);
            return true;
        }

        public async Task<bool> DeletePost(long id)
        {
            return await _postRepository.Delete(id);
        }

        // Helper method to validate AddPostDto
        private void ValidateAddPostDto(AddPostDto postDto)
        {
            var validValues = new List<string> { "family", "friends", "office", "public" };
            foreach (var visibility in postDto.Visibility)
            {
                if (!validValues.Contains(visibility.ToLower()))
                {
                    throw new ArgumentException($"Invalid visibility value: {visibility}");
                }
            }
        }

        public async Task<IEnumerable<Post>> GetPostsByVisibilityOfPerson(long personId, string visibility)
        {
            return await _postRepository.Find(p => p.AuthorId== personId && p.Visibility.Contains(visibility));
        }

        public async Task<IEnumerable<Post>> GetPostsOfPerson(long personId)
        {
            return await _postRepository.Find(p => p.AuthorId== personId);
        }
    }
}
