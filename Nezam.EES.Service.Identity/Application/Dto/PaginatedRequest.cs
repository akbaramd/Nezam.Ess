using Microsoft.AspNetCore.Mvc;

namespace Nezam.EES.Service.Identity.Application.Dto;

public class PaginatedRequest
{
    [FromQuery]
    public int Take { get; set; } = 10; // Default page size

    [FromQuery]
    public int Skip { get; set; } = 0; // Default page index

    [FromQuery]
    public string? Search { get; set; } // Optional search term
}