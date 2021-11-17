using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Theatrum.Dal.Abstract.IRepository.Base
{
    public interface IGenericKeyRepository<TKey, TEntity>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpsertAsync(TEntity value);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<int> GetCountAsync();
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate);

        //Task SaveAsync();
    }
}
