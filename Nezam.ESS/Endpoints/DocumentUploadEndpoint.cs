using System.Globalization;
using System.Security.Cryptography;
using FastEndpoints;
using Nezam.ESS.backend.Data;
using Nezam.ESS.backend.Data.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Nezam.ESS.backend.Endpoints;

public class DocumentUploadEndpointRequest
{
    public string Title { get; set; } = string.Empty;
    public int Type { get; set; } = 0;
    public IFormFile Upload { get; set; }
}

public class DocumentUploadEndpointResponse
{
    public int Id { get; set; } 
    public string Title { get; set; } = string.Empty;
    public string TrackingCode { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
    public string Createdat { get; set; } = string.Empty;
    public int Type { get; set; } = 0;
}

public class DocumentUploadEndpoint : Endpoint<DocumentUploadEndpointRequest, DocumentUploadEndpointResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly IHostEnvironment _hostingEnvironment;
    private const int ImageQuality = 75;
    private const int MaxImageDimension = 1024;
    private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png" };
    private static readonly string[] AllowedDocumentExtensions = { ".pdf", ".zip" };

    public DocumentUploadEndpoint(AppDbContext dbContext, IHostEnvironment hostingEnvironment)
    {
        _dbContext = dbContext;
        _hostingEnvironment = hostingEnvironment;
    }

    public override void Configure()
    {
        Post("/api/document/upload");
        AllowFileUploads();
        Roles("User");
    }

    public override async Task HandleAsync(DocumentUploadEndpointRequest req, CancellationToken ct)
    {
        if (req.Upload.Length <= 0) return;

       var userId = int.Parse(User.Claims.First(x => x.Type == "Id").Value);
            
        var createdAt = DateTime.Now;
        var trackingCode = createdAt.ToString("yyMMddHHmmss");
        var basePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot");
        var extension = Path.GetExtension(req.Upload.FileName).ToLowerInvariant();

        if (!AllowedImageExtensions.Contains(extension) && !AllowedDocumentExtensions.Contains(extension))
        {
            ThrowError("Invalid file format. Only JPG, PNG, PDF, and ZIP are allowed.");
            return;
        }

        var yearFolder = new PersianDateTime(createdAt).ToString("yyyy");
        var filePath = Path.Combine("uploads", "documents", yearFolder, $"{userId}-{trackingCode}{extension}");
        var fullPath = Path.Combine(basePath, filePath);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? throw new InvalidOperationException("Directory path is invalid"));

        if (AllowedImageExtensions.Contains(extension))
        {
            await ProcessImage(req.Upload, fullPath, ct);
        }
        else
        {
            await SaveFile(req.Upload, fullPath, ct);
        }

        var document = new TblEesDocuments
        {
            UserId = userId,
            TrackingCode = trackingCode,
            Title = req.Title,
            FilePath = filePath,
            CreatedAt = createdAt,
            State = 1,
            Type = req.Type
        };

        var res = await _dbContext.TblEesDocuments.AddAsync(document, ct);
        await _dbContext.SaveChangesAsync(ct);

        await SendOkAsync(new DocumentUploadEndpointResponse
        {
            Id = res.Entity.Id,
            Title = res.Entity.Title,
            TrackingCode = res.Entity.TrackingCode,
            File = res.Entity.FilePath,
            Createdat = new PersianDateTime(res.Entity.CreatedAt!.Value).ToString(),
            Type = res.Entity.Type
        }, ct);
    }

    private static async Task ProcessImage(IFormFile upload, string path, CancellationToken ct)
    {
        using var image = await Image.LoadAsync(upload.OpenReadStream(), ct);
        if (image.Width > MaxImageDimension || image.Height > MaxImageDimension)
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(MaxImageDimension, MaxImageDimension),
                Mode = ResizeMode.Max
            }));
        }

        var encoder = new JpegEncoder { Quality = ImageQuality };
        await image.SaveAsync(path, encoder, ct);
    }

    private static async Task SaveFile(IFormFile upload, string path, CancellationToken ct)
    {
        await using var fileStream = new FileStream(path, FileMode.Create);
        await upload.CopyToAsync(fileStream, ct);
    }
}