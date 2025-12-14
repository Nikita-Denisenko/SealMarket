namespace SealMarket.Application.DTOs.Responses.ReadDTOs.NotificationDtos
{
    public record ShortNotificationDto
    (
        int Id,
        string Name, 
        DateTime DateTime,
        bool HasBeenRead
    );
}
