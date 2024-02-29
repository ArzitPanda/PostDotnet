using SlackApi.Data.Model;
using SlackApi.Data.Repository;
using SocialTree.Data.Dto.ResponseDto;

namespace SlackApi.Services.FeedService
{
    public interface IFeedService
    {

      public Task<IEnumerable<Post>> GetFeedById(int id);

        public Task<IEnumerable<PostDto>> GetFeedByIdAndType(long id,string Type);

        




    }
}
