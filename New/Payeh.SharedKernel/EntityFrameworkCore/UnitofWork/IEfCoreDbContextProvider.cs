namespace Payeh.SharedKernel.EntityFrameworkCore.UnitofWork
{
    /// <summary>
    /// Defines the contract for providing a DbContext instance tied to a Unit of Work.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    public interface IEfCoreDbContextProvider<TDbContext> where TDbContext : IPayehDbContext
    {
        /// <summary>
        /// Retrieves or creates a DbContext instance and optionally initiates a transaction.
        /// </summary>
        /// <param name="createTransaction">Indicates whether a transaction should be created.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <returns>A Task resolving to the requested DbContext.</returns>
        Task<TDbContext> GetDbContextAsync(bool isReadOnly = false, CancellationToken cancellationToken = default);
    }
}