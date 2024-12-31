using Payeh.SharedKernel.UnitOfWork.Child;
using Payeh.SharedKernel.UnitOfWork.Null;

namespace Payeh.SharedKernel.UnitOfWork
{
    public class UnitOfWorkManager : IUnitOfWorkManager 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        private readonly IServiceScopeFactory _serviceProvider;
        private readonly IUnitOfWorkOptions _unitOfWorkOptions;
        private bool _disposed;

        // Using AsyncLocal to store the current UnitOfWork per async flow
        private static readonly AsyncLocal<IUnitOfWork?> _currentUnitOfWork = new AsyncLocal<IUnitOfWork?>();

        public UnitOfWorkManager(IServiceScopeFactory serviceProvider, IUnitOfWorkOptions unitOfWorkOptions)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _unitOfWorkOptions = unitOfWorkOptions;
        }

        public IUnitOfWork CurrentUnitOfWork
        {
            get
            {
                return _currentUnitOfWork.Value ?? throw new InvalidOperationException(
                    "No active UnitOfWork. Call Begin() to start a new UnitOfWork.");
            }
        }

        public IUnitOfWork Begin(IUnitOfWorkOptions? options = null)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWorkManager));
            }

              
            if (_currentUnitOfWork.Value != null)
            {
                return new ChildUnitOfWork(_currentUnitOfWork.Value);
            }
            // Create a new scope for the UnitOfWork
            return CreateNewUnitOfWork();
        }

        public void Dispose()
        {
            if (_disposed) return;

            if (_currentUnitOfWork.Value != null)
            {
                _currentUnitOfWork.Value.Dispose();
                _currentUnitOfWork.Value = null;
            }

            _disposed = true;
        }
        
        private IUnitOfWork CreateNewUnitOfWork()
        {
            var scope = _serviceProvider.CreateScope();
            try
            {

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();


                _currentUnitOfWork.Value = unitOfWork;

                unitOfWork.Disposed += (sender, args) =>
                {      _currentUnitOfWork.Value = null;
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
