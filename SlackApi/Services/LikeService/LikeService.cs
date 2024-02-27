using MongoDB.Driver;
using SocialTree.Data.Model;

namespace SocialTree.Services.LikeService
{
    public class LikeService : ILikeService
    {
        private IMongoCollection<Like> _likeCollection;

        public LikeService(IMongoClient mongo)
        {
            var database = mongo.GetDatabase("slack");
            _likeCollection = database.GetCollection<Like>("like");

           
        }


        public async Task<Like> AddLikeAsync(long userId, long postId)
        {





            var existingLike = await _likeCollection.Find(l => l.userId == userId && l.postId == postId).FirstOrDefaultAsync();
            if (existingLike != null)
            {
                // Like already exists, return existing like
                return existingLike;
            }

            var like = new Like
            {
                userId = userId,
                postId = postId,
                CreatedAt = DateTime.UtcNow
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
