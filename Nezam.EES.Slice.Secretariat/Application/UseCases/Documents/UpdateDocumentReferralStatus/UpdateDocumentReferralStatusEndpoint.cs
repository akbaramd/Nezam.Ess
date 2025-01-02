using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Repositories;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.Enumerations;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.UpdateDocumentReferralStatus;

public class UpdateDocumentReferralStatusEndpoint : Endpoint<UpdateDocumentReferralStatusRequest>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public UpdateDocumentReferralStatusEndpoint(IDocumentRepository documentRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _documentRepository = documentRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public override void Configure()
    {
        Put("/api/documents/{DocumentId}/referrals/{ReferralId}/update-status");
    }

    public override async Task HandleAsync(UpdateDocumentReferralStatusRequest req, CancellationToken ct)
    {
        using var uow = _unitOfWorkManager.Begin();

        // Validate Document ID and Referral ID
        var documentId = DocumentId.NewId(req.DocumentId);
        var referralId = DocumentReferralId.NewId(req.ReferralId);

        // Fetch the document and referral
        var document = await _documentRepository.FindOneAsync(x => x.DocumentId == documentId);
        if (document == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var referral = document.Referrals.FirstOrDefault(r => r.DocumentReferralId == referralId);
        if (referral == null)
        {
            AddError("Referral", "Referral not found.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

    

        referral.UpdateStatus(Enumeration.FromName<ReferralStatus>(req.Status)??ReferralStatus.Pending);

        // Save changes
        await _documentRepository.UpdateAsync(document, true);
        await uow.CommitAsync(ct);

        await SendOkAsync(ct);
    }
}

public class UpdateDocumentReferralStatusRequest
{
    [FromRoute]
    public Guid DocumentId { get; set; }

    [FromRoute]
    public Guid ReferralId { get; set; }


    public string Status { get; set; } // Enum value for ReferralStatus
}
