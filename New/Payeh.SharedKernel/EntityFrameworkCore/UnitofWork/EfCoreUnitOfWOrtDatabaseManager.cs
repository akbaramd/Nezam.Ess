using Microsoft.EntityFrameworkCore;
using Payeh.SharedKernel.Domain;
using Payeh.SharedKernel.Domain.DomainEvents;
using Payeh.SharedKernel.UnitOfWork;

namespace Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;

public class EfCoreUnitOfWOrtDatabaseManager : IUnitOfWOrtDatabaseManager
{
    /// <summary>
    /// The DbContext instance managed by this storage.
    /// </summary>
    internal IPayehDbContext DbContext { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EfCoreUnitOfWOrtDatabaseManager"/> class.
    /// </summary>
    /// <param name="dbContext">The DbContext to be managed.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided DbContext is null.</exception>
    public EfCoreUnitOfWOrtDatabaseManager(IPayehDbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("An error occurred while saving changes to the database.", ex);
        }
    }

    /// <summary>
    /// Retrieves all domain events currently tracked by the DbContext.
    /// </summary>
    /// <returns>A collection of domain events.</returns>
    public IEnumerable<IDomainEvent> GetDomainEvents()
    {
        try
        {
            return DbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .SelectMany(entry => entry.Entity.DomainEvents)
                .ToList();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve domain events from the DbContext.", ex);
        }
    }

    /// <summary>
    /// Clears all domain events currently tracked by the DbContext.
    /// </summary>
    public void ClearDomainEvents()
    {
        try
        {
            foreach (var entry in DbContext.ChangeTracker.Entries<AggregateRoot>())
            {
                entry.Entity.ClearDomainEvents();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to clear domain events from the DbContext.", ex);
        }
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}