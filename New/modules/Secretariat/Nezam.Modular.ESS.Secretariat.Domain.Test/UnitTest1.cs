using System;
using Xunit;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Bonyan.UserManagement.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

public class DocumentAggregateRootTests
{
    [Fact]
    public void Only_First_Response_Should_Be_Accepted_When_Multiple_Referrals()
    {
        // Arrange
        var senderId = UserId.CreateNew();
        var managerId = UserId.CreateNew();
        var employee1Id = UserId.CreateNew();
        var employee2Id = UserId.CreateNew();

        var title = "Test Document";
        var content = "Sample Content";
        var documentType = DocumentType.Internal;

        var document = new DocumentAggregateRoot(title, content, senderId, documentType);

        // Act
        // 1. Sender refers the document to the manager
        var referralToManager = document.AddReferral(senderId, managerId);

        // 2. Manager refers the document to Employee 1 and Employee 2 (parallel referrals)
        var referralToEmployee1 = document.AddReferral(managerId, employee1Id);
        var referralToEmployee2 = document.AddReferral(managerId, employee2Id);

        // 3. Employee 1 responds to the referral
        document.RespondToReferral(referralToEmployee1.Id, "Reviewed by Employee 1");

        // Assert
        Assert.Equal(ReferralStatus.Responded, referralToEmployee1.Status);
        Assert.Equal(ReferralStatus.Canceled, referralToEmployee2.Status); // Employee 2's referral should be canceled

        // 4. Trying to respond to the canceled referral (Employee 2) should throw an exception
        Assert.Throws<InvalidOperationException>(() => document.RespondToReferral(referralToEmployee2.Id, "Reviewed by Employee 2"));
    }

    [Fact]
    public void Referral_Cancellation_Should_Not_Interfere_With_Sequential_Workflow()
    {
        // Arrange
        var senderId = UserId.CreateNew();
        var managerId = UserId.CreateNew();
        var engineerId = UserId.CreateNew();

        var title = "Project Document";
        var content = "Project Details";
        var documentType = DocumentType.Outgoing;

        var document = new DocumentAggregateRoot(title, content, senderId, documentType);

        // Act
        // 1. Sender refers to manager
        var referralToManager = document.AddReferral(senderId, managerId);

        // 2. Manager refers to engineer
        var referralToEngineer = document.AddReferral(managerId, engineerId);

        // Assert initial statuses
        Assert.Equal(ReferralStatus.New, referralToEngineer.Status);

        // 3. Engineer responds to the referral
        document.RespondToReferral(referralToEngineer.Id, "Approved by Engineer");

        // Assert
        Assert.Equal(ReferralStatus.Responded, referralToEngineer.Status);
        Assert.True(referralToEngineer.IsProcessed());
    }

    [Fact]
    public void Multiple_Sequential_Referrals_Should_Work_Correctly()
    {
        // Arrange
        var senderId = UserId.CreateNew();
        var managerId = UserId.CreateNew();
        var employee1Id = UserId.CreateNew();
        var employee2Id = UserId.CreateNew();
        var engineerId = UserId.CreateNew();

        var title = "Multi-Step Document";
        var content = "Content for multiple steps";
        var documentType = DocumentType.Internal;

        var document = new DocumentAggregateRoot(title, content, senderId, documentType);

        // Act
        // 1. Sender refers the document to the manager
        var referralToManager = document.AddReferral(senderId, managerId);

        // 2. Manager refers to Employee 1 and Employee 2
        var referralToEmployee1 = document.AddReferral(managerId, employee1Id);
        var referralToEmployee2 = document.AddReferral(managerId, employee2Id);

        // Employee 1 responds
        document.RespondToReferral(referralToEmployee1.Id, "Reviewed by Employee 1");

        // 3. Manager refers to Engineer
        var referralToEngineer = document.AddReferral(managerId, engineerId);

        // Engineer responds
        document.RespondToReferral(referralToEngineer.Id, "Approved by Engineer");

        // Assert
        Assert.Equal(ReferralStatus.Responded, referralToEmployee1.Status);
        Assert.Equal(ReferralStatus.Canceled, referralToEmployee2.Status); // Only Employee 1’s response is accepted
        Assert.Equal(ReferralStatus.Responded, referralToEngineer.Status);

        // Verify referral path
        var referrals = document.Referrals.ToList();
        Assert.Equal(senderId, referrals[0].ReferrerUserId);
        Assert.Equal(managerId, referrals[1].ReferrerUserId);
        Assert.Equal(managerId, referrals[2].ReferrerUserId);
        Assert.Equal(employee1Id, referrals[3].ReferrerUserId);
        Assert.Equal(managerId, referrals[4].ReferrerUserId);
    }
}
