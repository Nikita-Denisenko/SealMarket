namespace SealMarket.Core.Models.Filters
{
    public record CartsFilter
    (
        int Page,
        int Size,
        decimal MinTotalPrice,
        decimal MaxTotalPrice,
        string OrderParam,
        bool ByAscending,
        string SearchText
    );
}