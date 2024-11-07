using FastEndpoints;
using Nezam.ESS.backend.Data;

namespace Nezam.ESS.backend.Endpoints;

public class GetDocumentListEndpointResponse
{
    public int Id { get; set; } 
    public string Title { get; set; } = string.Empty;
    public string TrackingCode { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
    public string Createdat { get; set; } = string.Empty;
    public int Type { get; set; } = 0;
}

public class GetDocumentListEndpoint : EndpointWithoutRequest<List<GetDocumentListEndpointResponse>>
{
    private readonly AppDbContext _dbContext;
    private readonly IHostEnvironment _hostingEnvironment;

    public GetDocumentListEndpoint(AppDbContext dbContext, IHostEnvironment hostingEnvironment)
    {
        _dbContext = dbContext;
        _hostingEnvironment = hostingEnvironment;
    }

    public override void Configure()
    {
        Get("/api/document");
        Roles("User");
    }

    public override async Task HandleAsync( CancellationToken ct)
    {
        var BonUserId = int.Parse(User.Claims.First(x => x.Type == "Id").Value);
        var data = _dbContext.TblEesDocuments.Where(x => x.BonUserId == BonUserId && x.State == 1).ToList().Select(x => new GetDocumentListEndpointResponse
        {
            File = x.FilePath,
            TrackingCode = x.TrackingCode,
            Title = x.Title,
            Id = x.Id,
            Createdat = new PersianDateTime(x.CreatedAt ?? DateTime.Now).ToString(),
            Type = x.Type,
        }).OrderByDescending(x=>x.Id).ToList();
        await SendOkAsync(data, ct);
    }
}