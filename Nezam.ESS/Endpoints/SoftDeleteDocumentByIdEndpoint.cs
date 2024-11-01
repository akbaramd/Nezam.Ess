using System.Globalization;
using FastEndpoints;
using Nezam.ESS.backend.Data;
using Nezam.ESS.backend.Data.Models;



using System.Globalization;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nezam.ESS.backend.Data;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Endpoints;


public class SoftDeleteDocumentByIdEndpointRequest
{
    [FromRoute(Name = "Id")]
    public int Id { get; set; }
}
public class SoftDeleteDocumentByIdEndpoint : Endpoint<SoftDeleteDocumentByIdEndpointRequest>
{
    private readonly AppDbContext _dbContext;
    private readonly IHostEnvironment _hostingEnvironment;

    public SoftDeleteDocumentByIdEndpoint(AppDbContext dbContext, IHostEnvironment hostingEnvironment)
    {
        _dbContext = dbContext;
        _hostingEnvironment = hostingEnvironment;
    }

    public override void Configure()
    {
        Delete("/api/document/{id}");
        Roles("User");
    }

    public override async Task HandleAsync(SoftDeleteDocumentByIdEndpointRequest request, CancellationToken ct)
    {
        var id = Route<int>("id");
        var data = await _dbContext.TblEesDocuments.FirstOrDefaultAsync(x => x.Id == id);

        if (data == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _dbContext.TblEesDocuments.Remove(data);
        await _dbContext.SaveChangesAsync(ct);
        
        await SendOkAsync(ct);
    }
}
