using MongoDB.Driver;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;
using SocialTree.Data.Dto.ResponseDto;
using SocialTree.Data.Model.MongoModel;
using SocialTree.Data.Model;
using Microsoft.Extensions.Hosting;

namespace SocialTree.Services.ConverterService
{
    public class Converter : IConverter
    {
        private readonly IUnitOfWork _unitOfWork;



        private readonly IMongoCollection<Like> _likeCollection;
        private readonly IMongoCollection<MComment> _commentCollection;

        public Converter(IUnitOfWork unitOfWork, IMongoClient mongo)
        {

            var database = mongo.GetDatabase("slack");
            _likeCollection = database.GetCollection<Like>("like");
            _commentCollection = database.GetCollection<MComment>("Comments");
            _unitOfWork = unitOfWork;
        }


        public async  Task<PostDto> postToPostDto(Post post)
        {
            var likeCount = await _likeCollection.CountDocumentsAsync(Builders<Like>.Filter.Eq(l => l.postId, post.Id));
            var CommentCount = await _commentCollection.CountDocumentsAsync(Builders<MComment>.Filter.Eq(l => l.PostId, post.Id));
            var top2Comments = await _commentCollection.Find(Builders<MComment>.Filter.Eq(c => c.PostId, post.Id))
                                            .SortByDescending(c => c.DateTime)
                                            .Limit(2)
            .ToListAsync();


            var top2Likes = await _likeCollection.Find(Builders<Like>.Filter.Eq(c => c.postId, post.Id)).SortByDescending(c => c.CreatedAt).Limit(2).ToListAsync();






            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                ImgUrl = post.ImgUrl,
                CreatedAt = post.CreatedAt,
                AuthorId = post.AuthorId,

                UserName = post.Author.UserName,
                UserPhotoUrl = post.Author.PhotoUrl,
                Likecount = likeCount,
                CommentCount = CommentCount,
                TopComments = top2Comments,
                RecentLikedUser = top2Likes.Select(p => p.Username).ToList(),




            };
        }



        public async Task<PostDto> postToPostDto(Post post,long userId)
        {
            var likeCount = await _likeCollection.CountDocumentsAsync(Builders<Like>.Filter.Eq(l => l.postId, post.Id));
            var CommentCount = await _commentCollection.CountDocumentsAsync(Builders<MComment>.Filter.Eq(l => l.PostId, post.Id));
            var top2Comments = await _commentCollection.Find(Builders<MComment>.Filter.Eq(c => c.PostId, post.Id))
                                            .SortByDescending(c => c.DateTime)
                                            .Limit(2)
            .ToListAsync();


            var top2Likes = await _likeCollection.Find(Builders<Like>.Filter.Eq(c => c.postId, post.Id)).SortByDescending(c => c.CreatedAt).Limit(2).ToListAsync();


            var filter = Builders<Like>.Filter
    .Eq(l => l.postId, post.Id) // Condition 1
    & Builders<Like>.Filter
    .Eq(l => l.userId, userId);



            var isLiked = await _likeCollection.CountDocumentsAsync(filter);



            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                ImgUrl = post.ImgUrl,
                CreatedAt = post.CreatedAt,
                AuthorId = post.AuthorId,
                IsLiked = isLiked >0 ?true : false,
                UserName = post.Author.UserName,
                UserPhotoUrl = post.Author.PhotoUrl,
                Likecount = likeCount,
                CommentCount = CommentCount,
                TopComments = top2Comments,
                RecentLikedUser = top2Likes.Select(p => p.Username).ToList(),




            };
        }












    }
}
