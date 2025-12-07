using static SealMarket.Core.Constans.NotificationOrderParams;

namespace SealMarket.Application.DTOs.Requests.FilterDTOs
{
    public record NotificationsFilterDto
    (
        int Page = 1,
        int Size = 20,
        DateTime? FromDateTime = null,
        DateTime? ToDateTime = null,
        bool HasBeenRead = false,
        string OrderParam = Name,
        bool ByAscending = true,
        string SearchText = ""
    );
}
