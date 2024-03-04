using MongoDB.Bson;

namespace SocialTree.Data.Model
{
    public class Like
    {
        public ObjectId Id { get; set; }
        public long userId { get; set; }

        public string Username { get; set; }
        public long postId { get; set; }

        public DateTime CreatedAt { get; set; }





    }
}
