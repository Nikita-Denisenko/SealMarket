namespace SealMarket.Application.DTOs.Requests.UpdateDTOs
{
    public record UpdateUserDto
    (
        string? Name = null,
        DateOnly? BirthDate = null,
        string? City = null
    );
}
