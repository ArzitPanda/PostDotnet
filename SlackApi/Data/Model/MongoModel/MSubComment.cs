using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialTree.Data.Model.MongoModel
{
    public class MSubComment
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public  long UserId  {get;set;}
        public ObjectId McommentId {  get; set;}
        
        public  string Content { get; set;} = string.Empty;


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
