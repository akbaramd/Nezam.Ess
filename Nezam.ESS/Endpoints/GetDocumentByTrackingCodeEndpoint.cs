using FastEndpoints;
using Nezam.ESS.backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Nezam.ESS.backend.Endpoints;



public class GetDocumentByTrackingCodeEndpointResponse
{
    public int Id { get; set; } 
    public string Title { get; set; } = string.Empty;
    public int Type { get; set; } = 0;
    public string TrackingCode { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
    public string Createdat { get; set; } = string.Empty;
}

public class GetDocumentByTrackingCodeEndpoint : EndpointWithoutRequest<GetDocumentByTrackingCodeEndpointResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly IHostEnvironment _hostingEnvironment;

    public GetDocumentByTrackingCodeEndpoint(AppDbContext dbContext, IHostEnvironment hostingEnvironment)
    {
        _dbContext = dbContext;
        _hostingEnvironment = hostingEnvironment;
    }

    public override void Configure()
    {
        Get("/api/document/byTrackingCode/{trackingCode}");
        Roles("User");
    }

    public override async Task HandleAsync( CancellationToken ct)
    {
        var trackingCode = Route<string>("trackingCode");
        var data = await _dbContext.TblEesDocuments.FirstOrDefaultAsync(x => x.TrackingCode == trackingCode);

        if (data == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        await SendOkAsync(new GetDocumentByTrackingCodeEndpointResponse
        {
            File = data.FilePath,
            TrackingCode = data.TrackingCode,
            Title = data.Title,
            Id = data.Id,
            Createdat = new PersianDateTime(data.CreatedAt ?? DateTime.Now).ToString(),
            Type = data.Type
        }, ct);
    }
}
