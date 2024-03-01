using MongoDB.Driver;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;
using SocialTree.Data.Dto.ResponseDto;
using SocialTree.Data.Model;
using SocialTree.Data.Model.MongoModel;
using SocialTree.Services.ConverterService;

namespace SlackApi.Services.FeedService
{
    public class FeedService : IFeedService
    {

        private readonly IUnitOfWork _unitOfWork;



        private readonly IMongoCollection<Like> _likeCollection;
        private readonly IMongoCollection<MComment> _commentCollection;
        private readonly IConverter _converter;

        public FeedService(IUnitOfWork unitOfWork,IMongoClient mongo,IConverter converter)
        {

            _converter = converter;
            var database = mongo.GetDatabase("slack");
            _likeCollection = database.GetCollection<Like>("like");
            _commentCollection = database.GetCollection<MComment>("Comments");
            _unitOfWork = unitOfWork;

        }

        public Task<IEnumerable<Post>> GetFeedById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostDto>> GetFeedByIdAndType(long id,string Type)
        {

            var result = await _unitOfWork.RelationRepository.Find(a => (a.UserId1 == id || a.UserId2 == id) && a.RelationType==Type);

           Console.WriteLine(result.ToList().Count);

            var userIds = result.Select(r => { if (r.UserId1 == id) { return r.UserId2; } return r.UserId1; }).ToList();


            Console.WriteLine("i am here un getfeed");
            for(var i = 0;i<userIds.Count;i++)
            {
                Console.WriteLine(userIds[i]);
            }

            var posts = await _unitOfWork.PostRepository.Find(a=>userIds.Contains(a.AuthorId) && a.Visibility.Contains(Type),a=>a.Author);


            var postDtos = posts.Select(async post => { return (await _converter.postToPostDto(post, id)); });


            return await Task.WhenAll(postDtos);


        }
    }
}
