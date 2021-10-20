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

        public async Task<bool> AddAsync(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await GetByIdAsync(id);
            Context.Set<TEntity>().Remove(entityToDelete);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return await SaveAsync();
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

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            return entity;
        }

        public async Task<bool> SaveAsync()
        {
            var rowsChanged = await Context.SaveChangesAsync();
            return rowsChanged >= 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return await SaveAsync();
        }
    }
}