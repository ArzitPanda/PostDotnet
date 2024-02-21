namespace SlackApi.Data.Model
{
    public class Thread
    {
        public long Id { get; set; }    
        public string Content { get; set; }
        public Comment Comment { get; set; }

        public long UserId {  get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
