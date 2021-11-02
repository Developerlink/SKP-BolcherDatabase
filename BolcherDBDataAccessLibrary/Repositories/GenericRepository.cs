using BolcherDBModelLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolcherDBDataAccessLibrary.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly TContext Context;

        public GenericRepository(TContext context)
        {
            Context = context;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            await SaveAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entityToDelete = await GetByIdAsync(id);
            Context.Set<TEntity>().Remove(entityToDelete);
            await SaveAsync();
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            var result = await Context.Set<TEntity>().FindAsync(id);
            if (result != null)
                return true;
            return false;
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            var entities = await Context.Set<TEntity>().ToListAsync();
            return entities;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            return entity;
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            await SaveAsync();
        }
    }
}