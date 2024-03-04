using MongoDB.Bson;
using MongoDB.Driver;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;
using SocialTree.Data.Dto.ResponseDto;
using SocialTree.Data.Model;
using SocialTree.Data.Model.MongoModel;
using SocialTree.Services.ConverterService;
using SocialTree.Utils;
using System.Linq;
using System.Numerics;

namespace SlackApi.Services.FeedService
{
    public class FeedService : IFeedService
    {

        private readonly IUnitOfWork _unitOfWork;



        private readonly IMongoCollection<Like> _likeCollection;
        private readonly IMongoCollection<MComment> _commentCollection;
        private readonly IConverter _converter;
        private readonly IMongoCollection<MPost> _postCollection;
        public FeedService(IUnitOfWork unitOfWork,IMongoClient mongo,IConverter converter)
        {

            _converter = converter;
            var database = mongo.GetDatabase("slack");
            _likeCollection = database.GetCollection<Like>("like");
            _commentCollection = database.GetCollection<MComment>("Comments");
            _postCollection = database.GetCollection<MPost>("Posts");
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

        public async  Task<IEnumerable<Post>> GetFeedSuggestionById(long id)
        {

            var filter = Builders<Like>.Filter.Empty;
            var sort = Builders<Like>.Sort.Descending(x => x.CreatedAt);
          

            var post = await _likeCollection.Find(filter)
                                              .Sort(sort)
                                              .Limit(5)
                                              .ToListAsync();
            var postIds = post.Select(a=>a.postId
            ).ToList();



             var postsContext =  (await _unitOfWork.PostRepository.Find(a => postIds.Contains(a.Id))).Select(a=>a.Description+""+a.Title);

                    EmbeddingGenerator generator = new EmbeddingGenerator();
                     float[]  data =await   generator.Method(postsContext.ToArray());

            var options = new VectorSearchOptions<MPost>()
            {
              
                IndexName = "vector_index",
                NumberOfCandidates = 10
            };
            // run query
            var results = _postCollection.Aggregate()
                        .VectorSearch(m => m.Embeding, data, 10, options)
                        .Project(Builders<MPost>.Projection
                          .Include(m => m.postId)
                          .Include(m=>m.CreatedAt)).ToList();


            var mPostList = results.Select(bson =>
            {
                var postId = bson["postId"].AsInt64;
               
                return postId;
            }).ToList();

                


            return (await _unitOfWork.PostRepository.Find(x=> mPostList.Contains(x.Id),a=>a.Author));

        }
    }
}
