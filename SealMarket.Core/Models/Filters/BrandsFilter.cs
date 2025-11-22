namespace SealMarket.Core.Models.Filters
{
    public record BrandsFilter
    (
        int Page,
        int Size,
        decimal MinAverageProductPrice,
        decimal MaxAverageProductPrice,
        string OrderParam,
        bool ByAscending,
        string SearchText
    );
}
