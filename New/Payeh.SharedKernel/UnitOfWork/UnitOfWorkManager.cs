using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Payeh.SharedKernel.UnitOfWork.Null;

namespace Payeh.SharedKernel.UnitOfWork
{
    /// <summary>
    /// Manages Unit of Work instances, ensuring proper scope and lifecycle management.
    /// </summary>
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IUnitOfWorkOptions _defaultOptions;
        private bool _disposed;

        // AsyncLocal to store the current UnitOfWork per async flow
        private static readonly AsyncLocal<IUnitOfWork?> Current = new AsyncLocal<IUnitOfWork?>();

        /// <summary>
        /// Unique identifier for the UnitOfWorkManager instance.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkManager"/> class.
        /// </summary>
        /// <param name="serviceScopeFactory">Factory to create service scopes for UnitOfWork instances.</param>
        /// <param name="defaultOptions">Default options for UnitOfWork initialization.</param>
        public UnitOfWorkManager(IServiceScopeFactory serviceScopeFactory, IUnitOfWorkOptions defaultOptions)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _defaultOptions = defaultOptions ?? throw new ArgumentNullException(nameof(defaultOptions));
        }

        /// <summary>
        /// Tracks the currently active UnitOfWork within the async context.
        /// Provides a mechanism to manage transactional operations scoped to the async flow.
        /// </summary>
        public IUnitOfWork CurrentUnitOfWork
        {
            get
            {
                return Current.Value ?? throw new InvalidOperationException(
                    "No active UnitOfWork. Call Begin() to start a new UnitOfWork.");
            }
        }

        /// <summary>
        /// Begins a new UnitOfWork or creates a child UnitOfWork if one is already active.
        /// </summary>
        /// <param name="options">Options for the UnitOfWork.</param>
        /// <returns>A new or child UnitOfWork instance.</returns>
        public IUnitOfWork Begin(IUnitOfWorkOptions? options = null)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(UnitOfWorkManager));

            if (Current.Value != null)
            {
                return new ChildUnitOfWork(Current.Value);
            }

            return CreateNewUnitOfWork(options ?? _defaultOptions);
        }

        /// <summary>
        /// Disposes the UnitOfWorkManager and clears the current UnitOfWork.
        /// Releases any associated resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

            if (Current.Value != null)
            {
                Current.Value.Dispose();
                Current.Value = null;
            }

            _disposed = true;
        }

        /// <summary>
        /// Creates a new UnitOfWork with its own service scope.
        /// </summary>
        /// <param name="options">Options for the UnitOfWork.</param>
        /// <returns>A new UnitOfWork instance.</returns>
        private IUnitOfWork CreateNewUnitOfWork(IUnitOfWorkOptions options)
        {
            var scope = _serviceScopeFactory.CreateScope();

            try
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                unitOfWork.Initialize(options);

                Current.Value = unitOfWork;

                unitOfWork.Disposed += (sender, args) =>
                {
                    Current.Value = null;
                    scope.Dispose();
                };

                return unitOfWork;
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }
    }
}