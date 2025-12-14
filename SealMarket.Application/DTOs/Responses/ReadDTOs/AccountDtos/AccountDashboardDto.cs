using SealMarket.Application.DTOs.Responses.ReadDTOs.NotificationDtos;

namespace SealMarket.Application.DTOs.Responses.ReadDTOs.AccountDtos
{
    public record AccountDashboardDto
    (
        int Id,
        decimal Balance,
        DateTime CreatedAt,
        int CartId,
        int CartItemsQuantity,
        int NoReadNotificationsQuantity
    );
}