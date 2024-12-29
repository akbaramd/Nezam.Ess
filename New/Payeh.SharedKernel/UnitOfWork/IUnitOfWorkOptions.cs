
using System.Data;

namespace Payeh.SharedKernel.UnitOfWork;

public interface IUnitOfWorkOptions
{
        public bool IsUnitOfWorkEnabled { get; set; } 
        public bool IsTransactional { get; set; }  // Default to transactional
        IsolationLevel IsolationLevel { get; set; }

}