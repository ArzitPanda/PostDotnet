namespace SlackApi.Data.Model
{
    public class Comment
    {


        public long Id { get; set; }
        public User Commentor { get; set; }
        public string Text { get; set; }

        public Post Post { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}



        public ICollection<User> Likes{ get; set;}

    }
}
