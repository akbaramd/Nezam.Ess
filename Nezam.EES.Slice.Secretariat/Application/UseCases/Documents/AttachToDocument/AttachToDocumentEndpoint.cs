using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Exceptions;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Repositories;
using Payeh.SharedKernel.UnitOfWork;

[AllowFileUploads]
public class AttachToDocumentEndpoint : EndpointWithoutRequest
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

    public override async Task HandleAsync(CancellationToken ct)
    {
        using var uow = _unitOfWorkManager.Begin();

        // Validate Document ID from route
        var documentIdStr = Route<string>("DocumentId");
        var documentId = DocumentId.NewId(Guid.Parse(documentIdStr ?? throw new InvalidOperationException()));

        // Fetch the document
        var document = await _documentRepository.FindOneAsync(x => x.DocumentId == documentId);
        if (document == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Validate files
        if (Files.Count == 0 || Files[0].Length == 0)
            throw new ValidationException("No file provided or file is empty.");

        var file = Files[0];

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
            await file.CopyToAsync(stream, ct);
        }

        // Attach file to document
        document.AddAttachment(
            file.FileName,
            file.ContentType,
            file.Length,
            filePath
        );

        // Save changes
        await _documentRepository.UpdateAsync(document, true);
        await uow.CommitAsync(ct);
        await SendOkAsync(new { Message = "File uploaded and attached successfully.", FilePath = filePath }, ct);
    }
}