using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialTree.Data.Model.MongoModel
{
    public class MComment
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public long UserId {  get; set; }
        public string Content { get; set; } = string.Empty;
        public long PostId { get; set; }

        public List<ObjectId> SubCommentIds { get; set; }

        public DateTime DateTime { get; set; }


    }
}
