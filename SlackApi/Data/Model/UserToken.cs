namespace SlackApi.Data.Model
{
    public class UserToken
    {

        public long Id { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime Validity { get; set; }


    }
}
