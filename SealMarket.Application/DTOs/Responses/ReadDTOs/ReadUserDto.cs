namespace SealMarket.Application.DTOs.Responses.ReadDTOs
{
    public record ReadUserDto
    (
        int Id,
        string Name,
        DateOnly BirthDate,
        string City
    );
}
