using System;
using System.Threading.Tasks;

namespace SlackApi.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SlackDbContext _context;

        public IUserRepository UserRepository { get; }
        public IPostRepository PostRepository { get; }
        public IRelationRequestRepository RelationRequestRepository { get; }

        public  ICredRepository CredentialRepository { get; }

        

        public UnitOfWork(SlackDbContext context, IUserRepository userRepository, IPostRepository postRepository, IRelationRequestRepository relationRequestRepository,
            ICredRepository credRepository
            
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            PostRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            RelationRequestRepository = relationRequestRepository ?? throw new ArgumentNullException(nameof(relationRequestRepository));
            CredentialRepository = credRepository ?? throw new ArgumentNullException(nameof(CredentialRepository));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
