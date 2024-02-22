namespace SlackApi.Data.Dto.RequestDto
{
    public class RelationCreateDto
    {

        public long Receiver {  get; set; }
        public long Sender { get; set; }
        public string RelationType { get; set; }
        


    }
}
