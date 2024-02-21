namespace SlackApi.Data.Model
{
    public class Relation
    {

        public long RelationId { get; set; }
        public long UserId1 { get; set; }
        public long UserId2 { get; set; }
        public string RelationType { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }
    }
}
