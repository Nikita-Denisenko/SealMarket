namespace SealMarket.Application.DTOs.Responses.EntityDtos
{
    public record ReadNotificationDto
    (
        int Id,
        string Message,
        DateTime DateTime,
        bool HasBeenRead,
        int AccountId
    );
}
