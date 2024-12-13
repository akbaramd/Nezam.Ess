using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Payeh.SharedKernel.Domain;
using Payeh.SharedKernel.Domain.Repositories;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Payeh.SharedKernel.EntityFrameworkCore.Domain;

public class EntityFrameworkRepository<TEntity,TDbContext>: IRepository<TEntity> where TEntity : Entity where TDbContext : DbContext
    {
        
        private IUnitOfWorkManager _unitOfWork;

        public EntityFrameworkRepository( IUnitOfWorkManager unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        internal TDbContext Context => (TDbContext)_unitOfWork.CurrentUnitOfWork.GetDbContext();
        internal DbSet<TEntity> DbSet =>Context.Set<TEntity>();
        public async Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            return await Task.FromResult(DbSet.AsQueryable());
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await FindOneAsync(predicate);
            if (entity == null)
                throw new InvalidOperationException("Entity not found.");

            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.CountAsync(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }

        public async Task<TEntity> AddAsync(TEntity entity, bool autoSave = false)
        {
            await DbSet.AddAsync(entity);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }

            return entity;
        }

        public async Task UpdateAsync(TEntity entity, bool autoSave = false)
        {
            DbSet.Update(entity);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(TEntity entity, bool autoSave = false)
        {
            DbSet.Remove(entity);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            await DbSet.AddRangeAsync(entities);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            DbSet.UpdateRange(entities);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            DbSet.RemoveRange(entities);
            if (autoSave)
            {
                await Context.SaveChangesAsync();
            }
        }
    }