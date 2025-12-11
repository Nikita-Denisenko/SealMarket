namespace SealMarket.Application.DTOs.Responses.EntityDtos
{
    public record ReadAccountDto
    (
        int Id,
        int UserId,
        decimal Balance,
        string Login,
        string Email,
        string PhoneNumber,
        DateTime CreatedAt,
        int CartId,
        List<int> NotificationsIds
    );
}
