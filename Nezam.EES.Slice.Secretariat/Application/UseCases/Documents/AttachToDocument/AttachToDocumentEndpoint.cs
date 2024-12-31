using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Repositories;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.UnitOfWork;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.AttachToDocument;

[AllowFileUploads]
public class AttachToDocumentEndpoint : Endpoint<AttachToDocumentRequest>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public AttachToDocumentEndpoint(IDocumentRepository documentRepository, IWebHostEnvironment webHostEnvironment, IUnitOfWorkManager unitOfWorkManager)
    {
        _documentRepository = documentRepository;
        _webHostEnvironment = webHostEnvironment;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public override void Configure()
    {
        Post("/documents/{DocumentId}/attach-file");
        AllowFileUploads();
    }

    public override async Task HandleAsync(AttachToDocumentRequest req, CancellationToken ct)
    {
        using var uow = _unitOfWorkManager.Begin();

        // Validate Document ID from route
        var documentId = DocumentId.NewId(req.DocumentId);

        // Fetch the document
        var document = await _documentRepository.FindOneAsync(x => x.DocumentId == documentId);
        if (document == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Validate files
        if (req.File == null || req.File.Length == 0)
        {
            AddError("File", "No file provided or file is empty.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        // Save file to wwwroot/uploads
        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var fileName = Path.GetRandomFileName();
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await req.File.CopyToAsync(stream, ct);
        }

        // Attach file to document
        document.AddAttachment(
            req.File.FileName,
            req.File.ContentType,
            req.File.Length,
            filePath
        );

        // Save changes
        await _documentRepository.UpdateAsync(document, true);
        await uow.CommitAsync(ct);

        await SendOkAsync(ct);
    }
}

public class AttachToDocumentRequest
{
    [FromRoute]
    public Guid DocumentId { get; set; }

    [FastEndpoints.FromForm]
    public IFormFile File { get; set; } = default!;
}


