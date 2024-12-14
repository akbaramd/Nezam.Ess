using Microsoft.EntityFrameworkCore;
using Payeh.SharedKernel.Domain;
using Payeh.SharedKernel.Domain.Repositories;

namespace Payeh.SharedKernel.EntityFrameworkCore.Domain
{
    public static class RepositoryExtensions
    {
        public static TDbContext GetDbContext<TEntity, TDbContext>(this IRepository<TEntity> repository)
            where TEntity : Entity
            where TDbContext : DbContext
        {
            if (repository is EntityFrameworkRepository<TEntity, TDbContext> efRepository)
            {
                return efRepository.Context;
            }

            throw new InvalidOperationException(
                $"The repository is not an EntityFrameworkRepository<{typeof(TEntity).Name}, {typeof(TDbContext).Name}>");
        }

        public static DbSet<TEntity> GetDbSet<TEntity, TDbContext>(this IRepository<TEntity> repository)
            where TEntity : Entity
            where TDbContext : DbContext
        {
            if (repository is EntityFrameworkRepository<TEntity, TDbContext> efRepository)
            {
                return efRepository.DbSet;
            }

            throw new InvalidOperationException(
                $"The repository is not an EntityFrameworkRepository<{typeof(TEntity).Name}, {typeof(TDbContext).Name}>");
        }
    }
}