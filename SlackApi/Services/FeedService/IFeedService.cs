using SlackApi.Data.Model;
using SlackApi.Data.Repository;

namespace SlackApi.Services.FeedService
{
    public interface IFeedService
    {

      public Task<IEnumerable<Post>> GetFeedById(int id);

        public Task<IEnumerable<Post>> GetFeedByIdAndType(long id,string Type);

        




    }
}
