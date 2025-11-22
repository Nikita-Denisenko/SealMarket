namespace SealMarket.Core.Models.Filters
{
    public record UsersFilter
    (
        int Page,
        int Size,
        int MinAge,
        int MaxAge,
        string OrderParam,
        bool ByAscending,
        string SearchText
    );
}
