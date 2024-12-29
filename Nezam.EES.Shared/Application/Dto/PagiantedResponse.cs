namespace Nezam.EEs.Shared.Application.Dto;

public class PaginatedResponse<T>
{
    public List<T> Results { get; set; } = new(); // DTO for security
    public int TotalCount { get; set; }   
}