using MongoDB.Driver;
using SlackApi.Data.Repository;
using SocialTree.Data.Model;

namespace SocialTree.Services.LikeService
{
    public class LikeService : ILikeService
    {
        private IMongoCollection<Like> _likeCollection;
        private IUnitOfWork _unitOfWork;

        public LikeService(IMongoClient mongo,IUnitOfWork unitOfWork)
        {
            var database = mongo.GetDatabase("slack");
            _likeCollection = database.GetCollection<Like>("like");

            _unitOfWork = unitOfWork;
           
        }


        public async Task<Like> AddLikeAsync(long userId, long postId)
        {





            var existingLike = await _likeCollection.Find(l => l.userId == userId && l.postId == postId).FirstOrDefaultAsync();




            var userQuery = await _unitOfWork.UserRepository.Find(a => a.UserId == userId);

            var user = userQuery.FirstOrDefault();


            if (existingLike != null)
            {
                // Like already exists, return existing like
                return existingLike;
            }

            var like = new Like
            {
                userId = userId,
                postId = postId,
                Username =user.UserName,
                CreatedAt = DateTime.UtcNow,
             

            };

            await _likeCollection.InsertOneAsync(like);
            return like;
        }

        public async Task<List<Like>> GetLikesAsync(long postId)
        {
            return await _likeCollection.Find(l => l.postId == postId).ToListAsync();
        }

        public async Task DeleteLikeAsync(long userId, long postId)
        {
            await _likeCollection.DeleteOneAsync(l => l.userId == userId && l.postId == postId);
        }
    }
}
