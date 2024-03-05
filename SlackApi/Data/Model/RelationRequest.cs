using System.ComponentModel.DataAnnotations.Schema;

namespace SlackApi.Data.Model
{
    public class RelationRequest
    {

        public long Id { get; set; }

        [ForeignKey("User")]
        public long UserId {  get; set; }
        public User Requestor { get; set; }
        public User User { get; set; }


        public string RelationShip {  get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
