namespace SlackApi.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    namespace SlackApi.Data.Repository
    {
        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            public readonly SlackDbContext _dbContext;
            private readonly DbSet<T> _dbSet;

            public GenericRepository(SlackDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _dbSet = _dbContext.Set<T>();
            }

            public  IQueryable<T> GetAll()
            {
                return  _dbSet.AsQueryable();
            }

            public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
            {
                return await _dbSet.Where(expression).ToListAsync();
            }

            public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
            {
                IQueryable<T> query = _dbSet.Where(expression);

                // Apply includes
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.ToListAsync();
            }

            public async Task<T> Insert(T entity)
            {
                _dbSet.Add(entity);
               await  _dbContext.SaveChangesAsync();
                return entity;
            }

            public async Task<IQueryable<T>> Update(T entity)
            {
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
                return _dbSet;
            }

            public async Task<bool> Delete(long id)
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return false;

                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }

}
