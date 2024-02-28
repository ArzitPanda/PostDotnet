using SlackApi.Data.Model;
using SlackApi.Data.Repository;

namespace SlackApi.Services.FeedService
{
    public class FeedService : IFeedService
    {

        private readonly IUnitOfWork _unitOfWork;

      

        public FeedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Post>> GetFeedById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetFeedByIdAndType(long id,string Type)
        {

            var result = await _unitOfWork.RelationRepository.Find(a => (a.UserId1 == id || a.UserId2 == id) && a.RelationType==Type);

           Console.WriteLine(result.ToList().Count);

            var userIds = result.Select(r => { if (r.UserId1 == id) { return r.UserId2; } return r.UserId1; }).ToList();


            Console.WriteLine("i am here un getfeed");
            for(var i = 0;i<userIds.Count;i++)
            {
                Console.WriteLine(userIds[i]);
            }

            var posts = await _unitOfWork.PostRepository.Find(a=>userIds.Contains(a.AuthorId) && a.Visibility.Contains(Type),a=>a.Author);

            return posts;


        }
    }
}
