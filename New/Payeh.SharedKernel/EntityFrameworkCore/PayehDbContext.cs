using MediatR;
using Microsoft.EntityFrameworkCore;
using Payeh.SharedKernel.Domain;
using Payeh.SharedKernel.UnitOfWork;

namespace Payeh.SharedKernel.EntityFrameworkCore;

public class PayehDbContext<TDbContext> : DbContext,IPayehDbContext where TDbContext : DbContext
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    public PayehDbContext(DbContextOptions<TDbContext> options , IUnitOfWorkManager unitOfWorkManager) : base(options)
    {
        _unitOfWorkManager =unitOfWorkManager;
    }

    public override int SaveChanges()
    {
        var result = base.SaveChanges();
        PublishDomainEventsAsync().Wait();
        return result;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        await PublishDomainEventsAsync();
        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        // Collect all entities that have domain events
        var entitiesWithEvents = ChangeTracker.Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        // Collect all domain events
        var domainEvents = entitiesWithEvents
            .SelectMany(e => e.DomainEvents)
            .ToList();

        // Publish domain events
        foreach (var domainEvent in domainEvents)
        {
           _unitOfWorkManager.CurrentUnitOfWork.AddDomainEvent(domainEvent);
        }

        // Clear domain events
        foreach (var entity in entitiesWithEvents)
        {
            entity.ClearDomainEvents();
        }
    }
}