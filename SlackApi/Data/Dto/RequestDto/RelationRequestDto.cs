namespace SlackApi.Data.Dto.RequestDto
{
    public class RelationRequestDto
    {
      
        public long UserId { get; set; }

        public string RelationShip {  get; set; }

        public long RequestorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
