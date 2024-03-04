using MongoDB.Bson.Serialization.Attributes;

namespace SocialTree.Data.Model.MongoModel
{
    public class MPost
    {
        public long  postId { get; set; }
        public DateTime CreatedAt { get; set; }

        [BsonElement("plot_embedding")]
        public float[] Embeding {  get; set; }

       

    }
}
