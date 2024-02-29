using SocialTree.Data.Model;
using System.Text.Json.Serialization;

namespace SlackApi.Data.Model
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }

        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;

        public string PhotoUrl { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
        [JsonIgnore]
        public ICollection<Relation> Relations { get; set; }


        [JsonIgnore]
        public ICollection<Post> Posts { get; set; }
        [JsonIgnore]
        public ICollection<RelationRequest> Requests { get; set; }



        [JsonIgnore]
        public UserVerification UserVerification { get; set; }
        
        public override string ToString()
        {
            return $"User: {UserId}, Name: {UserName}, Email: {UserEmail}, Phone: {UserPhone}, Date of Birth: {DateOfBirth}, Photo URL: {PhotoUrl}";
        }
    }
}
