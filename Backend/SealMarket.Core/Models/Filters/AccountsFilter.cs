namespace SealMarket.Core.Models.Filters
{
    public record AccountsFilter
    (
        int Page,
        int Size,
        decimal MinBalance,
        decimal MaxBalance,
        string OrderParam,
        bool ByAscending,
        string SearchText
    );
}
 