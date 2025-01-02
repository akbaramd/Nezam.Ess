using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EES.Slice.Secretariat.Application.Dto;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;
using Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore;
using Payeh.SharedKernel.Domain.Enumerations;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.GetMyDocuments;

public class GetMyDocumentsEndpoint : Endpoint<GetMyDocumentsRequest, GetMyDocumentsResponse>
{
    private readonly ISecretariatDbContext _dbContext;

    public GetMyDocumentsEndpoint(ISecretariatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/api/documents/mine");
    }

    public override async Task HandleAsync(GetMyDocumentsRequest req, CancellationToken ct)
    {
        // Extract current user ID from claims
        var userId = GetCurrentUserId();
        if (userId == null)
        {
            ThrowError("User ID not found in claims.");
        }

        // Find the participant ID associated with the user ID
        var participantId = await _dbContext.Participants
            .Where(p => p.UserId == userId)
            .Select(p => p.ParticipantId)
            .FirstOrDefaultAsync(ct);
            

        if (participantId == null)
        {
            ThrowError("Participant not found for the current user.");
        }

        // Base query
        var query = _dbContext.Documents
            .AsNoTracking()
            .Include(d => d.Attachments)
            .Include(d => d.OwnerParticipant)
            .Include(d => d.ReceiverParticipant)
            .Include(d => d.Referrals)
            .Where(d => d.OwnerParticipantId == participantId ||
                        d.ReceiverParticipantId == participantId ||
                        d.Referrals.Any(r => r.ReceiverParticipantId == participantId));

        // Apply filters
        if (!string.IsNullOrWhiteSpace(req.Filters))
        {
            var sections = req.Filters.Split(',');
            foreach (var filter in sections)
            {
                var parts = filter.Split(':');
                if (parts.Length == 2)
                {
                    var propertyPath = parts[0].Trim();
                    var value = parts[1].Trim();

                    switch (propertyPath)
                    {
                        case "TrackingCode":
                            query = query.Where(d => d.TrackingCode.Contains(value));
                            break;
                        case "Title":
                            query = query.Where(d => d.Title.Contains(value));
                            break;
                        case "LetterNumber":
                            if (int.TryParse(value, out var letterNumber))
                            {
                                query = query.Where(d => d.LetterNumber == letterNumber);
                            }
                            break;
                        case "LetterDate":
                            if (DateTime.TryParse(value, out var letterDate))
                            {
                                query = query.Where(d => d.LetterDate.Date == letterDate.Date);
                            }
                            break;
                        case "ReceiverName":
                            query = query.Where(d => d.ReceiverParticipant != null && d.ReceiverParticipant.Name.Contains(value));
                            break;
                        case "OwnerName":
                            query = query.Where(d => d.OwnerParticipant != null && d.OwnerParticipant.Name.Contains(value));
                            break;
                        case "Type":
                            query = query.Where(d => d.Type == Enumeration.FromName<DocumentType>(value));
                            break;
                        default:
                            ThrowError($"Unsupported filter property: {propertyPath}");
                            break;
                    }
                }
            }
        }

        // Apply sorting
        if (!string.IsNullOrWhiteSpace(req.Sorting))
        {
            var sortingParts = req.Sorting.Split(',');
            foreach (var sorting in sortingParts)
            {
                var parts = sorting.Split(':');
                if (parts.Length == 2)
                {
                    var propertyPath = parts[0].Trim();
                    var direction = parts[1].ToLower().Trim();

                    switch (propertyPath)
                    {
                        case "Title":
                            query = direction == "desc"
                                ? query.OrderByDescending(d => d.Title)
                                : query.OrderBy(d => d.Title);
                            break;
                        case "LetterDate":
                            query = direction == "desc"
                                ? query.OrderByDescending(d => d.LetterDate)
                                : query.OrderBy(d => d.LetterDate);
                            break;
                        case "LetterNumber":
                            query = direction == "desc"
                                ? query.OrderByDescending(d => d.LetterNumber)
                                : query.OrderBy(d => d.LetterNumber);
                            break;
                        case "TrackingCode":
                            query = direction == "desc"
                                ? query.OrderByDescending(d => d.TrackingCode)
                                : query.OrderBy(d => d.TrackingCode);
                            break;
                        case "ReceiverName":
                            query = direction == "desc"
                                ? query.OrderByDescending(d => d.ReceiverParticipant.Name)
                                : query.OrderBy(d => d.ReceiverParticipant.Name);
                            break;
                        case "OwnerName":
                            query = direction == "desc"
                                ? query.OrderByDescending(d => d.OwnerParticipant.Name)
                                : query.OrderBy(d => d.OwnerParticipant.Name);
                            break;
                        case "Type":
                            query = direction == "desc"
                                ? query.OrderByDescending(d => d.Type)
                                : query.OrderBy(d => d.Type);
                            break;
                        default:
                            ThrowError($"Unsupported sorting property: {propertyPath}");
                            break;
                    }
                }
            }
        }
        else
        {
            // Default sorting
            query = query.OrderBy(d => d.Title);
        }

        // Get total count for pagination
        var totalCount = await query.CountAsync(ct);

        // Fetch paginated results
        var documents =  query
            .Skip(req.Skip)
            .Take(req.Take).AsEnumerable()
            .Select(DocumentDto.FromEntity)
            .ToList();

        // Return response
        await SendOkAsync(new GetMyDocumentsResponse
        {
            TotalCount = totalCount,
            Results = documents,
        }, ct);
    }

    private UserId? GetCurrentUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        return userIdClaim != null ? UserId.NewId(Guid.Parse(userIdClaim.Value)) : null;
    }
}
