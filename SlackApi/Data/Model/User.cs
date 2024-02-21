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

        public ICollection<Relation> Relations { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<RelationRequest> Requests { get; set; }

       
       
   
        
        public override string ToString()
        {
            return $"User: {UserId}, Name: {UserName}, Email: {UserEmail}, Phone: {UserPhone}, Date of Birth: {DateOfBirth}, Photo URL: {PhotoUrl}";
        }
    }
}
