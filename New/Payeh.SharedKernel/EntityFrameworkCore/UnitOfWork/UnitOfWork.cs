using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Payeh.SharedKernel.Domain;

namespace Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private readonly IMediator _mediator;
        private IDbContextTransaction? _transaction;
        private readonly List<IUnitOfWork> _nestedUnits = new();
        private bool _isRootTransaction = true;
        private bool _isDisposed;

        public event EventHandler? Disposed;

        public UnitOfWork(TDbContext context, IMediator mediator, IDbContextTransaction? parentTransaction = null)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _transaction = parentTransaction;
            _isRootTransaction = parentTransaction == null;
        }

        public void BeginTransaction()
        {
            if (_isRootTransaction && _transaction == null)
            {
                _transaction = _context.Database.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_isRootTransaction && _transaction != null)
            {
                _context.SaveChanges();
                PublishDomainEventsAsync().Wait();
                _transaction.Commit();
                DisposeTransaction();
            }
            else
            {
                _context.SaveChanges();
            }
        }

        public async Task CommitAsync()
        {
            if (_isRootTransaction && _transaction != null)
            {
                await _context.SaveChangesAsync();
                await PublishDomainEventsAsync();
                await _transaction.CommitAsync();
                DisposeTransaction();
            }
            else
            {
                await _context.SaveChangesAsync();
            }
        }

        public void Rollback()
        {
            if (_isRootTransaction && _transaction != null)
            {
                _transaction.Rollback();
                DisposeTransaction();
            }
        }

        public async Task RollbackAsync()
        {
            if (_isRootTransaction && _transaction != null)
            {
                await _transaction.RollbackAsync();
                DisposeTransaction();
            }
        }

        public IUnitOfWork CreateNestedUnitOfWork()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWork<TDbContext>), "Cannot create a nested unit of work on a disposed unit of work.");
            }

            var nestedUnitOfWork = new UnitOfWork<TDbContext>(_context, _mediator, _transaction);
            _nestedUnits.Add(nestedUnitOfWork);
            nestedUnitOfWork.Disposed += (sender, args) => _nestedUnits.Remove(nestedUnitOfWork);
            return nestedUnitOfWork;
        }

        private async Task PublishDomainEventsAsync()
        {
            var entitiesWithEvents = _context.ChangeTracker.Entries<AggregateRoot>()
                .Where(entry => entry.Entity.DomainEvents.Any())
                .Select(entry => entry.Entity)
                .ToList();

            var domainEvents = entitiesWithEvents.SelectMany(e => e.DomainEvents).ToList();

            foreach (var entity in entitiesWithEvents)
            {
                entity.ClearDomainEvents();
            }

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }

        public DbContext GetDbContext()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWork<TDbContext>), "Cannot access the DbContext of a disposed unit of work.");
            }

            return _context;
        }

        private void DisposeTransaction()
        {
            _transaction?.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;

            foreach (var child in _nestedUnits.ToList())
            {
                child.Dispose();
            }

            DisposeTransaction();
            _context.Dispose();
            Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}
