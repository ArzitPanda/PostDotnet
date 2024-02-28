using MongoDB.Bson;

namespace SocialTree.Data.Dto.RequestDto
{
    public class SubCommentDto
    {

        public long UserId { get; set; }
        public string McommentId { get; set; }

        public string Content { get; set; } = string.Empty;


    }
}
