
using System.Data;

public class UnitOfWorkOptions : IUnitOfWorkOptions
{
    public bool IsUnitOfWorkEnabled { get; set; } = true;
    public bool IsTransactional { get; set; } = true;
    public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;
}