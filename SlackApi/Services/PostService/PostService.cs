using MongoDB.Driver;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;
using SlackApi.Utils;
using SocialTree.Data.Dto.ResponseDto;
using SocialTree.Data.Model;
using SocialTree.Data.Model.MongoModel;
using SocialTree.Services.ConverterService;
using SocialTree.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SlackApi.Services.PostService
{
    public class PostService :IPostService
    {

        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ImageUploadUtils _imageUploadUtils;
        private readonly IMongoCollection<MPost> _postCollection;
        private readonly IConverter _converter;
        public PostService(IPostRepository postRepository,IUserRepository userRepository,IMongoClient mongo,ImageUploadUtils imageUploadUtils,IConverter converter)
        {




            var database = mongo.GetDatabase("slack");
            _postCollection = database.GetCollection<MPost>("Posts");
            _postRepository = postRepository;
            _userRepository = userRepository;
            _imageUploadUtils = imageUploadUtils;
            _converter = converter;

        }

        public async Task<IQueryable<Post>> GetAllPosts()
        {
          var data =  _postRepository.GetAll();


          



            return data;
        }

        public async Task<PostDto> GetPostById(long id)
        {
           var res= await _postRepository.Find(p => p.Id == id,p=>p.Author);
            
            var post = res.FirstOrDefault();

            return (await  _converter.postToPostDto(post));



        }

        public async Task<Post> AddPost(AddPostDto postDto)
        {
             // Validate the incoming DTO

            var userQuery = await _userRepository.Find(a => a.UserId == postDto.AuthorId);
            var user  = userQuery.FirstOrDefault();
            string photoUrl = _imageUploadUtils.UploadImage(postDto.Photo);
            var post = new Post
            {
                Title = postDto.Title,
                Description = postDto.Description,
                Visibility = postDto.Visibility,
                AuthorId = postDto.AuthorId,
                Author = user,
                ImgUrl = photoUrl,
                CreatedAt = DateTime.Now,
                Comment = new List<Comment>()
            };

           var data = await _postRepository.Insert(post);

            if(postDto.Visibility.Contains("Public"))
            {
                List<string> text = new List<string>();
                text.Add(postDto.Title+""+postDto.Description);

                EmbeddingGenerator embedding = new EmbeddingGenerator();
                 var embeddingResult =   await embedding.Method(text.ToArray());

                MPost postEm = new MPost { Embeding
                = embeddingResult,CreatedAt=DateTime.Now,postId=data.Id};

               await  _postCollection.InsertOneAsync(postEm);

            }

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

        public async Task<IEnumerable<PostDto>> GetPostsByVisibilityOfPerson(long personId, string visibility)
        {
          var DataOne = await _postRepository.Find(p => p.AuthorId== personId && p.Visibility.Contains(visibility), p => p.Author);


            var postdatas = DataOne.Select(async x => { return (await _converter.postToPostDto(x)); });



            return await Task.WhenAll(postdatas);


        }

        public async Task<IEnumerable<PostDto>> GetPostsOfPerson(long personId)
        {
           var data = await _postRepository.Find(p => p.AuthorId == personId, p => p.Author);
            var postdatas = data.Select(async x => { return (await _converter.postToPostDto(x)); });



            return await Task.WhenAll(postdatas);

           
        }
    }
}
