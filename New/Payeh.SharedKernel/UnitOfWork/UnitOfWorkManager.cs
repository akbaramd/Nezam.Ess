using Payeh.SharedKernel.UnitOfWork.Null;

namespace Payeh.SharedKernel.UnitOfWork
{
    public class UnitOfWorkManager : IUnitOfWorkManager 
    {
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope? _currentScope;
        private IUnitOfWork? _currentUnitOfWork;
        private IUnitOfWorkOptions _unitOfWorkOptions;
        private bool _disposed;

        public UnitOfWorkManager(IServiceProvider serviceProvider, IUnitOfWorkOptions unitOfWorkOptions)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _unitOfWorkOptions = unitOfWorkOptions;
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

        public IUnitOfWork Begin(IUnitOfWorkOptions? options = null)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWorkManager));
            }
            
            if (_currentUnitOfWork != null)
            {
                return new NullUnitOfWork();
            }

            _currentScope = _serviceProvider.CreateScope();

            var unitOfWork = _currentScope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            options ??= _unitOfWorkOptions;

            unitOfWork.Initialize(options);
            
            if (unitOfWork == null)
            {
                throw new ArgumentNullException();
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