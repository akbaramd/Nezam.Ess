using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payeh.SharedKernel.Domain;

namespace Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork
{
    public class NullUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        public event EventHandler? Disposed;

        public void BeginTransaction()
        {
            // Do nothing
        }

        public void Commit()
        {
            // Do nothing
        }

        public async Task CommitAsync()
        {
            // Do nothing asynchronously
            await Task.CompletedTask;
        }

        public void Rollback()
        {
            // Do nothing
        }

        public async Task RollbackAsync()
        {
            // Do nothing asynchronously
            await Task.CompletedTask;
        }

        public IUnitOfWork CreateNestedUnitOfWork()
        {
            // Return another NullUnitOfWork instance
            return new NullUnitOfWork<TDbContext>();
        }

        public DbContext GetDbContext()
        {
            // Return null or throw an exception, as this is a null object
            throw new InvalidOperationException("NullUnitOfWork does not provide a DbContext.");
        }

        public void Dispose()
        {
            // Trigger Disposed event, if any listeners are registered
            Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}