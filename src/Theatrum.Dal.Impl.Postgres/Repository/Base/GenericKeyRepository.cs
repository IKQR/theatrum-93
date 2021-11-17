using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Theatrum.Dal.Abstract.IRepository.Base;

namespace Theatrum.Dal.Impl.Postgres.Repository.Base
{
    public abstract class GenericKeyRepository<TKey, TEntity, TContext> : IGenericKeyRepository<TKey, TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        public GenericKeyRepository(TContext context)
        {
            Context = context;
        }

        public TContext Context { get; }

        public DbSet<TEntity> DbSet => Context.Set<TEntity>();

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var item = await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return item.Entity;
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task UpsertAsync(TEntity value)
        {
            var entry = Context.Entry(value);
            //TODO 
            var key = entry.Metadata
                .FindPrimaryKey()
                .Properties
                .Select(prop => entry.Property(prop.Name).CurrentValue)
                .ToArray();

            var valueInDb = await DbSet.FindAsync(key);
            if (valueInDb != null)
            {
                var existingEntry = Context.Entry(valueInDb);
                existingEntry.State = EntityState.Detached;
                await UpdateAsync(value);
            }
            else
            {
                await AddAsync(value);
            }

            await Context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            TEntity result = Context.Set<TEntity>()
                .Remove(entity).Entity;
            await Context.SaveChangesAsync();

            return result;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetByAsync
            (Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }


        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await Context.Set<TEntity>()
                .FindAsync(id);
        }


        public virtual async Task<int> GetCountAsync()
        {
            return await Context.Set<TEntity>().CountAsync();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).CountAsync();
        }

        public virtual async Task<List<TEntity>> FetchByAsync
            (Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync
            (Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
    }
}
