﻿using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Application.Users;
using Nezam.Modular.ESS.Infrastructure.Data;

namespace Nezam.Modular.ESS.WebApi.UseCases.Users;

public class GetUsersRequest
{
    [FromQuery]
    public int Take { get; set; } = 10; // Default page size

    [FromQuery]
    public int Skip { get; set; } = 0; // Default page index

    [FromQuery]
    public string? Search { get; set; } // Optional search term
}

public class GetUsersResponse
{
    public List<UserDto> Users { get; set; } = new(); // DTO for security
    public int TotalCount { get; set; }
}



public class GetUsersEndpoint : Endpoint<GetUsersRequest, GetUsersResponse>
{
    private readonly AppDbContext _dbContext;

    public GetUsersEndpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUsersRequest req, CancellationToken ct)
    {
        // Step 1: Prepare the query
        var query = _dbContext.Users.AsNoTracking();

        // Step 2: Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            string searchTerm = req.Search.Trim();
            query = query.Where(u =>
                u.Email != null &&
                (u.UserName.Value.Contains(searchTerm) || u.Email.Value.Contains(searchTerm)));
        }

        // Step 3: Get total count for pagination
        int totalCount = await query.CountAsync(ct);

        // Step 4: Fetch the data with pagination and map to DTO
        var users = await query
            .Skip(req.Skip)
            .Take(req.Take)
            .ToListAsync(ct);

        // Map entities to DTOs using the FromEntity method
        var userDtos = users.Select(UserDto.FromEntity).ToList();

        // Step 5: Return the response
        await SendAsync(new GetUsersResponse
        {
            Users = userDtos,
            TotalCount = totalCount
        }, cancellation: ct);
    }
}
