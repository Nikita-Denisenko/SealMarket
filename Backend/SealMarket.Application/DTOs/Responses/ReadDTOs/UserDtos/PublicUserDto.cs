namespace SealMarket.Application.DTOs.Responses.ReadDTOs.UserDtos
{
    public record PublicUserDto
    (
        int Id,
        string Name,
        DateOnly BirthDate,
        string City,
        string AvatarUrl
    );
}
