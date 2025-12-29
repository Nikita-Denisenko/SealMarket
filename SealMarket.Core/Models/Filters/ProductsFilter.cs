namespace SealMarket.Core.Models.Filters
{
    public record ProductsFilter
    (
        int Page,
        int Size,
        decimal MinPrice,
        decimal MaxPrice,
        string OrderParam,
        bool ByAscending,
        string SearchText,
        string CategoryName
    );
}
