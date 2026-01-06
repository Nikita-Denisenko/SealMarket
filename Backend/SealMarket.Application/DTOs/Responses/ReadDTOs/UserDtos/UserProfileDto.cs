namespace SealMarket.Application.DTOs.Responses.ReadDTOs.UserDtos
{
    public record UserProfileDto
    (
        int Id,
        string Name,
        DateOnly BirthDate,
        string City,
        int AccountId,
        string Login,
        string Email,
        string PhoneNumber,
        decimal Balance,
        DateTime RegisteredAt,
        string AvatarUrl
    );
}
