using MongoDB.Bson.Serialization.Attributes;

namespace SocialTree.Data.Model.MongoModel
{
    public class MUser
    {
        public long Id;


        [BsonElement("plot_embedding")]
        public double[] Embedding {  get; set; }


        public DateTime CreatedAt { get; set; }

    }
}
