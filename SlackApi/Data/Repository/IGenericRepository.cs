using System.Linq.Expressions;

namespace SlackApi.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        public Task<IEnumerable<T>> GetAll();

        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        public Task<T>  Insert(T entity);
        public Task<IQueryable<T>> Update(T entity);

        public Task<bool> Delete(long id);

    }
}
