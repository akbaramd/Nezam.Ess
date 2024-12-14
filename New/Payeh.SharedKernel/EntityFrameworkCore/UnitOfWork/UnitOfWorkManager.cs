using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork
{
    public class UnitOfWorkManager<TDbContext> : IUnitOfWorkManager where TDbContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope? _currentScope;
        private IUnitOfWork? _currentUnitOfWork;
        private bool _disposed;

        public UnitOfWorkManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IUnitOfWork CurrentUnitOfWork
        {
            get
            {
                if (_currentUnitOfWork == null)
                {
                    throw new InvalidOperationException(
                        "No active UnitOfWork. Call Begin() to start a new UnitOfWork.");
                }

                return _currentUnitOfWork;
            }
        }

        public IUnitOfWork Begin(bool startTransaction = true)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWorkManager<TDbContext>));
            }

            if (_currentUnitOfWork != null)
            {
                return new NullUnitOfWork<TDbContext>();
            }

            _currentScope = _serviceProvider.CreateScope();
            var dbContext = _currentScope.ServiceProvider.GetRequiredService<TDbContext>();
            var mediator = _currentScope.ServiceProvider.GetRequiredService<IMediator>();

            var unitOfWork = new UnitOfWork<TDbContext>(dbContext, mediator);

            if (startTransaction)
            {
                unitOfWork.BeginTransaction();
            }

            unitOfWork.Disposed += OnUnitOfWorkDisposed;
            _currentUnitOfWork = unitOfWork;

            return _currentUnitOfWork;
        }

        private void OnUnitOfWorkDisposed(object? sender, EventArgs e)
        {
            if (sender == _currentUnitOfWork)
            {
                _currentUnitOfWork = null;
                _currentScope?.Dispose();
                _currentScope = null;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _currentUnitOfWork?.Dispose();
            _currentScope?.Dispose();

            _currentUnitOfWork = null;
            _currentScope = null;
            _disposed = true;
        }
    }
}