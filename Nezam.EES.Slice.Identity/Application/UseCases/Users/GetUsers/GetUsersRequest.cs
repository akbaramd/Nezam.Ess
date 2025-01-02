using Nezam.EES.Service.Identity.Application.Dto;
using Nezam.EEs.Shared.Application.Dto;
using System.Collections.Generic;

namespace Nezam.EES.Service.Identity.Application.UseCases.Users.GetUsers;

/// <summary>
/// Request class for fetching users with support for pagination, filtering, and sorting.
/// </summary>
public class GetUsersRequest : PaginatedRequest
{
    /// <summary>
    /// Filters to apply on the user data. Each filter is a string in the format "Property:Value".
    /// Example: "Profile.LastName:Smith,Email.Value:test".
    /// </summary>
    public string? Filters { get; set; }

    /// <summary>
    /// Sorting instructions in the format "Property:Direction".
    /// Example: "Email.Value:asc".
    /// </summary>
    public string? Sorting { get; set; }
}