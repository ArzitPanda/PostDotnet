using System;
using System.Threading.Tasks;

namespace SlackApi.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get;}
        IPostRepository PostRepository { get;}
        IRelationRequestRepository RelationRequestRepository { get;}

        ICredRepository CredentialRepository { get;}

        IRelationalRepository RelationRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
