using Bonyan.Layer.Domain.Entities;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.IdEntity.Domain.User;
using System;

namespace Nezam.Modular.ESS.IdEntity.Domain.Engineer
{
    public class EngineerEntity : BonEntity<EngineerId>
    {
        // Constructors
        protected EngineerEntity() { }

        public EngineerEntity(EngineerId engineerId, BonUserId userId, string? firstName, string? lastName, string registrationNumber)
        {
            Id = engineerId ?? throw new ArgumentNullException(nameof(engineerId));
            BonUserId = userId;
            FirstName = firstName;
            LastName = lastName;
            RegistrationNumber = registrationNumber ?? throw new ArgumentNullException(nameof(registrationNumber));
        }

        // Properties
        public BonUserId BonUserId { get; private set; }
        public UserEntity BonUser { get; set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string RegistrationNumber { get; private set; }

        // Update Methods

        /// <summary>
        /// Updates the engineer's personal information.
        /// </summary>
        public void UpdateDetails(string? firstName, string? lastName, string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber)) throw new ArgumentException("Registration number is required.", nameof(registrationNumber));

            FirstName = firstName;
            LastName = lastName;
            RegistrationNumber = registrationNumber;
            // Trigger domain event: EngineerDetailsUpdated (if needed)
        }

        // Example Behaviors (Status Management)

        /// <summary>
        /// Checks if the engineer has a valid registration number.
        /// </summary>
        public bool HasValidRegistration() => !string.IsNullOrWhiteSpace(RegistrationNumber);

        /// <summary>
        /// Resets the engineer's registration number, for example, in case of a revocation.
        /// </summary>
        public void ResetRegistration()
        {
            RegistrationNumber = string.Empty;
            // Trigger domain event: EngineerRegistrationReset (if needed)
        }

        // Optional Domain Events
        // Define and trigger domain events such as EngineerDetailsUpdated or EngineerRegistrationReset
        // if other parts of the system need to react to these changes.
    }

    // Custom ID class for EngineerEntity
    public class EngineerId : BonBusinessId<EngineerId>
    {
    }
}
