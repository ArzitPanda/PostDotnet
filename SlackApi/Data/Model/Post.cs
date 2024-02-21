namespace SlackApi.Data.Model
{
    public class Post
    {

        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }

        public string[]  Visibility {  get; set; } 
        public long AuthorId { get; set; }
        public User Author { get; set; }    
        public  DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Comment> Comment {  get; set; }

      

    }
}
