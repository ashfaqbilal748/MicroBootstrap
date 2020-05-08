using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Types;
using MongoDB.Driver.Linq;
using System.Linq;

namespace Common.Mongo
{
    public interface IMongoRepository<TEntity> where TEntity : IIdentifiable
    {
         Task<TEntity> GetAsync(Guid id);
         Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
         IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
         Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
           Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
				TQuery query) where TQuery : PagedQueryBase;
         Task AddAsync(TEntity entity);
         Task UpdateAsync(TEntity entity);
         Task DeleteAsync(Guid id); 
         Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}