using SocialTree.Data.Model.MongoModel;

namespace SocialTree.Data.Dto.ResponseDto
{
    public class PostDto
    {

        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }

        public string ImgUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public long AuthorId {  get; set; }
      
        public string UserName { get; set; }
        public string UserPhotoUrl { get; set; }



        public long Likecount {  get; set; }

        public List<string> RecentLikedUser {  get; set; } 

        public long CommentCount { get; set; }

        public List<MComment> TopComments { get; set; }



    }
}
