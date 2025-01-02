using MediatR;
using Nezam.EEs.Shared.Domain.Identity.User.DomainEvents;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Nezam.EES.Slice.Secretariat.Domains.Participant.Repositories;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Slice.Secretariat.Application.EventHandlers.Users;

public class DepartmentCreatedDomainEventHandler : INotificationHandler<DepartmentCreatedEvent>
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public DepartmentCreatedDomainEventHandler(IParticipantRepository participantRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _participantRepository = participantRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(DepartmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkManager.Begin();

        try
        {
            // Check if a participant already exists for the given user ID
            var existingParticipant = await _participantRepository.FindOneAsync(
                p => p.DepartmentId == notification.DepartmentId);

            if (existingParticipant != null)
            {
              
                    existingParticipant.UpdateName(notification.Title);
                    await _participantRepository.UpdateAsync(existingParticipant, autoSave: true);
               
              
            }
            else
            {
                
                    var newParticipant = new Participant(notification.DepartmentId,notification.Title);
                    await _participantRepository.AddAsync(newParticipant, autoSave: true);
            
            }

            await uow.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to handle DepartmentCreatedEvent", ex);
        }
    }
}
