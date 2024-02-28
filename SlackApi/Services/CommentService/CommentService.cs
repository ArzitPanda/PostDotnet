using MongoDB.Bson;
using MongoDB.Driver;
using SocialTree.Data.Dto.RequestDto;
using SocialTree.Data.Model.MongoModel;

namespace SocialTree.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IMongoCollection<MComment> _commentCollection;
        private readonly IMongoCollection<MSubComment> _subCommentCollection;

        public CommentService(IMongoClient client)
        {
            var database = client.GetDatabase("slack");
            _commentCollection = database.GetCollection<MComment>("Comments");
            _subCommentCollection = database.GetCollection<MSubComment>("SubComments");
        }

        public async Task<bool> DeleteComment(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            var result = await _commentCollection.DeleteOneAsync(c => c.Id == objectId);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<bool> DeleteSubComment(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            var result = await _subCommentCollection.DeleteOneAsync(sc => sc.Id == objectId);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<MComment> GetComment(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            return await _commentCollection.Find(c => c.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task<List<MComment>> GetCommentsByPost(long postId, int pageSize, int pageNo)
        {
            var filter = Builders<MComment>.Filter.Eq(c => c.PostId, postId);
            var findFluent = _commentCollection.Find(filter)
                 .SortByDescending(c => c.DateTime) // Corrected property name from DateTime to CreatedAt
                 .Skip(pageSize * (pageNo - 1))
                 .Limit(pageSize);

            return await findFluent
      .Project(c => new MComment
      {
          Id = c.Id,
         Content = c.Content,
          PostId = c.PostId,
          DateTime =c.DateTime,
          UserId = c.UserId,
      })
      .ToListAsync();
        }

        public async Task<List<MSubComment>> GetSubCommentByComment(string id, int pageSize, int pageNo)
        {


            ObjectId objectId = ObjectId.Parse(id);
            var filter = Builders<MSubComment>.Filter.Eq(sc => sc.McommentId, objectId);
            var options = new FindOptions<MSubComment>
            {
                Skip = pageSize * (pageNo - 1),
                Limit = pageSize
            };

            return await _subCommentCollection.Find(filter).SortByDescending(sc => sc.CreatedAt).Skip(pageSize*(pageNo-1)).Limit(pageSize).ToListAsync();
        }

        public async Task<bool> UpdateComment(string id, MComment updatedComment)
        {
            ObjectId objectId = ObjectId.Parse(id);
            var result = await _commentCollection.ReplaceOneAsync(c => c.Id == objectId, updatedComment);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> UpdateSubComment(string id, MSubComment updatedSubComment)
        {
            ObjectId objectId = ObjectId.Parse(id);
            var result = await _subCommentCollection.ReplaceOneAsync(sc => sc.Id == objectId, updatedSubComment);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> AddCommentToPost(MComment comment)
        {
            try
            {
                await _commentCollection.InsertOneAsync(comment);
                return true;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred while adding a comment to post: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddSubCommentToAComment(ObjectId parentId, SubCommentDto subComment)
        {
            try
            {
                var filter = Builders<MComment>.Filter.Eq(c => c.Id, parentId);
             

                    // If the parent comment is updated successfully, add the sub-comment
                    var subCommentCollection = _commentCollection.Database.GetCollection<MSubComment>("SubComments");


                MSubComment m = new MSubComment { CreatedAt = DateTime.Now, Content = subComment.Content, McommentId = parentId, UserId = subComment.UserId };
                    await subCommentCollection.InsertOneAsync(m);


                    var update = Builders<MComment>.Update.Push(c => c.SubCommentIds, m.Id);
                    var result = await _commentCollection.UpdateOneAsync(filter, update);
                    return true;
              
                  
                
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred while adding a sub-comment to a comment: {ex.Message}");
                return false;
            }
        }


    }
}
