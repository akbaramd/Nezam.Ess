using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.Domain;
using Payeh.SharedKernel.Domain.Enumerations;
namespace Nezam.EES.Slice.Secretariat.Domains.Participant
{
    /// <summary>
    /// Enumeration representing different participant types (e.g., User vs Department).
    /// </summary>
    public class ParticipantType : Enumeration
    {
        public static ParticipantType User       = new ParticipantType(1, nameof(User));
        public static ParticipantType Department = new ParticipantType(2, nameof(Department));

        public ParticipantType(int id, string name) : base(id, name)
        {
        }
    }

    public class Participant : Entity
    {
        // Primary ID for the Participant
        public ParticipantId ParticipantId { get; private set; }

        // If the participant is a user, store the UserId here
        public UserId? UserId { get; private set; }

        // If the participant is a department, store the DepartmentId here
        public DepartmentId? DepartmentId { get; private set; }

        // Additional "authority" info (optional)
        public string? Authority { get; private set; }
        public string[] Authorities => Authority?.Split(',') ?? Array.Empty<string>();

        // Participant's display name
        public string Name { get; private set; }

        // Identifies whether this participant is a User or a Department
        public ParticipantType? ParticipantType { get; private set; }

        // EF Core constructor (private)
        private Participant() { }

        /// <summary>
        /// Constructor to create a participant from a UserId
        /// Sets ParticipantType to <see cref="ParticipantType.User"/>.
        /// </summary>
        /// <param name="userId">The user's ID in the identity system.</param>
        /// <param name="name">The participant's name or label.</param>
        /// <param name="authority">Optional authority data.</param>
        public Participant(UserId userId, string name, string? authority = null)
        {
            ParticipantId = ParticipantId.NewId();
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));

            Name = !string.IsNullOrWhiteSpace(name) 
                ? name 
                : throw new ArgumentNullException(nameof(name));

            Authority = authority;
            ParticipantType = ParticipantType.User;
            
            DepartmentId = null; // Clear for user-based participant
        }

        /// <summary>
        /// Constructor to create a participant from a DepartmentId
        /// Sets ParticipantType to <see cref="ParticipantType.Department"/>.
        /// </summary>
        /// <param name="departmentId">The department's ID.</param>
        /// <param name="name">The participant's name or label.</param>
        /// <param name="authority">Optional authority data.</param>
        public Participant(DepartmentId departmentId, string name, string? authority = null)
        {
            ParticipantId = ParticipantId.NewId();
            DepartmentId = departmentId ?? throw new ArgumentNullException(nameof(departmentId));

            Name = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentNullException(nameof(name));

            Authority = authority;
            ParticipantType = ParticipantType.Department;
            
            UserId = null; // Clear for department-based participant
        }

        /// <summary>
        /// The composite or single key for this entity (in this case, ParticipantId).
        /// </summary>
        public override object GetKey()
        {
            return ParticipantId;
        }

        // ------------------------
        // Update Methods
        // ------------------------

        /// <summary>
        /// Update the participant's display name.
        /// </summary>
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty or whitespace.", nameof(name));

            Name = name;
        }

        /// <summary>
        /// Update the authority field.
        /// </summary>
        public void UpdateAuthority(string authority)
        {
            if (string.IsNullOrWhiteSpace(authority))
                throw new ArgumentException("Authority cannot be empty or whitespace.", nameof(authority));

            Authority = authority;
        }

        /// <summary>
        /// Change or set the associated UserId (switch to a user-based participant).
        /// Sets <see cref="ParticipantType"/> to <see cref="ParticipantType.User"/>.
        /// </summary>
        public void UpdateUserId(UserId userId)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            DepartmentId = null;              // Clear department if switching to user
            ParticipantType = ParticipantType.User;
        }

        /// <summary>
        /// Clear the associated UserId (indicating no user-based participant).
        /// </summary>
        public void ClearUserId()
        {
            UserId = null;
        }

        /// <summary>
        /// Set the department reference by providing a valid DepartmentId 
        /// (switch to a department-based participant).
        /// </summary>
        public void SetDepartment(DepartmentId departmentId)
        {
            DepartmentId = departmentId ?? throw new ArgumentNullException(nameof(departmentId));
            UserId = null;                         // Clear user if switching to department
            ParticipantType = ParticipantType.Department;
        }

        /// <summary>
        /// Clear the associated department (if disassociation is needed).
        /// </summary>
        public void ClearDepartment()
        {
            DepartmentId = null;
        }

        /// <summary>
        /// Update the participant type directly (if you need to override manually).
        /// </summary>
        public void UpdateParticipantType(ParticipantType? participantType)
        {
            ParticipantType = participantType;
        }
    }
}
