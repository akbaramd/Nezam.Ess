using Bonyan.Layer.Domain.Entities;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.IdEntity.Domain.User;
using System;

namespace Nezam.Modular.ESS.IdEntity.Domain.Employer
{
    public class EmployerEntity : BonEntity<EmployerId>
    {
        // Constructors
        protected EmployerEntity() { }

        public EmployerEntity(EmployerId employerId, BonUserId bonUserId, string firstName, string? lastName)
        {
            Id = employerId ?? throw new ArgumentNullException(nameof(employerId));
            BonUserId = bonUserId ?? throw new ArgumentNullException(nameof(bonUserId));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName;
        }

        // Properties
        public BonUserId BonUserId { get; private set; }
        public UserEntity BonUser { get; set; }
        public string FirstName { get; private set; }
        public string? LastName { get; private set; }

        // Update Methods

        /// <summary>
        /// Updates the employer's personal details.
        /// </summary>
        public void UpdateDetails(string firstName, string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.", nameof(firstName));

            FirstName = firstName;
            LastName = lastName;
            // Trigger domain event: EmployerDetailsUpdated (if needed)
        }
    }

    // Custom ID class for EmployerEntity
    public class EmployerId : BonBusinessId<EmployerId>
    {
    }
}