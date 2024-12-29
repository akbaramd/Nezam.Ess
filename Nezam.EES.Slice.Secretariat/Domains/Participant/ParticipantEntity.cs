using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.Domain;

namespace Nezam.EES.Slice.Secretariat.Domains.Participant;

public class Participant : Entity
{
    public ParticipantId ParticipantId { get; private set; } // Unique identifier for the participant
    public UserId? UserId { get; private set; } // Optional: User ID from external user system
    public string Name { get; private set; } // Name of the participant

    // Constructor for EF Core and other serialization frameworks
    private Participant() { }

    // Constructor to initialize a new Participant
    public Participant(string name)
    {
        ParticipantId = ParticipantId.NewId();
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
    }

    public override object GetKey()
    {
        return ParticipantId;
    }

    // Update the name of the participant
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty or whitespace.", nameof(name));
        }

        Name = name;
    }

    // Update the associated UserId
    public void UpdateUserId(UserId userId)
    {
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }

    // Clear the associated UserId (if disassociation is needed)
    public void ClearUserId()
    {
        UserId = null;
    }
}