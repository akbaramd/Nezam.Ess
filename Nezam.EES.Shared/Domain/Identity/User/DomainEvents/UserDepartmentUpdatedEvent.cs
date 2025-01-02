using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EEs.Shared.Domain.Identity.User.DomainEvents;

public class UserDepartmentUpdatedEvent : DomainEvent
{
    public UserId UserId { get; }
    public DepartmentId[] Departments { get; }

    public UserDepartmentUpdatedEvent(UserId userId, DepartmentId[] departments)
    {
        UserId = userId;
        Departments = departments;
    }
}