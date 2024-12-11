using Bonyan.Mediators;
using Bonyan.UnitOfWork;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.User.Events;
using Nezam.Modular.ESS.Units.Domain.Member;
using Nezam.Modular.ESS.Units.Domain.Shared.Members;

namespace Nezam.Modular.ESS.Units.Application.ExternalEvents.Identity.UserCreated;

public class UserCreatedExternalEventHandler : IBonEventHandler<UserCreatedEvent>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IBonUnitOfWorkManager _unitOfWorkManager;
    private readonly ILogger<UserCreatedExternalEventHandler> _logger;

    public UserCreatedExternalEventHandler(
        IMemberRepository memberRepository,
        IBonUnitOfWorkManager unitOfWorkManager,
        ILogger<UserCreatedExternalEventHandler> logger)
    {
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        _unitOfWorkManager = unitOfWorkManager ?? throw new ArgumentNullException(nameof(unitOfWorkManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task HandleAsync(UserCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            _logger.LogError("UserCreatedEvent is null.");
            throw new ArgumentNullException(nameof(@event), "UserCreatedEvent cannot be null.");
        }

        try
        {
            _logger.LogInformation("Handling UserCreatedEvent for UserId: {UserId}", @event.UserId);

            using var uow = _unitOfWorkManager.Begin();
            
            var member = await _memberRepository.FindByUserIdAsync(@event.UserId);
            if (member != null)
            {
                _logger.LogInformation("Member found for UserId: {UserId}, updating member.", @event.UserId);
                member.SetName(@event.Profile.FirstName, @event.Profile.LastName);
                await _memberRepository.UpdateAsync(member, true);
            }
            else
            {
                _logger.LogInformation("No member found for UserId: {UserId}, creating new member.", @event.UserId);
                member = new MemberEntity(MemberId.NewId(), @event.UserId, @event.Profile.FirstName, @event.Profile.LastName);
                await _memberRepository.AddAsync(member, true);
            }

            await uow.CompleteAsync(cancellationToken);
            _logger.LogInformation("UserCreatedEvent successfully handled for UserId: {UserId}", @event.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling UserCreatedEvent for UserId: {UserId}", @event.UserId);
            throw; // Re-throw the exception to allow the caller to handle it
        }
    }
}
