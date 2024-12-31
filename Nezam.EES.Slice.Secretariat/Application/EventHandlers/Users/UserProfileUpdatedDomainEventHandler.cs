using MediatR;
using Nezam.EEs.Shared.Domain.Identity.User.DomainEvents;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Nezam.EES.Slice.Secretariat.Domains.Participant.Repositories;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Slice.Secretariat.Application.EventHandlers.Users;

public class UserProfileUpdatedDomainEventHandler : INotificationHandler<UserProfileUpdatedEvent>
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public UserProfileUpdatedDomainEventHandler(IParticipantRepository participantRepository,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _participantRepository = participantRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(UserProfileUpdatedEvent notification, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkManager.Begin();

        try
        {
            // Check if a participant already exists for the given user ID
            var existingParticipant = await _participantRepository.FindOneAsync(
                p => p.UserId == notification.UserId);

            if (existingParticipant != null)
            {
                // Update the existing participant
                if (notification.NewProfile != null)
                {
                    existingParticipant.UpdateName(
                        $"{notification.NewProfile.FirstName} {notification.NewProfile.LastName}");
                    await _participantRepository.UpdateAsync(existingParticipant, autoSave: true);
                }
            }
            else
            {
                // Create a new participant
                if (notification.NewProfile != null)
                {
                    var newParticipant = new Participant(
                        $"{notification.NewProfile.FirstName} {notification.NewProfile.LastName}");
                    newParticipant.UpdateUserId(notification.UserId);
                    await _participantRepository.AddAsync(newParticipant, autoSave: true);
                }
            }
            await uow.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to handle UserCreatedEvent", ex);
        }
    }
}