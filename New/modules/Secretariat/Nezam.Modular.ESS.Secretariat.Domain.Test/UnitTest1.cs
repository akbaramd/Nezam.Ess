using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.BonEnumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Exceptions;
using Xunit.Abstractions;

namespace Nezam.Modular.ESS.Secretariat.Domain.Test;

public class DocumentAggregateRootTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DocumentAggregateRootTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void All_Users_Can_Respond_And_Refer_To_Others()
    {
        // Arrange
        var senderId = BonUserId.CreateNew();
        var managerId = BonUserId.CreateNew();
        var employee1Id = BonUserId.CreateNew();
        var employee2Id = BonUserId.CreateNew();
        var engineerId = BonUserId.CreateNew();
        var finalReviewerId = BonUserId.CreateNew();

        var document = new DocumentAggregateRoot("Complex Document", "Content for complex workflow", senderId, DocumentType.Internal);
        document.Publish(senderId);

        // Act
        var referralToManager = document.AddInitialReferral(managerId, senderId);
        document.RespondToReferral(referralToManager.Id, "Reviewed by Manager", managerId);

        var referralToEmployee1 = document.AddReferral(referralToManager.Id, employee1Id, managerId);
        var referralToEmployee2 = document.AddReferral(referralToManager.Id, employee2Id, managerId);

        // Each employee can respond and refer further
        document.RespondToReferral(referralToEmployee1.Id, "Reviewed by Employee 1", employee1Id);
        var referralFromEmployee1ToEngineer = document.AddReferral(referralToEmployee1.Id, engineerId, employee1Id);

        document.RespondToReferral(referralToEmployee2.Id, "Reviewed by Employee 2", employee2Id);
        var referralFromEmployee2ToFinalReviewer = document.AddReferral(referralToEmployee2.Id, finalReviewerId, employee2Id);

        // Engineer and Final Reviewer respond to their referrals
        document.RespondToReferral(referralFromEmployee1ToEngineer.Id, "Reviewed by Engineer", engineerId);
        document.RespondToReferral(referralFromEmployee2ToFinalReviewer.Id, "Reviewed by Final Reviewer", finalReviewerId);

        // Assert that all referrals are correctly processed
        Assert.All(document.Referrals, referral => 
            Assert.Equal(ReferralStatus.Responded, referral.Status));
    }

    [Fact]
    public void All_Referrals_Are_Pending_If_Not_Responded()
    {
        // Arrange
        var senderId = BonUserId.CreateNew();
        var employeeId1 = BonUserId.CreateNew();
        var employeeId2 = BonUserId.CreateNew();
        
        var document = new DocumentAggregateRoot("Document Workflow", "Workflow with multiple users", senderId, DocumentType.Outgoing);
        document.Publish(senderId);

        // Act
        var referralToEmployee1 = document.AddInitialReferral(employeeId1, senderId);
        var referralToEmployee2 = document.AddInitialReferral(employeeId2, senderId);

        // Assert
        Assert.Equal(ReferralStatus.Pending, referralToEmployee1.Status);
        Assert.Equal(ReferralStatus.Pending, referralToEmployee2.Status);
        Assert.Contains(referralToEmployee1, document.GetActiveReferrals());
        Assert.Contains(referralToEmployee2, document.GetActiveReferrals());
    }

    [Fact]
    public void Multiple_Responses_And_Referrals()
    {
        // Arrange
        var senderId = BonUserId.CreateNew();
        var managerId = BonUserId.CreateNew();
        var employeeId = BonUserId.CreateNew();
        
        var document = new DocumentAggregateRoot("Multiple Responses", "Complex multi-response workflow", senderId, DocumentType.Internal);
        document.Publish(senderId);
        
        // Act
        var referralToManager = document.AddInitialReferral(managerId, senderId);
        document.RespondToReferral(referralToManager.Id, "Reviewed by Manager", managerId);

        var referralFromManagerToEmployee = document.AddReferral(referralToManager.Id, employeeId, managerId);
        document.RespondToReferral(referralFromManagerToEmployee.Id, "Reviewed by Employee", employeeId);

        // Assert all referrals are correctly responded
        Assert.Equal(ReferralStatus.Responded, referralToManager.Status);
        Assert.Equal(ReferralStatus.Responded, referralFromManagerToEmployee.Status);
        
        _testOutputHelper.WriteLine(document.ToString());
    }

    [Fact]
    public void Trying_To_Respond_To_Already_Responded_Referral_Should_Throw_Exception()
    {
        // Arrange
        var senderId = BonUserId.CreateNew();
        var managerId = BonUserId.CreateNew();
        
        var document = new DocumentAggregateRoot("Single Referral Test", "Test to ensure double response throws exception", senderId, DocumentType.Internal);
        document.Publish(senderId);

        // Act
        var referralToManager = document.AddInitialReferral(managerId, senderId);
        document.RespondToReferral(referralToManager.Id, "Reviewed by Manager", managerId);

        // Assert that responding again throws an exception
        Assert.Throws<ReferralAlreadyRespondedException>(() => document.RespondToReferral(referralToManager.Id, "Attempting second response", managerId));
    }

    [Fact]
    public void Document_Versions_Are_Created_On_Content_Update()
    {
        // Arrange
        var senderId = BonUserId.CreateNew();
        var document = new DocumentAggregateRoot("Initial Title", "Initial Content", senderId, DocumentType.Internal);
        document.Publish(senderId);
        
        // Act
        document.UpdateContent("Updated Content 1", senderId);
        document.UpdateContent("Updated Content 2", senderId);

    }

    [Fact]
    public void Document_Activity_Log_Should_Record_All_Actions()
    {
        // Arrange
        var senderId = BonUserId.CreateNew();
        var managerId = BonUserId.CreateNew();
        
        var document = new DocumentAggregateRoot("Activity Log Test", "Initial Content", senderId, DocumentType.Internal);
        
        // Act
        document.Publish(senderId);
        document.AddInitialReferral(managerId, senderId);
        document.UpdateContent("Updated Content", senderId);
        document.Archive(senderId);

        // Assert: Check that each activity is logged
        Assert.Equal(5, document.ActivityLogs.Count); // Publish, AddReferral, UpdateContent, Archive
    }

    [Fact]
    public void Updating_Title_And_Type_Should_Create_New_Versions()
    {
        // Arrange
        var senderId = BonUserId.CreateNew();
        var document = new DocumentAggregateRoot("Initial Title", "Initial Content", senderId, DocumentType.Internal);
        
        // Act
        document.Publish(senderId);
        document.UpdateTitle("Updated Title", senderId);
        document.ChangeType(DocumentType.Outgoing, senderId);

    }

    [Fact]
    public void Archiving_Document_Should_Not_Allow_Further_Changes()
    {
        // Arrange
        var senderId = BonUserId.CreateNew();
        var document = new DocumentAggregateRoot("Archivable Document", "Archivable Content", senderId, DocumentType.Internal);
        document.Publish(senderId);

        // Act
        document.Archive(senderId);

        // Assert: Ensure archived document cannot be updated or changed
        Assert.Throws<InvalidOperationException>(() => document.UpdateContent("New Content", senderId));
        Assert.Throws<InvalidOperationException>(() => document.UpdateTitle("New Title", senderId));
        Assert.Throws<InvalidOperationException>(() => document.ChangeType(DocumentType.Outgoing, senderId));
        Assert.Equal(DocumentStatus.Archive, document.Status);
    }
}
