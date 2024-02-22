namespace SlackApi.Data.Model
{
    public class Relation
    {

        public long RelationId { get; set; }
        public long UserId1 { get; set; }  //who get the req
        public long UserId2 { get; set; } // who send the req
        public string RelationType { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }
    }
}
