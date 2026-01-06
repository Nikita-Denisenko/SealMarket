namespace SealMarket.Application.DTOs.Responses.ReadDTOs.NotificationDtos
{
    public record NotificationDto
    (
        int Id,
        string Name,
        string Message,
        DateTime DateTime,
        bool HasBeenRead,
        int AccountId
    );
}
