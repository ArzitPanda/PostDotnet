namespace SlackApi.Data.Model
{
    public class UserAuthorization
    {
        public long Id { get; set; }
        public User user { get; set; }
        public string UserAuthorizeCode { get; set; }

    }
}
