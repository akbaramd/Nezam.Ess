using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EEs.Shared.Domain.Identity.User.DomainEvents
{
    public class DepartmentCreatedEvent : DomainEvent
    {
        public DepartmentId DepartmentId { get; }
        public string Title { get; }

        public DepartmentCreatedEvent(DepartmentId userId, string title)
        {
            DepartmentId = userId;
            Title = title;
        }
    }
}
