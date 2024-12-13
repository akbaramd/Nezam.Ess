using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.User;
using Payeh.SharedKernel.EntityFrameworkCore.FluentQueries;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Nezam.Modular.ESS.Infrastructure.Data;
using Payeh.SharedKernel.Domain.Repositories;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Users;

public class GetUsersRequest
{
    [FromQuery]
    public int Take { get; set; } = 10; // Default page size

    [FromQuery]
    public int Skip { get; set; } = 0; // Default page index

    [FromQuery]
    public string? Filters { get; set; } = string.Empty; // JSON string of filters

    [FromQuery]
    public string? Sorts { get; set; } = string.Empty; // JSON string of sorts

    [FromQuery]
    public string? Includes { get; set; } = string.Empty; // Comma-separated include paths

    public FluentQuery ToFluentQuery()
    {
        var filters = string.IsNullOrWhiteSpace(Filters) ? new List<FluentQueryFilter>() : JsonSerializer.Deserialize<List<FluentQueryFilter>>(Filters);
        var sorts = string.IsNullOrWhiteSpace(Sorts) ? new List<FluentQuerySort>() : JsonSerializer.Deserialize<List<FluentQuerySort>>(Sorts);
        var includes = string.IsNullOrWhiteSpace(Includes) ? Array.Empty<string>() : Includes.Split(",");

        return new FluentQuery
        {
            Take = Take,
            Skip = Skip,
            Filters = filters ?? new List<FluentQueryFilter>(),
            Sorts = sorts ?? new List<FluentQuerySort>(),
            Includes = includes
        };
    }
}

public class GetUsersResponse
{
    public List<UserEntity> Users { get; set; } = new();
    public int TotalCount { get; set; }
}

public class GetUsersEndpoint : Endpoint<GetUsersRequest, GetUsersResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public GetUsersEndpoint(IUserRepository userRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _userRepository = userRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUsersRequest req, CancellationToken ct)
    {
        using var uow = _unitOfWorkManager.Begin();
        var query = await _userRepository.GetQueryableAsync();

        // Convert request to FluentQuery and apply it
        var fluentQuery = req.ToFluentQuery();
        query = query.ApplyFluentQuery(fluentQuery);

        // Get total count for pagination
        var totalCount = await query.CountAsync(ct);

        // Fetch the paginated and filtered results
        var users = await query.ToListAsync(ct);

        await SendAsync(new GetUsersResponse
        {
            Users = users,
            TotalCount = totalCount
        }, cancellation: ct);
    }
}
