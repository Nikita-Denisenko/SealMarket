namespace SealMarket.Core.Models.Filters
{
    public record NotificationsFilter
    (
        int Page,
        int Size,
        DateTime FromDateTime,
        DateTime ToDateTime,
        bool HasBeenRead,
        string OrderParam,
        bool ByAscending,
        string SearchText
    );
}
