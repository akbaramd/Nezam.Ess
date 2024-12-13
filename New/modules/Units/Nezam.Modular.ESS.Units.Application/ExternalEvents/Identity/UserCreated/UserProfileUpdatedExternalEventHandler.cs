using MediatR;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Shared.User.DomainEvents;
using Nezam.Modular.ESS.Units.Domain.Member;
using Nezam.Modular.ESS.Units.Domain.Shared.Members;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.Units.Application.ExternalEvents.Identity.UserCreated;

public class UserProfileUpdatedExternalEventHandler : INotificationHandler<UserProfileUpdatedEvent>
{
    public IMemberRepository MemberRepository { get; }

    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ILogger<UserProfileUpdatedExternalEventHandler> _logger;

    public UserProfileUpdatedExternalEventHandler(
   
        IUnitOfWorkManager unitOfWorkManager,
        IMemberRepository memberRepository,
        ILogger<UserProfileUpdatedExternalEventHandler> logger)
    {
        MemberRepository = memberRepository;

        _unitOfWorkManager = unitOfWorkManager ?? throw new ArgumentNullException(nameof(unitOfWorkManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(UserProfileUpdatedEvent @event, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkManager.Begin();
        if (@event == null)
        {
            _logger.LogError("UserCreatedEvent is null.");
            throw new ArgumentNullException(nameof(@event), "UserCreatedEvent cannot be null.");
        }

        try
        {
            _logger.LogInformation("Handling UserCreatedEvent for UserId: {UserId}", @event.UserId);

        
            var member = await MemberRepository.FindOneAsync(x=>x.UserId == @event.UserId);
            if (member != null)
            {
                _logger.LogInformation("Member found for UserId: {UserId}, updating member.", @event.UserId);
                member.SetName(@event.NewProfile.FirstName, @event.NewProfile.LastName);
                await MemberRepository.UpdateAsync(member, true);
            }
            else
            {
                _logger.LogInformation("No member found for UserId: {UserId}, creating new member.", @event.UserId);
                member = new MemberEntity(MemberId.NewId(), @event.UserId, @event.NewProfile?.FirstName, @event.NewProfile?.LastName);
                await MemberRepository.AddAsync(member, true);
            }

            await uow.CommitAsync();
            _logger.LogInformation("UserCreatedEvent successfully handled for UserId: {UserId}", @event.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling UserCreatedEvent for UserId: {UserId}", @event.UserId);
            throw; // Re-throw the exception to allow the caller to handle it
        }
    }
}
